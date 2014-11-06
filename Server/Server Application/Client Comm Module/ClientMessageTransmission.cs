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
    class ClientMessageTransmission
    {
        private List<Data> messageQueue;

        private TcpClient TCPClient;

        public ClientMessageTransmission()
        {
            setup();
        }

        /// <summary>
        /// Initiate the message transmission client.
        /// </summary>
        private void setup()
        {
            TCPClient = new TcpClient();
            messageQueue = new List<Data>();

            try
            {
                new Thread(sendMessages).Start();
            }
            catch (ThreadStateException e) { Console.WriteLine("Client has crashed." + e.ToString()); return; }
            catch (OutOfMemoryException e) { Console.WriteLine("Client has crashed." + e.ToString()); return; }
            catch (InvalidOperationException e) { Console.WriteLine("Client has crashed." + e.ToString()); return; }
        }

        /// <summary>
        /// Send a login request to the server.
        /// </summary>
        /// <param name="playerData">Player data containing player information.</param>
        public void sendLoginRequest(Player playerData)
        {
            DataControl.sendTCPData(TCPClient, playerData, ClientConstants.SERVER_IPADDRESS, ClientConstants.TCPLoginListener);
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
                messageQueue.Add(message);
            }
            catch (InvalidCastException e) { Console.WriteLine(e.ToString()); return; }
        }

        /// <summary>
        /// Sends a chat message to the server.
        /// </summary>
        private void sendMessages()
        {
            while (true)
            {
                Data message = removeMessageFromQueue();
                if (message == null)
                {
                    Thread.Sleep(ClientConstants.MESSAGE_SEND_INTERVAL);
                    continue;
                }
                DataControl.sendTCPData(TCPClient, message, ClientConstants.SERVER_IPADDRESS, ClientConstants.TCPLoginListener);
            }
        }

        /// <summary>
        /// Gets the oldest message from a queue based on the type of message.
        /// </summary>
        /// <param name="type">The type of message to get.</param>
        /// <returns>The oldest message in the queue.</returns>
        /*private Data removeFromQueue(byte type)
        {
            switch (type)
            {
                case Constants.CHAT_MESSAGE:
                    return removeMessageFromQueue();
            }
            return null;
        }*/

        /// <summary>
        /// Returns the oldest chat message in the queue.
        /// </summary>
        /// <returns>The oldest message awaiting transfer.</returns>
        private Data removeMessageFromQueue()
        {
            if (messageQueue.Count == 0)
                return null;
            Data message = messageQueue.ElementAt(0);
            messageQueue.RemoveAt(0);
            return message;
        }
    }
}
