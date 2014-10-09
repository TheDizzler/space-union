using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structures;
using Data_Manipulation;

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
        DataReceiving receiving;
        /// <summary>
        /// The class responsible for transmitting data to clients.
        /// </summary>
        DataTransmission transmission;
        /// <summary>
        /// List of all available game rooms.
        /// </summary>
        List<Gameroom> gamerooms;
        /// <summary>
        /// Total list of online players.
        /// </summary>
        List<Player> onlineplayers;
        /// <summary>
        /// Only players who are currently looking for a game match.
        /// </summary>
        List<Player> searchingplayers;

        public Server()
        {
            gamerooms = new List<Gameroom>();
            onlineplayers = new List<Player>();
            searchingplayers = new List<Player>();
            receiving = new DataReceiving(this);
            transmission = new DataTransmission(this);
        }

        /// <summary>
        /// Retrieve the gameroom matching the given room number.
        /// </summary>
        /// <param name="roomnum">The room number of the gameroom.</param>
        /// <returns>The gameroom matching the room number.</returns>
        public Gameroom getGameroom(int roomnum)
        {
            foreach (Gameroom room in gamerooms)
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
                player.PortSend = Constants.UDPClientToServerPort + (onlineplayers.Count % 6);
                player.PortReceive = Constants.UDPServerToClientPort + (onlineplayers.Count % 6);
                onlineplayers.Add(player);
                transmission.addMessageToQueue(player);
                //the following line is only used for the prototype
                addPlayerToFreeRoom(player);
            }
        }

        private void addPlayerToFreeRoom(Player player)
        {
            foreach (Gameroom room in gamerooms)
            {
                if (room.getPlayerList().Count < 6)
                {
                    room.addPlayer(player);
                    return;
                }
            }
            Gameroom temproom = new Gameroom();
            temproom.RoomNumber = gamerooms.Count;
            temproom.addPlayer(player);
        }

        /// <summary>
        /// Adds a message to the DataTransmission queue.
        /// </summary>
        /// <param name="message">The message to add to the queue.</param>
        public void addMessageToQueue(Data message)
        {
            if (message != null)
            {
                transmission.addMessageToQueue(message);
            }
        }

        /// <summary>
        /// Checks the number of currently active game rooms.
        /// </summary>
        public void getNumberOfRooms()
        {
            Console.WriteLine("Number of currently active game rooms: " + gamerooms.Count + "\n");
        }

        /// <summary>
        /// Checks the number of online players.
        /// </summary>
        public void getNumberOfOnlinePlayers()
        {
            Console.WriteLine("Number of online players: " + onlineplayers.Count + "\n");
        }

        /// <summary>
        /// Checks the number of players searching for games.
        /// </summary>
        public void getNumberOfSearchingPlayers()
        {
            Console.WriteLine("Number of players searching for games: " + searchingplayers.Count + "\n");
        }
        
        /// <summary>
        /// Checks the size of the Error Message queue.
        /// </summary>
        public void checkErrorQueueSize()
        {
            transmission.checkErrorQueueSize();
        }

        /// <summary>
        /// Checks the size of the Chat Message queue.
        /// </summary>
        public void checkChatMessageQueueSize()
        {
            transmission.checkChatMessageQueueSize();
        }

        /// <summary>
        /// Checks the size of the Login Request queue.
        /// </summary>
        public void checkLoginRequestQueueSize()
        {
            transmission.checkLoginRequestQueueSize();
        }

        /// <summary>
        /// Checks the size of the Game Data queues.
        /// </summary>
        public void checkGameDataQueueSize()
        {
            transmission.checkGameDataQueueSize();
        }
    }
}
