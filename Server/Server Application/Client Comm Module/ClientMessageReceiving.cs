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
    class ClientMessageReceiving
    {
        public delegate void GameStartEventHandler(Player setupMessage);
        public event GameStartEventHandler gameStart;
        
        private TcpListener TCPListener;
        private List<GameMessage> messageQueue;

        private List<RoomInfo> roomInfoQueue;
        private List<RoomList> roomListQueue;

        private Player player = null;

        /// <summary>
        /// Initiate the message receiver client.
        /// </summary>
        public ClientMessageReceiving()
        {
            messageQueue = new List<GameMessage>();
            roomInfoQueue = new List<RoomInfo>();
            roomListQueue = new List<RoomList>();

            // NOTE: fix required to only listen to the server.
            TCPListener = new TcpListener(IPAddress.Any, Constants.TCPLoginClient);
            TCPListener.Start();

            new Thread(receiveMessages).Start();
            //new Thread(receiveChatMessages).Start();
        }

        /// <summary>
        /// Waits for the server to send a login confirmation with
        /// the port assignments for this client.
        /// </summary>
        /*public Player receiveLoginConfirmation()
        {
            //TcpListener listener = new TcpListener(IPAddress.Any, Constants.TCPLoginClient);
            //listener.Start();
            return (Player)DataControl.receiveTCPData(TCPListener);
        }*/

        /// <summary>
        /// Begin receiving chat messages from the server.
        /// </summary>
        /*public void receiveChatMessages()
        {
            while (true)
            {
                GameMessage chatData = (GameMessage)DataControl.receiveTCPData(TCPListener);
                if (chatData != null)
                    messageQueue.Add(chatData);
            }
        }*/

        /// <summary>
        /// Begin receiving chat and setup messages from the server.
        /// </summary>
        public void receiveMessages()
        {
            Console.WriteLine("Receiving messages now.");
            while (true)
            {
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
                        gameStart((Player)data);
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
    }
}
