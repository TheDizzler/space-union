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
        public List<Gameroom> Gamerooms { get; private set; }
        /// <summary>
        /// Total list of online players.
        /// </summary>
        public List<Player> OnlinePlayers { get; private set; }
        /// <summary>
        /// Only players who are currently looking for a game match.
        /// </summary>
        public List<Player> SearchingPlayers { get; private set; }

        public Server()
        {
            Gamerooms = new List<Gameroom>();
            OnlinePlayers = new List<Player>();
            SearchingPlayers = new List<Player>();
            Receiving = new DataReceiving(this);
            Transmission = new DataTransmission(this);
            new Thread(cleanRooms).Start();
        }

        private void cleanRooms()
        {
            while (true)
            {
                foreach (Gameroom room in Gamerooms.ToArray())
                {
                    foreach (GameData player in room.getPlayerList())
                    {
                        if (compareTime(player.Time, 10, true))
                        {
                            room.removePlayer(player);
                        }
                    }
                    if (room.Players == 0)
                        Gamerooms.Remove(room);
                }
                Thread.Sleep(10000);
            }
        }

        private bool compareTime(DateTime time, int period, bool seconds)
        {
            if(seconds)
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
            foreach (Gameroom room in Gamerooms.ToArray())
            {
                if (room.RoomNumber == roomnum)
                {
                    return room;
                }
            }
            return null;
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
                OnlinePlayers.Add(player);
                Transmission.addMessageToQueue(player);
                // the following line is only used for the prototype
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
            foreach (Gameroom room in Gamerooms.ToArray())
            {
                // If a room matching the given room number was found 
                // and the player was successfully added to it.
                if (room.RoomNumber == roomNumber && room.addPlayer(player))
                {
                    // Send the room information to the player.
                    addMessageToQueue(new RoomInfo(room.getPlayers(), room.RoomNumber, room.RoomName, room.Host, room.InGame, player));   
                }
                else
                {
                    // If the player was not successfully added because the room was full or whatever reason,
                    // then send the room list.
                    addMessageToQueue(new RoomList(player, organizeRoomList()));
                }
            }
        }

        /// <summary>
        /// Create a game room upon a player request.
        /// </summary>
        /// <param name="player">The player requesting the room creation.</param>
        /// <param name="roomName">The name of the room specified by the player.</param>
        public void createPlayerRequestedRoom(Player player, string roomName)
        {
            int assignedRoomNumber = findAvailableRoomNumber();

            // If no more rooms can be created.
            if (assignedRoomNumber == 0)
            {
                // then send the room list.
                addMessageToQueue(new RoomList(player, organizeRoomList()));
            }
            else
            {
                // If a room was successfully created
                // add the room to the list and send the room info to the player.
                Gameroom room = new Gameroom(findAvailableRoomNumber(), roomName, player);
                Gamerooms.Add(room);
                addMessageToQueue(new RoomInfo(room.getPlayers(), room.RoomNumber, room.RoomName, room.Host, room.InGame, player));
            }
        }

        /// <summary>
        /// Find the lowest available game room number.
        /// If no more rooms can be created, return 0.
        /// </summary>
        /// <returns>The lowest available game room number. 0 if no more rooms can be created.</returns>
        private int findAvailableRoomNumber()
        {
            for (int roomNumber = 1; roomNumber <= Constants.MAX_NUMBER_OF_ROOMS; ++roomNumber)
            {
                bool matchFound = false;
                foreach (Gameroom room in Gamerooms.ToArray())
                {
                    matchFound = (room.RoomNumber == roomNumber);
                }
                if (!matchFound)
                {
                    return roomNumber;
                }
            }
            return 0;
        }

        /// <summary>
        /// TEMPORARY METHOD FOR TESTING.
        /// </summary>
        /// <param name="player"></param>
        private void addPlayerToFreeRoom(Player player)
        {
            foreach (Gameroom room in Gamerooms.ToArray())
            {
                if (room.Players < 6)
                {
                    player.GameRoom = room.RoomNumber;
                    room.addPlayer(player);
                    return;
                }
            }
            Gameroom temproom = new Gameroom();
            temproom.RoomNumber = Gamerooms.Count;
            player.GameRoom = temproom.RoomNumber;
            temproom.addPlayer(player);
            Gamerooms.Add(temproom);
        }

        /// <summary>
        /// Adds a message to the DataTransmission queue.
        /// </summary>
        /// <param name="message">The message to add to the queue.</param>
        public void addMessageToQueue(Data message)
        {
            if (message != null)
            {
                Transmission.addMessageToQueue(message);
            }
        }

        private List<RoomInfo> organizeRoomList()
        {
            List<RoomInfo> list = new List<RoomInfo>();

            foreach (Gameroom room in Gamerooms.ToArray())
            {
                RoomInfo info = new RoomInfo(room.getPlayers(), room.RoomNumber, room.RoomName, room.Host, room.InGame);
                list.Add(info);
            }

            return list;
        }
    }
}
