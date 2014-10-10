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
        TcpClient[] TCPClients;
        List<GameData>[] UDPQueue;
        List<GameMessage> chatmessages;
        List<Player> loginrequests;
        List<ErrorMessage> errormessages;
        Server owner;

        public DataTransmission(Server owner)
        {
            UDPClients = new UdpClient[Constants.NumberOfUdpClients];
            TCPClients = new TcpClient[Constants.NumberOfTcpClients];
            UDPQueue = new List<GameData>[6];
            chatmessages = new List<GameMessage>();
            loginrequests = new List<Player>();
            errormessages = new List<ErrorMessage>();
            this.owner = owner;
            setup();
        }

        /// <summary>
        /// Sets up the server by starting all of its main components in new threads.
        /// </summary>
        private void setup()
        {
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
                UDPClients[x] = new UdpClient(Constants.UDPServerToClientPort + x);
            for (int x = 0; x < Constants.NumberOfTcpClients; x++)
                TCPClients[x] = new TcpClient();
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
                UDPQueue[x] = new List<GameData>();
            try
            {
                new Thread(sendLoginValidationMessage).Start();
                new Thread(sendChatMessages).Start();
                new Thread(sendErrorMessage).Start();
                for (byte x = 0; x < Constants.NumberOfUdpClients; x++)
                {
                    byte client = x;
                    new Thread(() => sendClientData(client)).Start();
                }
            }
            catch (ThreadStateException e) { Console.WriteLine("Server has crashed." + e.ToString()); return; }
            catch (OutOfMemoryException e) { Console.WriteLine("Server has crashed." + e.ToString()); return; }
            catch (InvalidOperationException e) { Console.WriteLine("Server has crashed." + e.ToString()); return; }
        }

        /// <summary>
        /// Validates the oldest login request and replies with either permission to
        /// login to the game or a denial.
        /// </summary>
        private void sendLoginValidationMessage()
        {
            while (true)
            {
                if (loginrequests.Count == 0)
                {
                    Thread.Sleep(5);
                    continue;
                }
                Player request = (Player)removeFromQueue(Constants.LOGIN_REQUEST);
                DataControl.sendTCPData(TCPClients[0], request, request.IPAddress, Constants.TCPLoginClient);
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
                foreach (GameData player in room.getPlayerList())
                {
                    if (player.Player.Username != message.Username)
                    {
                        DataControl.sendTCPData(TCPClients[0], message, player.Player.IPAddress, Constants.TCPLoginClient);
                    }
                }
            }
        }

        /// <summary>
        /// Sends an error message to a client.
        /// </summary>
        private void sendErrorMessage()
        {
            while (true)
            {
                ErrorMessage message = (ErrorMessage)removeFromQueue(Constants.ERROR_MESSAGE);
                if (message == null)
                {
                    Thread.Sleep(5);
                    continue;
                }
                DataControl.sendTCPData(TCPClients[2], message, message.Player.IPAddress, Constants.TCPErrorClient);
            }
        }

        /// <summary>
        /// Updates all currently active game clients with current positions.
        /// </summary>
        private void sendClientData(byte client)
        {
            while (true)
            {
                GameData data = getGameData(client);
                if (data == null)
                {
                    Thread.Sleep(1);
                    continue;
                }   
                DataControl.sendUDPData(UDPClients[client], data, data.Player.IPAddress, ((IPEndPoint)UDPClients[client].Client.LocalEndPoint).Port);
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
            Object threadlocker = new Object();
            try
            {
                switch (message.Type)
                {
                    case Constants.LOGIN_REQUEST:
                        lock (threadlocker)
                        {
                            loginrequests.Add((Player)message);
                        }
                        break;
                    case Constants.GAME_DATA:
                        lock (threadlocker)
                        {
                            addGameDataToQueue((GameData)message);
                        }
                        break;
                    case Constants.CHAT_MESSAGE:
                        lock (threadlocker)
                        {
                            chatmessages.Add((GameMessage)message);
                        }
                        break;
                    case Constants.ERROR_MESSAGE:
                        lock (threadlocker)
                        {
                            errormessages.Add((ErrorMessage)message);
                        }
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
            foreach(GameData player in room.getPlayerList())
            {
                if (player.Player.Username != message.Player.Username)
                {
                    UDPQueue[player.Player.PortReceive - Constants.UDPServerToClientPort].Add(message);
                }
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
                case Constants.LOGIN_REQUEST:
                    return removeLoginRequestFromQueue();
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
            Object threadlocker = new Object();
            GameData data = null;
            lock(threadlocker)
            {
                data = UDPQueue[queue].ElementAt(0);
                UDPQueue[queue].RemoveAt(0);
            }
            return data;
        }

        /// <summary>
        /// Returns the oldest error message in the queue.
        /// </summary>
        /// <returns>The oldest error message awaiting transfer.</returns>
        private ErrorMessage removeErrorMessageFromQueue()
        {
            if (errormessages.Count == 0)
                return null;
            Object threadlocker = new Object();
            ErrorMessage message = null;
            lock (threadlocker)
            {
                message = errormessages.ElementAt(0);
                errormessages.RemoveAt(0);
            }
            return message;
        }

        /// <summary>
        /// Returns the oldest chat message in the queue.
        /// </summary>
        /// <returns>The oldest message awaiting transfer.</returns>
        private GameMessage removeMessageFromQueue()
        {
            if (chatmessages.Count == 0)
                return null;
            Object threadlocker = new Object();
            GameMessage message = null;
            lock (threadlocker)
            {
                message = chatmessages.ElementAt(0);
                chatmessages.RemoveAt(0);
            }
            return message;
        }

        /// <summary>
        /// Returns the oldest login request in the queue.
        /// </summary>
        /// <returns>The oldest login request awaiting validation.</returns>
        private Player removeLoginRequestFromQueue()
        {
            if (loginrequests.Count == 0)
                return null;
            Object threadlocker = new Object();
            Player message = null;
            lock (threadlocker)
            {
                message = loginrequests.ElementAt(0);
                loginrequests.RemoveAt(0);
            }
            return message;
        }

        /// <summary>
        /// Checks the size of the Error Message queue.
        /// </summary>
        public void checkErrorQueueSize()
        {
            Console.WriteLine("Queue size of the Error Message list: " + errormessages.Count + "\n");
        }

        /// <summary>
        /// Checks the size of the Chat Message queue.
        /// </summary>
        public void checkChatMessageQueueSize()
        {
            Console.WriteLine("Queue size of the Chat Message list: " + chatmessages.Count + "\n");
        }

        /// <summary>
        /// Checks the size of the Login Request queue.
        /// </summary>
        public void checkLoginRequestQueueSize()
        {
            Console.WriteLine("Queue size for Login Requests list: " + loginrequests.Count + "\n");
        }

        /// <summary>
        /// Checks the size of the Game Data queues.
        /// </summary>
        public void checkGameDataQueueSize()
        {
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
            {
                Console.WriteLine("Queue " + x + ": " + UDPQueue[x].Count);
            }
            Console.WriteLine();
        }
    }
}
