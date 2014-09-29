using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    class DataReceiving
    {
        UdpClient[] clients = new UdpClient[6];
        public DataReceiving()
        {
            for (int x = 0; x < 6; x++)
                clients[x] = new UdpClient(6964 + x);
        }
    }
}
