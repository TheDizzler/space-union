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

namespace Server_Application
{
    /// <summary>
    /// This class is responsible for transmitting data to clients.
    /// Whether updating a player's position or the chat log or simply
    /// allowing them to log into the game.
    /// </summary>
    class DataTransmission
    {
        UdpClient[] UDPClients;
        TcpClient TCPClient;
        List<GameMessage> chatmessages;
        /// <summary>
        /// Queue for handling intermittent message types (ie. login requests, error messages, room lists).
        /// </summary>
        List<Data> genericQueue;
        Server owner;

        public DataTransmission(Server owner)
        {
            UDPClients   = new UdpClient[Constants.NumberOfUdpClients];
            TCPClient   = new TcpClient();
            chatmessages = new List<GameMessage>();
            genericQueue = new List<Data>();
            this.owner   = owner;
            setup();
        }

        /// <summary>
        /// Sets up the server by starting all of its main components in new threads.
        /// </summary>
        private void setup()
        {
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
                UDPClients[x] = new UdpClient(Constants.UDPServerToClientPort + x);
            TCPClient = new TcpClient();
            try
            {
                new Thread(sendMessage).Start();
                new Thread(sendChatMessages).Start();
                for (byte x = 0; x < Constants.NumberOfUdpClients; x++)
                {
                    byte client = x;
                    new Thread(() => sendClientData(client)).Start();
                }
            }
            catch (ThreadStateException e) { Console.WriteLine("Server has crashed.\n" + e.ToString()); return; }
            catch (OutOfMemoryException e) { Console.WriteLine("Server has crashed.\n" + e.ToString()); return; }
            catch (InvalidOperationException e) { Console.WriteLine("Server has crashed.\n" + e.ToString()); return; }
        }

        /// <summary>
        /// Sends a generic message to a client.
        /// </summary>
        private void sendMessage()
        {
            string ipAddress = null;
            while (true)
            {
                Data message = removeGenericMessageFromQueue();
                if (message == null)
                {
                    Thread.Sleep(5);
                    continue;
                }
                switch (message.Type)
                {
                    case Constants.LOGIN_REQUEST:
                        ipAddress = ((Player)message).IPAddress;
                        break;
                    case Constants.ERROR_MESSAGE:
                        ipAddress = ((ErrorMessage)message).Player.IPAddress;
                        break;
                    case Constants.ROOM_LIST:
                        ipAddress = ((RoomList)message).Receiver.IPAddress;
                        break;
                    case Constants.ROOM_INFO:
                        ipAddress = ((RoomInfo)message).RequesterIP;
                        break;
                }
                if (ipAddress != null)
                    DataControl.sendTCPData(TCPClient, message, ipAddress, Constants.TCPMessageClient);
            }
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
                Gameroom room = owner.getGameroom(message.Gameroom);
                if (room == null)
                    continue;
                foreach (GameData player in room.getPlayerList().ToArray())
                    if (player.Player.Username != message.Username)
                        DataControl.sendTCPData(TCPClient, message, player.Player.IPAddress, Constants.TCPMessageClient);
            }
        }

        /// <summary>
        /// Updates all currently active game clients with current positions.
        /// </summary>
        private void sendClientData(byte client)
        {
            UdpClient sender = UDPClients[client];
            int port = ((IPEndPoint)sender.Client.LocalEndPoint).Port;
            while (true)
            {
                if (owner.Gamerooms.Count == 0)
                {
                    Thread.Sleep(1);
                    continue;
                }
                foreach (KeyValuePair<int, Gameroom> room in owner.Gamerooms.ToArray())
                {
                    if (room.Value.InGame)
                    {
                        GameFrame frame = room.Value.getGameFrame();
                        foreach (object ip in frame.ipAddresses(port))
                            DataControl.sendUDPData(UDPClients[client], frame, (string)ip, port);
                    }
                }
            }
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
                        addGameDataToQueue((GameData)message);
                        break;
                    case Constants.CHAT_MESSAGE:
                        chatmessages.Add((GameMessage)message);
                        break;
                    default:
                        genericQueue.Add(message);
                        break;
                }
            }
            catch (InvalidCastException e) { Console.WriteLine(e.ToString()); return; }
        }

        /// <summary>
        /// Adds a GameData message to a queue based on which port it came from.
        /// </summary>
        /// <param name="message">The message to add to the queue.</param>
        private void addGameDataToQueue(GameData message)
        {
            Gameroom room = owner.getGameroom(message.Player.GameRoom);
            if (room == null)
                return;
            room.updatePlayer(message);
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
                default:
                    return removeGenericMessageFromQueue();                    
            }
        }

        /// <summary>
        /// Returns the oldest chat message in the queue.
        /// </summary>
        /// <returns>The oldest message awaiting transfer.</returns>
        private GameMessage removeMessageFromQueue()
        {
            if (chatmessages.Count == 0)
                return null;
            GameMessage message = null;
            message = chatmessages.ElementAt(0);
            chatmessages.RemoveAt(0);
            return message;
        }

        /// <summary>
        /// Returns the oldest login request in the queue.
        /// </summary>
        /// <returns>The oldest login request awaiting validation.</returns>
        private Data removeGenericMessageFromQueue()
        {
            if (genericQueue.Count == 0)
                return null;
            Data message = null;
            message = genericQueue.ElementAt(0);
            genericQueue.RemoveAt(0);
            return message;
        }

        /// <summary>
        /// Checks the size of the generic queue.
        /// </summary>
        public void checkGenericQueueSize()
        {
            Console.WriteLine("Generic queue size: " + genericQueue.Count + "\n");
        }

        /// <summary>
        /// Checks the size of the Chat Message queue.
        /// </summary>
        public void checkChatMessageQueueSize()
        {
            Console.WriteLine("Queue size of the Chat Message list: " + chatmessages.Count + "\n");
        }
    }
}
