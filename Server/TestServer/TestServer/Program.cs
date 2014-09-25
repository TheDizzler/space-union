using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
                IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);
                List<int> average = new List<int>();
                for (int n = 0; n < 100; n++)
                {
                    DateTime time = DateTime.Now;
                    short x = 0;
                    List<short> temp = new List<short>();
                    while (x != 1000)
                    {
                        byte[] input = client.Receive(ref ip);
                        if (x == 0)
                            time = DateTime.Now;
                        //short c = ((Datagram)BytesToObject(Decompress(input))).Health;
                        short c = ((Datagram)BytesToObject(input)).Health;
                        temp.Add(c);
                        if (c == 999)
                            break;
                        //Datagram received = (Datagram)BytesToObject(input);
                        x++;
                    }
                    Console.WriteLine(DateTime.Now - time);
                    Console.WriteLine("Transmission: " + n + " - List size: " + temp.Count);
                    average.Add(temp.Count);
                    Console.WriteLine();
                }
                int total = 0;
                foreach (int x in average)
                {
                    total += x;
                }
                Console.WriteLine("Average packets received: " + total / average.Count);
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

        static byte[] Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream ms = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            ms.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return ms.ToArray();
                }

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
