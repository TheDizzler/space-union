using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structures;
using Data_Manipulation;
using System.Threading;
using System.Collections.Concurrent;
using SpaceUnionDatabase;

namespace Server_Application
{
    /// <summary>
    /// This is the main server application. It will tie together
    /// the DataReceiving and DataTransmission classes, ensuring
    /// fluid communication between the two. It should be set up
    /// in a fashion so that instantiating this class starts the
    /// server running.
    /// </summary>
    class Server
    {
        /// <summary>
        /// The class responsible for listening to data from clients.
        /// </summary>
        public DataReceiving Receiving { get; private set; }
        /// <summary>
        /// The class responsible for transmitting data to clients.
        /// </summary>
        public DataTransmission Transmission { get; private set; }
        /// <summary>
        /// List of all available game rooms.
        /// </summary>
        //public List<Gameroom> Gamerooms { get; private set; }
        public ConcurrentDictionary<int, Gameroom> Gamerooms { get; private set; }
        /// <summary>
        /// Total list of online players.
        /// </summary>
        public ConcurrentDictionary<string, Player> OnlinePlayers { get; private set; }
        /// <summary>
        /// Allows read/write to the user table in the spaceunion database
        /// </summary>
        private UserTableAccess userTable = new UserTableAccess();

        public Server()
        {
            Gamerooms = new ConcurrentDictionary<int, Gameroom>();
            OnlinePlayers = new ConcurrentDictionary<string, Player>();
            Receiving = new DataReceiving(this);
            Transmission = new DataTransmission(this);
            new Thread(stopGame).Start();
            //new Thread(cleanRooms).Start();
        }

        private void cleanRooms()
        {
            while (true)
            {
                foreach (KeyValuePair<int, Gameroom> room in Gamerooms.ToArray())
                {
                    if (room.Value.Players == 0)
                    {
                        Gameroom temp;
                        Gamerooms.TryRemove(room.Key, out temp);
                        continue;
                    }
                    foreach (GameData player in room.Value.getPlayerList())
                    {
                        if (compareTime(player.Player.Time, 10))
                        {
                            room.Value.removePlayer(player.Player);
                        }
                    }
                }
                foreach(KeyValuePair<string, Player> player in OnlinePlayers.ToArray())
                {
                    if (compareTime(player.Value.Time, 10))
                    {
                        Player temp;
                        OnlinePlayers.TryRemove(player.Key, out temp);
                    }
                }
                Thread.Sleep(10000);
            }
        }

        private bool compareTime(DateTime time, int period)
        {
            if ((DateTime.Now - time).Seconds > period)
                return true;
            return false;
        }

        public void updateOnlinePlayer(Player player)
        {
            OnlinePlayers.AddOrUpdate(player.Username, player, (k, v) => v = player);
        }

        public void updatePlayer(GameData player)
        {
            Gameroom room = getGameroom(player.Player.GameRoom);
            if (room == null)
                return;
            room.updatePlayer(player);
        }

        public void stopGame()
        {
            while (true)
            {
                foreach (Gameroom room in Gamerooms.Values.ToArray())
                {
                    if ((DateTime.Now - room.GameStart).Minutes == Constants.GamePeriod && room.InGame)
                    {
                        room.InGame = false;
                        foreach (GameData data in room.getPlayerList())
                            addMessageToQueue(new PlayerRequest(data.Player, Constants.PLAYER_REQUEST_END));
                        room.GameStart = DateTime.Now;
                        room.stopGame();
                    }
                }
                Thread.Sleep(1000);
            }
        }

        public void listRooms()
        {
            foreach (Gameroom room in Gamerooms.Values.ToArray())
            {
                Console.WriteLine("Gameroom: " + room.RoomNumber);
                foreach (GameData player in room.getPlayerList())
                    Console.WriteLine("Player: " + player.Player.Username);
            }
        }

        public void removePlayerFromRoom(Player player, int roomNumber)
        {
            Gameroom room = getGameroom(roomNumber);
            if (room == null || player == null)
                return;
            room.removePlayer(player);
            if (room.Players == 0)
                Gamerooms.TryRemove(roomNumber, out room);
            sendRoomUpdate(roomNumber);
        }

        private void sendRoomUpdate(int roomNumber)
        {
            Gameroom room = getGameroom(roomNumber);
            if (room == null)
                return;
            if(!room.InGame)
                foreach (GameData p in room.getPlayerList())
                    addMessageToQueue(new RoomInfo(room.getPlayerList(), room.RoomNumber, room.RoomName, room.Host.Username, room.InGame, p.Player.IPAddress));
        }

        /// <summary>
        /// Retrieve the gameroom matching the given room number.
        /// </summary>
        /// <param name="roomnum">The room number of the gameroom.</param>
        /// <returns>The gameroom matching the room number.</returns>
        public Gameroom getGameroom(int roomnum)
        {
            Gameroom room;
            Gamerooms.TryGetValue(roomnum, out room);
            return room;
        }
        /// <summary>
        /// Add the given player to the list of online players.
        /// </summary>
        /// <param name="player">The player to mark as active.</param>
        public void addOnlinePlayer(Player player)
        {
            if (player == null)
                return;
            player.PortSend = Constants.UDPClientToServerPort + (OnlinePlayers.Count % 6);
            player.PortReceive = Constants.UDPServerToClientPort + (OnlinePlayers.Count % 6);
            OnlinePlayers.AddOrUpdate(player.Username, player, (k, v) => v = player);
            Transmission.addMessageToQueue(player);
        }

