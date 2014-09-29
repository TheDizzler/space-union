using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace Server_Application
{
    class DataReceiving
    {
        public const int numberOfUdpClients = 6;
        public const int numberOfTcpListeners = 2;

        /// <summary>
        /// Listens to data from clients (1 for each)
        /// </summary>
        UdpClient[] clients = new UdpClient[numberOfUdpClients];

        /// <summary>
        /// listeners[0] for login requests.
        /// listeners[1] for chat messages. 

        /// </summary>
        TcpListener[] listeners = new TcpListener[numberOfTcpListeners];

        public DataReceiving()
        {
            // Initialize the UDP clients
            for (int x = 0; x < numberOfUdpClients; x++)
                clients[x] = new UdpClient(6964 + x);

            // Initialize the TCP clients.
            for (int x = 0; x < numberOfTcpListeners; x++) 
                listeners[x] = new TcpListener(IPAddress.Parse("0.0.0.0"), 6980 + x);

            // Begin running the UDP client listeners.
            for (int x = 0; x < numberOfUdpClients; x++)
                new Thread(receiveClientData).Start(clients[x]);

            new Thread(receiveLoginRequests).Start();
            new Thread(receiveChatMessages).Start();
        }

        public void receiveLoginRequests()
        {
            while (true)
            {
                Object loginData = DataControl.receiveTCPData(listeners[0]);

                // do whatever for the login requests
            }
        }

        public void receiveChatMessages()
        {
            while (true)
            {
                Object chatData = DataControl.receiveTCPData(listeners[1]);

                // do whatever for chat messages
            }
        }

        public void receiveClientData(Object udpClient)
        {
            while (true)
            {
                Object clientData = DataControl.receiveUDPData((UdpClient)udpClient);

                // do whatever to handle client data
            }
        }
    }
}
