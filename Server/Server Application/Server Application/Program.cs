﻿using System;
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
    class Program
    {
        static void Main(string[] args)
        {   
            menuSetup();
            while (true)
            {
                menuSetup();
                string input = Console.ReadLine();
                switch (input)
                {
                    case "help":
                        helpMenu();
                        break;
                    case "ip":
                        getLocalIPv4Address();
                        break;
                    case "exit":
                        return;
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
            Console.WriteLine("exit - Shuts down the server.");
            Console.ReadLine();
        }

        /// <summary>
        /// Gets the current IP address.
        /// </summary>
        /// <returns>Returns the current IP address.</returns>
        private static void getLocalIPv4Address()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
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