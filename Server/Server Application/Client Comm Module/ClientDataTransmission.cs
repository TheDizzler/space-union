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
        private List<GameData> UDPQueue;
        private List<GameMessage> messages;

        UdpClient UDPClients;
        TcpClient TCPClient;

        public ClientDataTransmission()
        {

        }

        private void setup()
        {
            UDPClients = new UdpClient(7000);

            TCPClient = new TcpClient();

            // Initialize the lists.
            UDPQueue = new List<GameData>();
            messages = new List<GameMessage>();

            try
            {
                new Thread(sendChatMessages).Start();

                new Thread(sendLoginValidationMessage).Start();
                new Thread(sendErrorMessage).Start();
            }
            catch (ThreadStateException e) { Console.WriteLine("Client has crashed." + e.ToString()); return; }
            catch (OutOfMemoryException e) { Console.WriteLine("Client has crashed." + e.ToString()); return; }
            catch (InvalidOperationException e) { Console.WriteLine("Client has crashed." + e.ToString()); return; }
        }

        /// <summary>
        /// Sends a chat message to every player in the game room.
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
                    return removeErrorMessageFromQueue();
            }
            return null;
        }

        /// <summary>
        /// Removes an item from the GameData queue.
        /// </summary>
        /// <param name="queue">The queue from which to remove.</param>
        /// <returns></returns>
        private GameData getGameData(byte queue)
        {
            if (UDPQueue[queue].Count == 0)
                return null;
            GameData data = UDPQueue[queue].ElementAt(0);
            UDPQueue[queue].RemoveAt(0);
            return data;
        }

        /// <summary>
        /// Returns the oldest chat message in the queue.
        /// </summary>
        /// <returns>The oldest message awaiting transfer.</returns>
        private GameMessage removeMessageFromQueue()
        {
            if (messages.Count == 0)
                return null;
            GameMessage message = messages.ElementAt(0);
            messages.RemoveAt(0);
            return message;
        }
    }
}
