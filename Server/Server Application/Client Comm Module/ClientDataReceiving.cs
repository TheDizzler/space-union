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
            
            UDPListener = new UdpClient(assignedUDPPort_Listen);
            messageQueue = new List<GameMessage>();

            // NOTE: fix required to only listen to the server.
            TCPListener = new TcpListener(IPAddress.Parse("0.0.0.0"), Constants.TCPMessageClient);
            TCPListener.Start();

            new Thread(receiveChatMessages).Start();
            //new Thread(receiveData).Start();
        }

        /// <summary>
        /// Begin receiving chat messages from clients and 
        /// handle each received message in a separate thread.
        /// </summary>
        public void receiveChatMessages()
        {
            while (true)
            {
                Object chatData = DataControl.receiveTCPData(TCPListener);
                messageQueue.Add((GameMessage)chatData);
            }
        }

        /// <summary>
        /// Begin receiving game data from clients and
        /// handle each received game data in a separate thread.
        /// </summary>
        /// <param name="UDPListener"></param>
        /*public void receiveData(Object UDPListener)
        {
            while (true)
            {
                Object clientData = DataControl.receiveUDPData((UdpClient)UDPListener);
                dataQueue.Add((GameData)clientData);
            }
        }*/

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
