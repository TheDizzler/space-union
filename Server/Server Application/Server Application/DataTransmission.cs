using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    /// <summary>
    /// This class is responsible for transmitting data to clients.
    /// Whether updating a player's position or the chat log or simply
    /// allowing them to log into the game.
    /// </summary>
    class DataTransmission
    {
        UdpClient[] UDPClients = new UdpClient[DataControl.NumberOfUdpClients];
        TcpClient[] TCPClients = new TcpClient[DataControl.NumberOfTcpClients];

        public DataTransmission()
        {
            // Initialize the UDP clients
            for (int x = 0; x < DataControl.NumberOfUdpClients; x++)
                UDPClients[x] = new UdpClient(6964 + x);

            // Initialize the TCP clients.
            for (int x = 0; x < DataControl.NumberOfTcpClients; x++)
                TCPClients[x] = new TcpClient();
        }
    }
}
