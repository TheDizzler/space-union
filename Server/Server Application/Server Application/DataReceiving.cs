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
        UdpClient[] UDPListeners = new UdpClient[DataControl.NumberOfUdpClients];

        /// <summary>
        /// listeners[0] for login requests.
        /// listeners[1] for chat messages. 
        /// </summary>
        TcpListener[] TCPListeners = new TcpListener[DataControl.NumberOfTcpClients];

        public DataReceiving()
        {
            // Initialize the UDP clients
            for (int x = 0; x < DataControl.NumberOfUdpClients; x++)
                UDPListeners[x] = new UdpClient(6964 + x);

            // Initialize the TCP clients.
            for (int x = 0; x < DataControl.NumberOfTcpClients; x++) 
                TCPListeners[x] = new TcpListener(IPAddress.Parse("0.0.0.0"), 6980 + x);

            // Begin running the UDP client listeners.
            for (int x = 0; x < DataControl.NumberOfUdpClients; x++)
                new Thread(receiveClientData).Start(UDPListeners[x]);

            // Begin running the TCP login request listener.
            new Thread(receiveLoginRequests).Start();

            // Begin running the TCP chat message listener.
            new Thread(receiveChatMessages).Start();
        }

        /// <summary>
        /// Begin receiving login requests from clients and
        /// handle each request in a separate thread.
        /// </summary>
        public void receiveLoginRequests()
        {
            while (true)
            {
                // The received login data from a client.
                Object loginData = DataControl.receiveTCPData(TCPListeners[0]);

                // Handle the login request in a thread.
                new Thread(LoginRequests.handleLoginRequest).Start(loginData);
            }
        }

        /// <summary>
        /// Begin receiving chat messages from clients and 
        /// handle each received message in a separate thread.
        /// </summary>
        public void receiveChatMessages()
        {
            while (true)
            {
                Object chatData = DataControl.receiveTCPData(TCPListeners[1]);

                // do whatever for chat messages
            }
        }

        /// <summary>
        /// Begin receiving game data from clients and
        /// handle each received game data in a separate thread.
        /// </summary>
        /// <param name="UDPListener"></param>
        public void receiveClientData(Object UDPListener)
        {
            while (true)
            {
                Object clientData = DataControl.receiveUDPData((UdpClient)UDPListener);

                // do whatever to handle client data
            }
        }
    }
}
