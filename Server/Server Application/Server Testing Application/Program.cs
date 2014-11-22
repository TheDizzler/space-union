using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data_Manipulation;
using Data_Structures;

namespace Server_Testing_Application
{
    class Program
    {
        static bool finished = false;
        static void Main(string[] args)
        {
            int threads = 4;
            UdpClient[] clients = new UdpClient[threads];
            for (int x = 0; x < threads; x++)
                clients[x] = new UdpClient(6940 + x);
            Console.WriteLine("Sending");
            DateTime now = DateTime.Now;
            for (int x = 0; x < threads; x++)
            {
                int y = x;
                new Thread(() => crush(clients[y], 6940 + y)).Start();
            }
            string t = Console.ReadLine();
            if (t == "exit")
                finished = true;
            Console.WriteLine("Time sending data: " + (DateTime.Now - now));
            Console.ReadLine();
        }

        static void crush(UdpClient client, int port)
        {
            long transmissions = 0;
            DateTime now = DateTime.Now;
            while (!finished)
            {
                GameData temp = new GameData();
                temp.Player = new Player();
                DataControl.sendUDPData(client, temp, "192.168.1.222", port);
                transmissions++;
            }
            Console.WriteLine("Port: " + port + ". Data sent: " + transmissions + ". Time: " + (DateTime.Now - now));
        }
    }
}
