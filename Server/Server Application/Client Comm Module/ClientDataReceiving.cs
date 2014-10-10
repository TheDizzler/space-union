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
    class ClientDataReceiving
    {
        private UdpClient UDPListener;

        private TcpListener TCPListener;

        private List<GameMessage> messageQueue;
        private List<GameData> dataQueue;

        private int assignedUDPPort_Listen;

        public ClientDataReceiving()
        {
            messageQueue = new List<GameMessage>();
            dataQueue = new List<GameData>();

            UDPListener = new UdpClient(assignedUDPPort_Listen);

            // NOTE: fix required to only listen to the server.
            TCPListener = new TcpListener(IPAddress.Parse("0.0.0.0"), Constants.TCPMessageClient);
            TCPListener.Start();

            new Thread(receiveMessages).Start();
            new Thread(receiveData).Start();
        }

        /// <summary>
        /// Assign a UDP port to receive data from.
        /// </summary>
        /// <param name="UDPPort">The UDP port to assign.</param>
        public void assignUDPPort_Listen(int UDPPort)
        {
            assignedUDPPort_Listen = UDPPort;
        }

        /// <summary>
        /// Begin receiving chat messages from the server.
        /// </summary>
        /*public void receiveChatMessages()
        {
            while (true)
            {
                Object chatData = DataControl.receiveTCPData(TCPListener);
                if (chatData != null)
                    messageQueue.Add((GameMessage)chatData);
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

                        case Constants.GAME_SETUP_MESSAGE:

                            break;
                    }
            }
        }

        /// <summary>
        /// Begin receiving game data from the server.
        /// </summary>
        public void receiveData()
        {
            while (true)
            {
                Object clientData = DataControl.receiveUDPData(UDPListener);
                if(clientData != null)
                    dataQueue.Add((GameData)clientData);
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

        /// <summary>
        /// Gets the oldest data from the data queue.
        /// </summary>
        /// <returns>The oldest data in the queue.</returns>
        public GameData getGameData()
        {
            if (dataQueue.Count == 0)
                return null;
            GameData data = dataQueue.ElementAt(0);
            dataQueue.RemoveAt(0);
            return data;
        }
    }
}
