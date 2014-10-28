using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Data_Structures;

namespace Server_Application
{
    class ServerAnalyzer
    {
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

        public ServerAnalyzer(DataTransmission transmission,
                              List<Gameroom> gamerooms,
                              List<Player> onlineplayers,
                              List<Player> searchingplayers)
        {
            this.transmission = transmission;
            this.gamerooms = gamerooms;
            this.onlineplayers = onlineplayers;
            this.searchingplayers = searchingplayers;
        }

        /// <summary>
        /// Displays a list of players and the receiving port they're using.
        /// </summary>
        public void getReceivingPorts()
        {
            foreach (Player player in onlineplayers)
            {
                Console.WriteLine("Player - " + player.Username + " Receiving port: " + player.PortReceive);
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

        /// <summary>
        /// Displays the amount of memory used by the server in bytes.
        /// </summary>
        public void usedMemory()
        {
            Console.WriteLine("Bytes used by this application: " + Process.GetCurrentProcess().PrivateMemorySize64 + "\n");
        }

        /// <summary>
        /// Displays the number of threads running in the application.
        /// </summary>
        public void threadsRunning()
        {
            Console.WriteLine("Number of threads running: " + Process.GetCurrentProcess().Threads.Count + "\n");
        }
    }
}
