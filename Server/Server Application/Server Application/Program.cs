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
    /// are handled on other threads.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Space Union Server";
            Console.WriteLine("Enter the phrase \"help\" at any moment to display a list of commands.\n");
            Server server = new Server();
            ServerAnalyzer analyzer = new ServerAnalyzer(server.Transmission, server.Gamerooms, server.OnlinePlayers);
            
            while (true)
            {
                string input = Console.ReadLine();
                switch (input)
                {
                    case "help":
                        helpMenu();
                        break;
                    case "ip":
                        Console.WriteLine(getLocalIPv4Address());
                        break;
                    case "threads":
                        analyzer.threadsRunning();
                        break;
                    case "rooms":
                        analyzer.getNumberOfRooms();
                        break;
                    case "players":
                        analyzer.getNumberOfOnlinePlayers();
                        break;
                    case "error":
                        analyzer.checkGenericQueueSize();
                        break;
                    case "chat":
                        analyzer.checkChatMessageQueueSize();
                        break;
                    case "login":
                        analyzer.checkGenericQueueSize();
                        break;
                    case "ports":
                        analyzer.getReceivingPorts();
                        break;
                    case "memory":
                        analyzer.usedMemory();
                        break;
                    case "requests":
                        server.getRequests();
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
            Console.WriteLine("memory - Gets the amount of memory used by this program.");
            Console.WriteLine("clear - Clears the console screen.");
            Console.WriteLine("exit - Shuts down the server." + "\n");
        }

        /// <summary>
        /// Gets the current IP address.
        /// </summary>
        /// <returns>Returns the current IP address.</returns>
        private static string getLocalIPv4Address()
        {
            IPHostEntry host = null;
            try
            {
                host = Dns.GetHostEntry(Dns.GetHostName());
            }
            catch (ArgumentNullException e) { Console.WriteLine(e.ToString()); return null; }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine(e.ToString()); return null; }
            catch (ArgumentException e) { Console.WriteLine(e.ToString()); return null; }
            catch (SocketException e) { Console.WriteLine(e.ToString()); return null; }
            foreach (IPAddress ipv4 in host.AddressList)
                if (ipv4.AddressFamily == AddressFamily.InterNetwork)
                    return ipv4.ToString();
            return null;
        }
    }
}
