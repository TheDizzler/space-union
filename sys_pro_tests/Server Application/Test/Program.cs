using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TestLib;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "192.168.229.1";
            TcpListener receiver = new TcpListener(IPAddress.Parse("0.0.0.0"), 6990);
            TcpClient client = new TcpClient();
            GameMessage message = new GameMessage(2);
            receiver.Start();
            message.Username = "hello";
            Console.WriteLine(getLocalIPv4Address());
            message.IPAddress = getLocalIPv4Address();
            DataControl.sendTCPData(client, message, ip, 6981);
            GameMessage incoming = null;
            incoming = (GameMessage)DataControl.receiveTCPData(receiver);
            Console.WriteLine(incoming.Username);
        }

        private static string getLocalIPv4Address()
        {
            IPHostEntry host = null;
            try
            {
                host = Dns.GetHostEntry(Dns.GetHostName());
            }
            catch (ArgumentNullException e) { Console.WriteLine("Method: getLocalIPv$Address()\n" + e.ToString()); return null; }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("Method: getLocalIPv$Address()\n" + e.ToString()); return null; }
            catch (ArgumentException e) { Console.WriteLine("Method: getLocalIPv$Address()\n" + e.ToString()); return null; }
            catch (SocketException e) { Console.WriteLine("Method: getLocalIPv$Address()\n" + e.ToString()); return null; }

            foreach (IPAddress ipv4 in host.AddressList)
            {
                if (ipv4.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipv4.ToString();
                }
            }
            return null;
        }
    }
}
