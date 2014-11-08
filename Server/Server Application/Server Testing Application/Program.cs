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
        static void Main(string[] args)
        {
            int threads = 4;
            UdpClient[] clients = new UdpClient[threads];
            for (int x = 0; x < threads; x++)
                clients[x] = new UdpClient(6940 + x);
            Console.ReadLine();
            for (int x = 0; x < threads; x++)
            {
                int y = x;
                new Thread(() => crush(clients[y], 6940 + y)).Start();
            }
            Console.WriteLine("Sending");
        }

        static void crush(UdpClient client, int port)
        {
            while (true)
            {
                GameData temp = new GameData();
                DataControl.sendUDPData(client, temp, "192.168.1.222", port);
            }
        }
    }
}
