using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Server_Application
{
    /// <summary>
    /// The main server application. The main thread of the application handles
    /// the input from an administrator at the server console. The setup for the
    /// server and all the processing of incoming communication requests
    /// are handled on other threads arnd are started up by the method
    /// serverSetup() before the primary while loop.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            Console.Title = "Space Union Server";
            Console.WriteLine("Enter the phrase \"help\" at any moment to display a list of commands.\n");
            while (true)
            {
                string input = Console.ReadLine();
                switch (input)
                {
                    case "help":
                        helpMenu();
                        break;
                    case "ip":
                        getLocalIPv4Address();
                        break;
                    case "threads":
                        threadsRunning();
                        break;
                    case "rooms":
                        server.getNumberOfRooms();
                        break;
                    case "players":
                        server.getNumberOfOnlinePlayers();
                        break;
                    case "searching":
                        server.getNumberOfSearchingPlayers();
                        break;
                    case "error":
                        server.checkErrorQueueSize();
                        break;
                    case "chat":
                        server.checkChatMessageQueueSize();
                        break;
                    case "login":
                        server.checkLoginRequestQueueSize();
                        break;
                    case "data":
                        server.checkGameDataQueueSize();
                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
            }
        }

        /// <summary>
        /// Displays the amount of memory used by the server in bytes.
        /// </summary>
        private static void usedMemory()
        {
            Console.WriteLine("Bytes used by this application: " + Process.GetCurrentProcess().PrivateMemorySize64 + "\n");
        }

        /// <summary>
        /// Displays the number of threads running in the application.
        /// </summary>
        private static void threadsRunning()
        {
            Console.WriteLine("Number of threads running: " + Process.GetCurrentProcess().Threads.Count + "\n");
        }

        /// <summary>
        /// Displays a list of commands available to the server.
        /// </summary>
        private static void helpMenu()
        {
            Console.WriteLine("games - Displays the number of currently running games.");
            Console.WriteLine("players - Displays the number of currently active players.");
            Console.WriteLine("threads - Displays the number of currently running threads.");
            Console.WriteLine("ip - Displays the current IP address.");
            Console.WriteLine("rooms - Displays the number of active game rooms.");
            Console.WriteLine("players - Displays the number of players online.");
            Console.WriteLine("searching - Displays the number of players looking for a game.");
            Console.WriteLine("error - Displays the size of the error queue.");
            Console.WriteLine("chat - Displays the size of the chat message queue.");
            Console.WriteLine("login - Displays the size of the login queue.");
            Console.WriteLine("data - Displays the size of the game data queue.");
            Console.WriteLine("clear - Clears the console screen.");
            Console.WriteLine("exit - Shuts down the server." + "\n");
        }

        /// <summary>
        /// Gets the current IP address.
        /// </summary>
        /// <returns>Returns the current IP address.</returns>
        private static void getLocalIPv4Address()
        {
            IPHostEntry host = null;
            try
            {
                host = Dns.GetHostEntry(Dns.GetHostName());
            }
            catch (ArgumentNullException e) { Console.WriteLine(e.ToString()); return; }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine(e.ToString()); return; }
            catch (ArgumentException e) { Console.WriteLine(e.ToString()); return; }
            catch (SocketException e) { Console.WriteLine(e.ToString()); return; }
            foreach (IPAddress ipv4 in host.AddressList)
            {
                if (ipv4.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine("Current IP address is: " + ipv4.ToString() + "\n");
                }
            }
        }
    }
}
