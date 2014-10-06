using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Data_Structures;
using System.Threading;
using System.Threading.Tasks;
using Data_Manipulation;

namespace Client_Comm_Module
{
    class ClientDataTransmission
    {
        private List<GameData> dataQueue;
        private List<GameMessage> messageQueue;

        private UdpClient UDPClient;
        private TcpClient TCPClient;

        private int assignedUDPPort;

        public ClientDataTransmission()
        {
            setup();
        }

        public void assignUDPPort(int UDPPort)
        {
            assignedUDPPort = UDPPort;
        }

        /// <summary>
        /// Add the given message to the appropriate queue.
        /// </summary>
        /// <param name="message">The message to add to a queue.</param>
        public void addMessageToQueue(Data message)
        {
            if (message == null)
                return;
            try
            {
                switch (message.Type)
                {
                    case Constants.GAME_DATA:

                        dataQueue.Add((GameData)message);
                        break;
                    case Constants.CHAT_MESSAGE:
                        messageQueue.Add((GameMessage)message);
                        break;
                }
            }
            catch (InvalidCastException e) { Console.WriteLine(e.ToString()); return; }
        }

        private void setup()
        {
            UDPClient = new UdpClient(ClientConstants.UDP_PORT_SEND);

            TCPClient = new TcpClient();

            // Initialize the lists.
            dataQueue = new List<GameData>();
            messageQueue = new List<GameMessage>();

            try
            {
                new Thread(sendChatMessages).Start();
                new Thread(sendGameData).Start();
            }
            catch (ThreadStateException e) { Console.WriteLine("Client has crashed." + e.ToString()); return; }
            catch (OutOfMemoryException e) { Console.WriteLine("Client has crashed." + e.ToString()); return; }
            catch (InvalidOperationException e) { Console.WriteLine("Client has crashed." + e.ToString()); return; }
        }

        /// <summary>
        /// Sends a chat message to the server.
        /// </summary>
        private void sendChatMessages()
        {
            while (true)
            {
                GameMessage message = (GameMessage)removeFromQueue(Constants.CHAT_MESSAGE);
                if (message == null)
                {
                    Thread.Sleep(5);
                    continue;
                }
                DataControl.sendTCPData(TCPClient, message, ClientConstants.SERVER_IPADDRESS, Constants.TCPMessageClient);
            }
        }

        /// <summary>
        /// Sends a game data to the server.
        /// </summary>
        private void sendGameData()
        {
            while (true)
            {
                GameData data = (GameData)removeFromQueue(Constants.GAME_DATA);
                if (data == null)
                {
                    Thread.Sleep(5);
                    continue;
                }
                DataControl.sendUDPData(UDPClient, data, ClientConstants.SERVER_IPADDRESS, assignedUDPPort);
            }
        }

        /// <summary>
        /// Gets the oldest message from a queue based on the type of message.
        /// </summary>
        /// <param name="type">The type of message to get.</param>
        /// <returns>The oldest message in the queue.</returns>
        private Data removeFromQueue(byte type)
        {
            switch (type)
            {
                case Constants.CHAT_MESSAGE:
                    return removeMessageFromQueue();
                case Constants.ERROR_MESSAGE:
                    return removeDataFromQueue();
            }
            return null;
        }

        /// <summary>
        /// Returns the oldest chat message in the queue.
        /// </summary>
        /// <returns>The oldest message awaiting transfer.</returns>
        private GameMessage removeMessageFromQueue()
        {
            if (messageQueue.Count == 0)
                return null;
            GameMessage message = messageQueue.ElementAt(0);
            messageQueue.RemoveAt(0);
            return message;
        }

        /// <summary>
        /// Returns the oldest game data in the queue.
        /// </summary>
        /// <returns>The oldest message awaiting transfer.</returns>
        private GameData removeDataFromQueue()
        {
            if (dataQueue.Count == 0)
                return null;
            GameData data = dataQueue.ElementAt(0);
            dataQueue.RemoveAt(0);
            return data;
        }
    }
}
