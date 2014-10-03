using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient client = new UdpClient(6969);
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = client.Receive(ref ip);
            
    }
}
