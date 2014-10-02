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
        List<GameMessage> messages;
        List<GameData> gamedata;
        List<Player> loginrequests;
        List<ErrorMessage> errormessages;
        Server owner;

        public DataTransmission(Server owner)
        {
            for (int x = 0; x < Constants.NumberOfUdpClients; x++)
                UDPClients[x] = new UdpClient(6944 + x);
            for (int x = 0; x < Constants.NumberOfTcpClients; x++)
                TCPClients[x] = new TcpClient();
            messages = new List<GameMessage>();
            gamedata = new List<GameData>();
            loginrequests = new List<Player>();
            errormessages = new List<ErrorMessage>();
            this.owner = owner;
        }

        public void addMessageToQueue(Data message)
        {
            if (message == null)
                return;
                
            try
            {
                switch (message.Type)
                {
                    case Constants.GAME_DATA:
                        gamedata.Add((GameData)message);
                        break;
                    case Constants.CHAT_MESSAGE:
                        messages.Add((GameMessage)message);
                        break;
                    case Constants.ERROR_MESSAGE:
                        errormessages.Add((ErrorMessage)message);
                        break;
                }
            } 
            catch(InvalidCastException e) { Console.WriteLine(e.ToString()); return; }
        }

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

        public void respondToLoginRequest()
        {
            while (true)
            {
                if (loginrequests.Count == 0)
                {
                    Thread.Sleep(5);
                    continue;
                }
                Player request = (Player)removeFromQueue(0);
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

        public void sendClientData()
        {
            
        }

        /// <summary>
        /// Sends a chat message to every player in the game room.
        /// </summary>
        public void sendChatMessages()
        {
            while (true)
            {
                GameMessage message = (GameMessage)removeFromQueue(2);
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
                ErrorMessage message = (ErrorMessage)removeFromQueue(3);
                if (message == null)
                {
                    Thread.Sleep(5);
                    continue;
                }
                DataControl.sendTCPData(TCPClients[2], message, message.Player.IPAddress, Constants.TCPErrorClient);
            }
        }
    }
}
