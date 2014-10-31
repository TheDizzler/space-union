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
        /// Add the given player to the room matching the given room number.
        /// </summary>
        /// <param name="player">The player to add to the room.</param>
        /// <param name="roomNumber">The room number of the room to add the player to.</param>
        private bool addPlayerToRequestedRoom(Player player, int roomNumber)
        {
            foreach (Gameroom room in Gamerooms.ToArray())
            {
                if (room.RoomNumber == roomNumber)
                {
                    if (room.addPlayer(player))
                    {
                        // If the player was successfully added.

                        // Send confirmation message.

                        return true;
                    }
                    else
                    {
                        // If the player was not successfully added because the room was full or whatever reason.

                        // Send an error message and update the player's room list.
                        Transmission.addMessageToQueue(new ErrorMessage(player, 2));
                        Transmission.addMessageToQueue(new RoomList(player, organizeRoomList()));
                        return false;
                    }
                }
            }
            return false;
        }

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
