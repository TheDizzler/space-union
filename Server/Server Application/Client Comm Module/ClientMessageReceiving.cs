using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data_Structures;
using Data_Manipulation;

namespace Client_Comm_Module
{
    public class ClientMessageReceiving
    {
        public delegate void GameStartEventHandler();
        public delegate void GameEndEventHandler();

        public event GameStartEventHandler gameStart;
        public event GameEndEventHandler gameEnd;
        
        private TcpListener TCPListener;
        private List<GameMessage> messageQueue;
        private List<RoomInfo> roomInfoQueue;
        private List<RoomList> roomListQueue;
        private List<PlayerRequest> startSignalQueue;
        private Player player = null;

        private ClientCommHandler Owner;

        /// <summary>
        /// Initiate the message receiver client.
        /// </summary>
        public ClientMessageReceiving(ClientCommHandler owner)
        {
            messageQueue = new List<GameMessage>();
            roomInfoQueue = new List<RoomInfo>();
            roomListQueue = new List<RoomList>();
            startSignalQueue = new List<PlayerRequest>();

            Owner = owner;

            // NOTE: fix required to only listen to the server.
            TCPListener = new TcpListener(IPAddress.Any, Constants.TCPMessageClient);
            TCPListener.Start();

            new Thread(receiveMessages).Start();
        }

        /// <summary>
        /// Begin receiving chat and setup messages from the server.
        /// </summary>
        public void receiveMessages()
        {
            Console.WriteLine("Receiving messages now.");
            while (true)
            {
                Console.WriteLine("receiveMessages()");
                Data data = (Data)DataControl.receiveTCPData(TCPListener);
                if (data == null)
                    continue;
                Console.WriteLine("message was received");
                switch (data.Type)
                {
                    case Constants.LOGIN_REQUEST:
                        Console.WriteLine("-----------------LOGIN request received-----------------");
                        Console.WriteLine(((Player)data).PortReceive + "AND" + ((Player)data).PortSend);
                        player = (Player)data;
                        Owner.UDPPortReceive = ((Player)data).PortReceive;
                        Owner.UDPPortSend = ((Player)data).PortSend;
                        break;

                    case Constants.CHAT_MESSAGE:
                        Console.WriteLine("-----------------CHAT message received-----------------");
                        Console.WriteLine(((GameMessage)data).Message);
                        messageQueue.Add((GameMessage)data);
                        break;

                    case Constants.ROOM_LIST:
                        Console.WriteLine("-----------------ROOM LIST received-----------------");
                        roomListQueue.Add((RoomList)data);
                        break;

                    case Constants.ROOM_INFO:
                        Console.WriteLine("-----------------ROOM INFO received-----------------");
                        roomInfoQueue.Add((RoomInfo)data);
                        break;

                    case Constants.PLAYER_REQUEST:
                        handlePlayerRequestResponses((PlayerRequest)data);
                        break;
                }
            }
        }

        /// <summary>
        /// Return the current player using the client.
        /// </summary>
        /// <returns>The current player.</returns>
        public Player getPlayer()
        {
            return player;
        }

        /// <summary>
        /// Gets the oldest message from the message queue.
        /// </summary>
        /// <returns>The oldest message in the queue.</returns>
        public GameMessage getGameMessage()
        {
            if (messageQueue.Count == 0)
                return null;
            GameMessage message = messageQueue.ElementAt(0);
            messageQueue.RemoveAt(0);
            return message;
        }

        public RoomInfo getRoomInfo()
        {
            if (roomInfoQueue.Count == 0)
                return null;
            RoomInfo roomInfo = roomInfoQueue.ElementAt(0);
            roomInfoQueue.RemoveAt(0);
            return roomInfo;
        }

        public RoomList getRoomList()
        {
            if (roomListQueue.Count == 0)
                return null;
            RoomList roomList = roomListQueue.ElementAt(0);
            roomListQueue.RemoveAt(0);
            return roomList;
        }

        public PlayerRequest getStartSignal()
        {
            if (startSignalQueue.Count == 0)
                return null;
            PlayerRequest request = startSignalQueue.ElementAt(0);
            startSignalQueue.RemoveAt(0);
            return request;
        }

        private void handlePlayerRequestResponses(PlayerRequest response)
        {
            switch (response.RequestType)
            {
                case Constants.PLAYER_REQUEST_START:
                    Console.WriteLine("-----------------GAME START reveived-----------------");
                    gameStart();
                    startSignalQueue.Add(response);
                    break;
                case Constants.PLAYER_REQUEST_END:
                    Console.WriteLine("-----------------GAME END reveived-----------------");
                    Owner.endGame();
                    gameEnd();
                    break;
            }
        }
    }
}
