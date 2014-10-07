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
using TestLib;

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
            while (true)
            {
                //menuSetup();
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
                    case "clear":
                        menuSetup();
                        break;
                    case "exit":
                        break;
                    default:
                        menuSetup();
                        break;
                }
            }
        }

        /// <summary>
        /// Sets up all the necessary threads for the operation of the server.
        /// </summary>
        private static void serverSetup()
        {

        }

        /// <summary>
        /// Clears the console window and initializes it.
        /// </summary>
        private static void menuSetup()
        {
            Console.Clear();
            Console.Title = "Space Union Server";
            Console.WriteLine("Enter the phrase \"help\" at any moment to display a list of commands.\n");
        }

        /// <summary>
        /// Displays the amount of memory used by the server in bytes.
        /// </summary>
        private static void usedMemory()
        {
            Console.WriteLine("Bytes used by this application: " + Process.GetCurrentProcess().PrivateMemorySize64);
            Console.ReadLine();
        }

        /// <summary>
        /// Displays the number of threads running in the application.
        /// </summary>
        private static void threadsRunning()
        {
            Console.WriteLine("Number of threads running: " + Process.GetCurrentProcess().Threads.Count);
            Console.ReadLine();
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
            Console.WriteLine("clear - Clears the console screen.");
            Console.WriteLine("exit - Shuts down the server.");
            Console.ReadLine();
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
            catch (ArgumentNullException e) { Console.WriteLine("Method: getLocalIPv$Address()\n" + e.ToString()); return; }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("Method: getLocalIPv$Address()\n" + e.ToString()); return; }
            catch (ArgumentException e) { Console.WriteLine("Method: getLocalIPv$Address()\n" + e.ToString()); return; }
            catch (SocketException e) { Console.WriteLine("Method: getLocalIPv$Address()\n" + e.ToString()); return; }

            foreach (IPAddress ipv4 in host.AddressList)
            {
                if (ipv4.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine("Current IP address is: " + ipv4.ToString());
                    Console.ReadLine();
                }
            }
        }
    }
}
