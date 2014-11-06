using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Data_Structures;
using System.Collections.Concurrent;

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
        ConcurrentDictionary<int, Gameroom> gamerooms;
        /// <summary>
        /// Total list of online players.
        /// </summary>
        ConcurrentDictionary<string, Player> onlineplayers;

        public ServerAnalyzer(DataTransmission transmission,
                              ConcurrentDictionary<int, Gameroom> gamerooms,
                              ConcurrentDictionary<string, Player> onlineplayers)
        {
            this.transmission = transmission;
            this.gamerooms = gamerooms;
            this.onlineplayers = onlineplayers;
        }

        /// <summary>
        /// Displays a list of players and the receiving port they're using.
        /// </summary>
        public void getReceivingPorts()
        {
            foreach (KeyValuePair<string, Player> player in onlineplayers)
                Console.WriteLine("Player - " + player.Key + " Receiving port: " + player.Value.PortReceive);
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
        /// Checks the size of the Chat Message queue.
        /// </summary>
        public void checkChatMessageQueueSize()
        {
            transmission.checkChatMessageQueueSize();
        }

        public void checkGenericQueueSize()
        {
            transmission.checkGenericQueueSize();
        }

        /// <summary>
        /// Displays the amount of memory used by the server in bytes.
        /// </summary>
        public void usedMemory()
        {
            Console.WriteLine("Megabytes used by this application: " + (Process.GetCurrentProcess().PrivateMemorySize64 / 1048576) + "\n");
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
