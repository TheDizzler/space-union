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
        public Player receiveLoginConfirmation()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, Constants.TCPLoginClient);
            return (Player)DataControl.receiveTCPData(listener);
        }

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
                Data data = (Data)DataControl.receiveTCPData(TCPListener);
                if (data != null)
                    switch(data.Type) {
                        case Constants.CHAT_MESSAGE:
                            messageQueue.Add((GameMessage)data);
                            break;

                        case Constants.LOGIN_REQUEST:
                            gameStart((Player)data);
                            break;
                    }
            }
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
