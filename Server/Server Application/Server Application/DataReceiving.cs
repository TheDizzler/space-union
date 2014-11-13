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
        /// TCP listener, used to listen to message and login requests.
        /// </summary>
        TcpListener TCPListener = new TcpListener(IPAddress.Parse("0.0.0.0"), Constants.TCPMessageListener);

        LoginRequests login;

        public DataReceiving(Server owner)
        {
            this.owner = owner;
            login = new LoginRequests();
            setup();
        }

        /// <summary>
        /// Sets up the server by starting all of its main components in new threads.
        /// </summary>
        private void setup()
        {
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
                UDPListeners[x] = new UdpClient(Constants.UDPClientToServerPort + x);
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
                new Thread(receiveClientData).Start(UDPListeners[x]);
            TCPListener.Start();
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
                Data message = (Data)DataControl.receiveTCPData(TCPListener);
                if (message == null)
                    continue;
                switch (message.Type)
                {
                    case Constants.LOGIN_REQUEST:
                        {
                            new Thread(() => login.handleLoginRequest((Player)message, owner)).Start();
                            Console.WriteLine("received login requests");
                        }
                        break;
                    case Constants.PLAYER_REQUEST:
                        //Console.WriteLine("received player request");
                        new Thread(() => handlePlayerRequest((PlayerRequest)message)).Start();
                        break;
                    case Constants.CHAT_MESSAGE:
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
                owner.updatePlayer(clientData);
            }
        }

        /// <summary>
        /// Handle the given player request.
        /// </summary>
        /// <param name="request">The player request to handle.</param>
        private void handlePlayerRequest(PlayerRequest request)
        {
            switch (request.RequestType)
            {
                case Constants.PLAYER_REQUEST_ROOMLIST:
                    owner.sendRoomList(request.Sender);
                    break;
                case Constants.PLAYER_REQUEST_ROOMCREATE:
                    owner.createPlayerRequestedRoom(request.Sender, request.RoomName);
                    break;
                case Constants.PLAYER_REQUEST_ROOMJOIN: 
                    owner.addPlayerToRequestedRoom(request.Sender, request.RoomNumber);
                    break;
                case Constants.PLAYER_REQUEST_ROOMEXIT:
                    owner.removePlayerFromRoom(request.Sender, request.RoomNumber);
                    break;
                case Constants.PLAYER_REQUEST_ROOMINFO:
                    owner.sendRoomInfo(request.Sender, request.RoomNumber);
                    break;
                case Constants.PLAYER_REQUEST_LOGOUT:
                    owner.handleLogout(request.Sender);
                    break;
                case Constants.PLAYER_REQUEST_READY:
                    owner.updatePlayerReadyStatus(request.Sender, request.RoomNumber);
                    break;
                case Constants.PLAYER_REQUEST_HEARTBEAT:
                    owner.updateOnHeartbeat(request);
                    break;
                case Constants.PLAYER_REQUEST_SHIP:
                    owner.updatePlayerShipChoice(request.Sender, request.RoomNumber);
                    break;
                case Constants.PLAYER_REQUESTS_START:
                    owner.startGame(request.Sender, request.RoomNumber);
                    break;
            }
        }
    }
}
