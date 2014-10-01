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
    /// <summary>
    /// This is the class responsible for receiving data from clients. It will
    /// route this information to other classes like DataTransmission or 
    /// DatabaseRequests to either send back to the client or make a request
    /// from the database.
    /// </summary>
    class DataReceiving
    {
        /// <summary>
        /// Listens to data from clients (1 for each client)
        /// </summary>
<<<<<<< HEAD
        UdpClient[] UDPListeners = new UdpClient[DataControl.numberOfUdpClients];
=======
        UdpClient[] UDPListeners = new UdpClient[DataControl.NumberOfUdpClients];
>>>>>>> 9f26a3b6bfaf5b8147258a00afa8def71fd60b60

        /// <summary>
        /// listeners[0] for login requests.
        /// listeners[1] for chat messages. 
        /// </summary>
<<<<<<< HEAD
        TcpListener[] TCPListeners = new TcpListener[DataControl.numberOfTcpClients];
=======
        TcpListener[] TCPListeners = new TcpListener[DataControl.NumberOfTcpClients];
>>>>>>> 9f26a3b6bfaf5b8147258a00afa8def71fd60b60

        public DataReceiving()
        {
            // Initialize the UDP clients
<<<<<<< HEAD
            for (int x = 0; x < DataControl.numberOfUdpClients; x++)
                UDPListeners[x] = new UdpClient(6964 + x);

            // Initialize the TCP clients.
            for (int x = 0; x < DataControl.numberOfTcpClients; x++) 
                TCPListeners[x] = new TcpListener(IPAddress.Parse("0.0.0.0"), 6980 + x);

            // Begin running the UDP client listeners.
            for (int x = 0; x < DataControl.numberOfUdpClients; x++)
=======
            for (int x = 0; x < DataControl.NumberOfUdpClients; x++)
                UDPListeners[x] = new UdpClient(6964 + x);

            // Initialize the TCP clients.
            for (int x = 0; x < DataControl.NumberOfTcpClients; x++) 
                TCPListeners[x] = new TcpListener(IPAddress.Parse("0.0.0.0"), 6980 + x);

            // Begin running the UDP client listeners.
            for (int x = 0; x < DataControl.NumberOfUdpClients; x++)
>>>>>>> 9f26a3b6bfaf5b8147258a00afa8def71fd60b60
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
