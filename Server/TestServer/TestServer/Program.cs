using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    class Program
    {
        static string receiverip;
        static void Main(string[] args)
        {
            Console.WriteLine("Current IP: " + GetLocalIPv4Address().ToString());
            Console.WriteLine("Size of Datagram: " + ObjectToBytes(new Datagram()).Length);
            receiverip = "ip";
            UdpClient client = new UdpClient(6969);
            try
            {
                byte[] data;
                client.Connect(receiverip, 6969);
                data = ObjectToBytes(new Datagram() { Health = 69 });
                client.Send(data, data.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private static byte[] ObjectToBytes(Object target)
        {
            if (target == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, target);
                return ms.ToArray();
            }
        }

        private static IPAddress GetLocalIPv4Address()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ipv4 in host.AddressList)
            {
                if (ipv4.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipv4;
                }
            }
            return null;
        }
    }
}
