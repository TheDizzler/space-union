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
        /// <summary>
        /// Listens to data from clients (1 for each client)
        /// </summary>
        UdpClient[] UDPListeners = new UdpClient[DataControl.numberOfUdpClients];

        /// <summary>
        /// listeners[0] for login requests.
        /// listeners[1] for chat messages. 
        /// </summary>
        TcpListener[] TCPListeners = new TcpListener[DataControl.numberOfTcpClients];

        public DataReceiving()
        {
            // Initialize the UDP clients
            for (int x = 0; x < DataControl.numberOfUdpClients; x++)
                UDPListeners[x] = new UdpClient(6964 + x);

            // Initialize the TCP clients.
            for (int x = 0; x < DataControl.numberOfTcpClients; x++) 
                TCPListeners[x] = new TcpListener(IPAddress.Parse("0.0.0.0"), 6980 + x);

            // Begin running the UDP client listeners.
            for (int x = 0; x < DataControl.numberOfUdpClients; x++)
                new Thread(receiveClientData).Start(UDPListeners[x]);

            new Thread(receiveLoginRequests).Start();
            new Thread(receiveChatMessages).Start();
        }

        public void receiveLoginRequests()
        {
            while (true)
            {
                Object loginData = DataControl.receiveTCPData(TCPListeners[0]);

                // do whatever for the login requests
            }
        }

        public void receiveChatMessages()
        {
            while (true)
            {
                Object chatData = DataControl.receiveTCPData(TCPListeners[1]);

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