        /// <summary>
        /// Sends the room list to the given player.
        /// </summary>
        /// <param name="player">The player to send the room list to.</param>
        public void sendRoomList(Player player)
        {
            if(player != null)
                addMessageToQueue(new RoomList(player, organizeRoomList()));
        }

        public void sendRoomInfo(Player player, int roomNumber)
        {
            Gameroom room = getGameroom(roomNumber);
            if (room != null && player != null)
                addMessageToQueue(new RoomInfo(room.getPlayerList(), room.RoomNumber, room.RoomName, room.Host.Username, room.InGame, player.IPAddress));
        }

        /// <summary>
        /// Add the given player to the room matching the given room number.    
        /// </summary>
        /// <param name="player">The player to add to the room.</param>
        /// <param name="roomNumber">The room number of the room to add the player to.</param>
        public void addPlayerToRequestedRoom(Player player, int roomNumber)
        {
            Gameroom room = getGameroom(roomNumber);
            if (room == null)
                return;
            if(!OnlinePlayers.ContainsKey(player.Username) || !room.addPlayer(OnlinePlayers[player.Username]))
                return;
            addMessageToQueue(new RoomList(player, organizeRoomList()));
            sendRoomUpdate(roomNumber);
        }

        public void updateOnHeartbeat(PlayerRequest request)
        {
            if (request != null)
                if (OnlinePlayers.ContainsKey(request.Sender.Username))
                    OnlinePlayers[request.Sender.Username].Time = DateTime.Now;
        }

        /// <summary>
        /// Create a game room upon a player request.
        /// </summary>
        /// <param name="player">The player requesting the room creation.</param>
        /// <param name="roomName">The name of the room specified by the player.</param>
        public void createPlayerRequestedRoom(Player player, string roomName)
        {
            if (player == null)
                return;
            int assignedRoomNumber = findAvailableRoomNumber();
            if (assignedRoomNumber == 0)
            {
                addMessageToQueue(new RoomList(player, organizeRoomList()));
                return;
            }
            Gameroom room = new Gameroom(assignedRoomNumber, roomName, player);
            Gamerooms.TryAdd(assignedRoomNumber, room);
            addMessageToQueue(new RoomInfo(room.getPlayerList(), room.RoomNumber, room.RoomName, room.Host.Username, room.InGame, player.IPAddress));
        }

        public void updatePlayerReadyStatus(Player player, int roomNumber)
        {
            Gameroom room = getGameroom(roomNumber);
            if (room != null && player != null)
            {
                GameData temp = room.getPlayer(player.Username);
                temp.Player.Ready = true;
                room.updatePlayer(temp);
            }
        }

        public void updatePlayerShipChoice(Player player, int roomNumber) 
        {
            Gameroom room = getGameroom(roomNumber);
            if (room != null && player != null)
            {
                GameData temp = room.getPlayer(player.Username);
                temp.Player.ShipChoice = player.ShipChoice;
                room.updatePlayer(temp);
            }
        }

        public void startGame(Player sender, int roomNumber)
        {
            Gameroom room = getGameroom(roomNumber);
            if (room == null || sender == null)// || room.Players % 2 == 1)
                return;
            room.InGame = true;
            room.GameStart = DateTime.Now;
            foreach (GameData p in room.getPlayerList())
                addMessageToQueue(new PlayerRequest(p.Player, Constants.PLAYER_REQUEST_START));
        }

        public void sendLoginConfirmation(Player player)
        {
            if (player == null)
                return;
            addMessageToQueue(player);
        }

        public void handleLogout(Player player)
        {
            if (player == null)
                return;
            Player temp;
            int updateOnlinErrCode = 0;
            OnlinePlayers.TryRemove(player.Username, out temp);
            userTable.UpdateUserIsOnline(player.Username, 0, ref updateOnlinErrCode); //to be moved when a user is kicked from server
        }

        /// <summary>
        /// Find the lowest available game room number.
        /// If no more rooms can be created, return 0.
        /// </summary>
        /// <returns>The lowest available game room number. 0 if no more rooms can be created.</returns>
        private int findAvailableRoomNumber()
        {
            for (int roomNumber = 1; roomNumber <= Constants.MAX_NUMBER_OF_ROOMS; roomNumber++)
                if (getGameroom(roomNumber) == null)
                    return roomNumber;
            return 0;
        }

        /// <summary>
        /// Adds a message to the DataTransmission queue.
        /// </summary>
        /// <param name="message">The message to add to the queue.</param>
        public void addMessageToQueue(Data message)
        {
            if (message != null)
                Transmission.addMessageToQueue(message);
        }

        private List<RoomInfo> organizeRoomList()
        {
            List<RoomInfo> list = new List<RoomInfo>();
            foreach (KeyValuePair<int, Gameroom> room in Gamerooms.ToArray())
                list.Add(new RoomInfo(room.Value.getPlayerList(), room.Value.RoomNumber, room.Value.RoomName, room.Value.Host.Username, room.Value.InGame));
            return list;
        }
    }
}
