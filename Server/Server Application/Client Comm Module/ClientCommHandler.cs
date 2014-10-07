﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structures;
using Data_Manipulation;

namespace Client_Comm_Module
{
    public class ClientCommHandler
    {
        private ClientDataReceiving receiver;
        private ClientDataTransmission sender;

        public ClientCommHandler()
        {
            receiver = new ClientDataReceiving();
            sender = new ClientDataTransmission();
        }

        /// <summary>
        /// Send a login request to the server.
        /// </summary>
        /// <param name="playerData">Player data containing player information.</param>
        public void sendLoginRequest(Player playerData)
        {
            sender.sendLoginRequest(playerData);
        }

        /// <summary>
        /// Send a registration request to the server.
        /// </summary>
        /// <param name="d">Registration data containing player information.</param>
        public void sendRegistrationInfo(RegistrationData data)
        {
            sender.sendRegistrationInfo(data);
        }

        /// <summary>
        /// Send a game message to the server.
        /// </summary>
        /// <param name="message">The message to send to the server.</param>
        public void sendGameMessage(GameMessage message)
        {
            sender.addMessageToQueue(message);
        }

        /// TESTING SERVER CONNECTION
        /// SENDS GAME MESSAGE TO SERVER
        /// </summary>
        /// <param name="gameMessage"></param>
        public void sendGameMessage(GameMessage gameMessage)
        {
            sender.addMessageToQueue(gameMessage);
        }

        /// <summary>
        /// Fetch a game data received from the server.
        /// </summary>
        /// <returns>Game data received from the server.</returns>
        public GameData getGameData()
        {
            return receiver.getGameData();
        }

        /// <summary>
        /// Fetch a game message received from the server.
        /// </summary>
        /// <returns>Game message received from the server.</returns>
        public GameMessage getGameMessage()
        {
            return receiver.getGameMessage();
        }
    }
}
