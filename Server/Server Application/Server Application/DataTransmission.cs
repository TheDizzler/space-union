﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_Application
{
    /// <summary>
    /// This class is responsible for transmitting data to clients.
    /// Whether updating a player's position or the chat log or simply
    /// allowing them to log into the game.
    /// </summary>
    class DataTransmission
    {
        UdpClient[] UDPClients = new UdpClient[Constants.NumberOfUdpClients];
        TcpClient[] TCPClients = new TcpClient[Constants.NumberOfTcpClients];
        List<GameData>[] UDPQueue = new List<GameData>[6];
        List<GameMessage> messages;
        List<GameData> gamedata;
        List<Player> loginrequests;
        List<ErrorMessage> errormessages;
        Server owner;

        public DataTransmission(Server owner)
        {
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
                UDPClients[x] = new UdpClient(Constants.UDPOutPortOne + x);
            for (int x = 0; x < Constants.NumberOfTcpClients; x++)
                TCPClients[x] = new TcpClient();
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
                UDPQueue[x] = new List<GameData>();
            messages = new List<GameMessage>();
            gamedata = new List<GameData>();
            loginrequests = new List<Player>();
            errormessages = new List<ErrorMessage>();
            this.owner = owner;
        }

        /// <summary>
        /// Validates the oldest login request and replies with either permission to
        /// login to the game or a denial.
        /// </summary>
        public void sendLoginValidationMessage()
        {
            while (true)
            {
                if (loginrequests.Count == 0)
                {
                    Thread.Sleep(5);
                    continue;
                }
                Player request = (Player)removeFromQueue(Constants.LOGIN_REQUEST);
                bool valid = true; //validate with the database, if username/pass combo wrong return false
                if (valid)
                {
                    DataControl.sendTCPData(TCPClients[0], request, request.IPAddress, Constants.TCPLoginClient);
                }
                else
                {
                    ErrorMessage message = null; //Create an error message saying invalid data
                    DataControl.sendTCPData(TCPClients[2], message, message.Player.IPAddress, Constants.TCPErrorClient);
                }
            }
        }

        /// <summary>
        /// Sends a chat message to every player in the game room.
        /// </summary>
        public void sendChatMessages()
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
                foreach (GameData player in room.getPlayerList())
                {
                    if (player.Username != message.Username)
                    {
                        DataControl.sendTCPData(TCPClients[1], message, player.IP, Constants.TCPMessageClient);
                    }
                }
            }
        }

        /// <summary>
        /// Sends an error message to a client.
        /// </summary>
        public void sendErrorMessage()
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
        public void sendClientData()
        {

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
                    case Constants.LOGIN_REQUEST:
                        loginrequests.Add((Player)message);
                        break;
                    case Constants.GAME_DATA:
                        addGameDataToQueue((GameData)message);
                        break;
                    case Constants.CHAT_MESSAGE:
                        messages.Add((GameMessage)message);
                        break;
                    case Constants.ERROR_MESSAGE:
                        errormessages.Add((ErrorMessage)message);
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
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
            {
                if (message.Port == Constants.UDPOutPortOne + x)
                {
                    UDPQueue[x].Add(message);
                    return;
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
                case Constants.GAME_DATA:
                    break;
                case Constants.CHAT_MESSAGE:
                    return removeMessageFromQueue();
                case Constants.ERROR_MESSAGE:
                    return removeErrorMessageFromQueue();
            }
            return null;
        }

        /// <summary>
        /// Returns the oldest error message in the queue.
        /// </summary>
        /// <returns>The oldest error message awaiting transfer.</returns>
        private ErrorMessage removeErrorMessageFromQueue()
        {
            if (errormessages.Count == 0)
                return null;
            ErrorMessage message = errormessages.ElementAt(0);
            errormessages.RemoveAt(0);
            return message;
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

        /// <summary>
        /// Returns the oldest login request in the queue.
        /// </summary>
        /// <returns>The oldest login request awaiting validation.</returns>
        private Player removeLoginRequestFromQueue()
        {
            if (loginrequests.Count == 0)
                return null;
            Player message = loginrequests.ElementAt(0);
            messages.RemoveAt(0);
            return message;
        }
    }
}
