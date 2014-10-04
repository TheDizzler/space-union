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
        UdpClient[] UDPListeners = new UdpClient[Constants.NumberOfUdpClients];

        /// <summary>
        /// listeners[0] for login requests.
        /// listeners[1] for chat messages. 
        /// </summary>
        TcpListener[] TCPListeners = new TcpListener[Constants.NumberOfTcpClients];

        Object ownerLock = new Object();
        Server owner;

        public DataReceiving(Server owner)
        {
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
                UDPListeners[x] = new UdpClient(Constants.UDPInPortOne + x);
            for (int x = 0; x < Constants.NumberOfTcpClients; x++) 
                TCPListeners[x] = new TcpListener(IPAddress.Parse("0.0.0.0"), 6980 + x);
            for (int x = 0; x < Constants.NumberOfTcpClients; x++)
                TCPListeners[x].Start();
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
                new Thread(receiveClientData).Start(UDPListeners[x]);

            this.owner = owner;

            new Thread(receiveLoginRequests).Start();
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
                Object loginData = DataControl.receiveTCPData(TCPListeners[0]);
                new Thread(unused => LoginRequests.handleLoginRequest(loginData, owner)).Start();
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
                lock (ownerLock)
                {
                    owner.addMessageToQueue((GameMessage)chatData);
                }
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
                lock (ownerLock)
                {
                    owner.addMessageToQueue((GameData)clientData);
                }
            }
        }
    }
}
