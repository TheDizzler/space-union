using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structures;
using Data_Manipulation;
using System.Threading;

//complete game room creation system
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
        public Dictionary<int, Gameroom> Gamerooms { get; private set; }
        /// <summary>
        /// Total list of online players.
        /// </summary>
        public Dictionary<string, Player> OnlinePlayers { get; private set; }

        public Server()
        {
            Gamerooms = new Dictionary<int, Gameroom>();
            OnlinePlayers = new Dictionary<string, Player>();
            Receiving = new DataReceiving(this);
            Transmission = new DataTransmission(this);
            new Thread(cleanRooms).Start();
        }

        private void cleanRooms()
        {
            while (true)
            {
                foreach (KeyValuePair<int, Gameroom> room in Gamerooms.ToArray())
                {
                    if (room.Value.Players == 0)
                        Gamerooms.Remove(room.Key);
                    foreach (GameData player in room.Value.getPlayerList())
                        if (compareTime(player.Time, 10, true))
                            room.Value.removePlayer(player);
                }
                Thread.Sleep(5000);
            }
        }

        private bool compareTime(DateTime time, int period, bool seconds)
        {
            if (seconds)
                if ((DateTime.Now - time).Seconds > period)
                    return true;
            if (!seconds)
                if ((DateTime.Now - time).Minutes > period)
                    return true;
            return false;
        }

        public void updatePlayer(GameData player)
        {
            Gameroom room = getGameroom(player.Player.GameRoom);
            if (room == null)
                return;
            room.updatePlayer(player);
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
            if (player != null)
            {
                player.PortSend = Constants.UDPClientToServerPort + (OnlinePlayers.Count % 6);
                player.PortReceive = Constants.UDPServerToClientPort + (OnlinePlayers.Count % 6);
                OnlinePlayers.Add(player.Username, player);
                Transmission.addMessageToQueue(player);
                addPlayerToFreeRoom(player);
            }
        }

        /// <summary>
        /// Sends the room list to the given player.
        /// </summary>
        /// <param name="player">The player to send the room list to.</param>
        public void sendRoomList(Player player)
        {
            addMessageToQueue(new RoomList(player, organizeRoomList()));
        }

        /// <summary>
        /// Add the given player to the room matching the given room number.
        /// </summary>
        /// <param name="player">The player to add to the room.</param>
        /// <param name="roomNumber">The room number of the room to add the player to.</param>
        public void addPlayerToRequestedRoom(Player player, int roomNumber)
        {
            Gameroom room = getGameroom(roomNumber);
            if (room == null || !room.addPlayer(player))
                addMessageToQueue(new RoomList(player, organizeRoomList()));
            addMessageToQueue(new RoomInfo(room.getPlayers(), room.RoomNumber, room.RoomName, room.Host, room.InGame, player));
        }

        /// <summary>
        /// Create a game room upon a player request.
        /// </summary>
        /// <param name="player">The player requesting the room creation.</param>
        /// <param name="roomName">The name of the room specified by the player.</param>
        public void createPlayerRequestedRoom(Player player, string roomName)
        {
            int assignedRoomNumber = findAvailableRoomNumber();
            if (assignedRoomNumber == 0)
            {
                addMessageToQueue(new RoomList(player, organizeRoomList()));
                return;
            }
            Gameroom room = new Gameroom(assignedRoomNumber, roomName, player);
            Gamerooms.Add(assignedRoomNumber, room);
            addMessageToQueue(new RoomInfo(room.getPlayers(), room.RoomNumber, room.RoomName, room.Host, room.InGame, player));
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
        /// TEMPORARY METHOD FOR TESTING.
        /// </summary>
        /// <param name="player"></param>
        private void addPlayerToFreeRoom(Player player)
        {
            foreach (KeyValuePair<int, Gameroom> room in Gamerooms.ToArray())
            {
                if (room.Value.Players < 6)
                {
                    player.GameRoom = room.Value.RoomNumber;
                    room.Value.addPlayer(player);
                    return;
                }
            }
            Gameroom temproom = new Gameroom();
            temproom.RoomNumber = Gamerooms.Count;
            player.GameRoom = temproom.RoomNumber;
            temproom.addPlayer(player);
            Gamerooms.Add(temproom.RoomNumber, temproom);
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
            {
                RoomInfo info = new RoomInfo(room.Value.getPlayers(), room.Value.RoomNumber, room.Value.RoomName, room.Value.Host, room.Value.InGame);
                list.Add(info);
            }
            return list;
        }
    }
}
