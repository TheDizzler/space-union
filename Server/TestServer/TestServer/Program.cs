using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
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
            receiverip = "192.168.1.102";
            UdpClient client = new UdpClient();
            try
            {
                byte[] data;
                
                short x = 0;
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));
                while (x != 1000)
                {
                    data = ObjectToBytes(new Datagram() { Health = x });
                    client.Send(data, data.Length, receiverip, 6969);
                    x++;
                    Thread.Sleep(5);
                }
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss:fff"));
                /*
                Console.WriteLine("Receive data: ");
                if (Console.ReadLine() == "y")
                {
                    IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);
                    byte[] input = client.Receive(ref ip);
                    Datagram received = (Datagram)BytesToObject(input);
                    Console.WriteLine("Received: " + received.Health);
                }
                */
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

        private static Object BytesToObject(byte[] target)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(target, 0, target.Length);
                ms.Seek(0, SeekOrigin.Begin);
                Object data = (Object)bf.Deserialize(ms);
                return data;
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
