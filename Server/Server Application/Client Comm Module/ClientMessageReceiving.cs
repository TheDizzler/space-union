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

        private Player player = null;

        /// <summary>
        /// Initiate the message receiver client.
        /// </summary>
        public ClientMessageReceiving()
        {
            messageQueue = new List<GameMessage>();

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
            while (true)
            {
                Console.WriteLine("-----------------A message was received-----------------");
                Data data = (Data)DataControl.receiveTCPData(TCPListener);
                if (data != null)
                    switch(data.Type) {
                        case Constants.CHAT_MESSAGE:
                            Console.WriteLine("-----------------A chat message was received-----------------");
                            Console.WriteLine(((GameMessage)data).Message);
                            messageQueue.Add((GameMessage)data);
                            break;
                            
                        case Constants.LOGIN_REQUEST:
                            Console.WriteLine("-----------------A login request was received-----------------");
                            Console.WriteLine(((Player)data).PortReceive + "AND" + ((Player)data).PortSend);
                            player = (Player)data;
                            gameStart((Player)data);
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
            if (player == null)
            {
                Console.WriteLine("-------player is null--------");
                return null;
            }
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
    }
}
