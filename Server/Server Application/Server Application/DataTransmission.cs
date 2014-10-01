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
        //int port = ((IPEndPoint)socket.Client.LocalEndPoint).Port;
        UdpClient[] UDPClients = new UdpClient[DataControl.NumberOfUdpClients];
        TcpClient[] TCPClients = new TcpClient[DataControl.NumberOfTcpClients];

        public DataTransmission()
        {
            for (int x = 0; x < DataControl.NumberOfUdpClients; x++)
                UDPClients[x] = new UdpClient(6944 + x);
            for (int x = 0; x < DataControl.NumberOfTcpClients; x++)
                TCPClients[x] = new TcpClient();
        }

        public void respondToLoginRequest()
        {

        }

        public void sendChatMessages()
        {

        }

        public void sendClientData()
        {

        }
    }
}
