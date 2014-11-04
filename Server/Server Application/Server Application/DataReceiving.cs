using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using Data_Structures;
using Data_Manipulation;

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
        /// The Server class which initiates this class.
        /// </summary>
        Server owner;
        /// <summary>
        /// Listens to data from clients (1 for each client)
        /// </summary>
        UdpClient[] UDPListeners = new UdpClient[Constants.NumberOfUdpClients];
        /// <summary>
        /// listeners[0] for login requests.
        /// listeners[1] for chat messages. 
        /// </summary>
        TcpListener[] TCPListeners = new TcpListener[Constants.NumberOfTcpClients];

        public DataReceiving(Server owner)
        {
            this.owner = owner;
            setup();
        }

        /// <summary>
        /// Sets up the server by starting all of its main components in new threads.
        /// </summary>
        private void setup()
        {
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
                UDPListeners[x] = new UdpClient(Constants.UDPClientToServerPort + x);
            //Number of TCP clients - 1 because it created an error listener which wasn't being used.
            for (int x = 0; x < Constants.NumberOfTcpClients - 2; x++)
                TCPListeners[x] = new TcpListener(IPAddress.Parse("0.0.0.0"), Constants.TCPLoginListener + x);
            for (int x = 0; x < Constants.NumberOfTcpClients - 2; x++)
                TCPListeners[x].Start();
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
                new Thread(receiveClientData).Start(UDPListeners[x]);
            new Thread(receiveLoginRequests).Start();
        }

        /// <summary>
        /// Begin receiving login requests from clients and
        /// handle each request in a separate thread.
        /// </summary>
        public void receiveLoginRequests()
        {
            while (true)
            {
                Data message = (Player)DataControl.receiveTCPData(TCPListeners[0]);
                if (message == null)
                    return;

                switch (message.Type)
                {
                    case Constants.LOGIN_REQUEST:
                        Console.WriteLine(((Player)message).Username);
                        new Thread(unused => LoginRequests.handleLoginRequest((Player)message, owner)).Start();
                        break;

                    case Constants.PLAYER_REQUEST:
                        
                        break;

                    default:
                        owner.addMessageToQueue((GameMessage)message);
                        break;
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
                GameData clientData = (GameData)DataControl.receiveUDPData((UdpClient)UDPListener);
                new Thread(() => owner.updatePlayer(clientData)).Start();
            }
        }

        private void handlePlayerRequest(PlayerRequest request)
        {

        }
    }
}
