﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Data_Structures;
using Data_Manipulation;

namespace Client_Comm_Module
{
    public class ClientCommHandler
    {
        private ClientMessageReceiving receiver;
        private ClientMessageTransmission sender;
        
        private ClientDataReceiving dataReceiver;
        private ClientDataTransmission dataSender;

        private ClientHandlerHelper helper;

        private bool gameStarted = false;

        public ClientCommHandler()
        {
            receiver = new ClientMessageReceiving();
            sender = new ClientMessageTransmission();

            receiver.gameStart += new ClientMessageReceiving.GameStartEventHandler(initializeDataTransmission);
            Console.WriteLine("Created ClientCommHandler()");
        }

        /// <summary>
        /// Returns the login confirmation data to the client with
        /// port assignment data.
        /// </summary>
        /// <returns>The login confirmation data.</returns>
        /*public Player getLoginConfirmation()
        {
            return receiver.receiveLoginConfirmation();
        }*/

        public Player getPlayer()
        {
            return receiver.getPlayer();
        }

        /// <summary>
        /// Gets the other players in the gameroom as an array.
        /// </summary>
        /// <returns>Gets an array of data about the positions of the other players.</returns>
        public GameFrame getPlayersData()
        {
            return helper.getPlayersData();
        }

        /// <summary>
        /// Called when a GameSetupMessage is received.
        /// </summary>
        /// <param name="setupMessage">The received setup message.</param>
        private void initializeDataTransmission(Player setupMessage)
        {
            if (!gameStarted)
            {
                gameStarted = true;
                dataReceiver = new ClientDataReceiving(setupMessage.PortReceive);
                dataSender = new ClientDataTransmission(setupMessage.PortSend);
                helper = new ClientHandlerHelper(getGameData);
                Console.WriteLine("InitializedDataTransmission");
            }
        }

        // SEND FUNCTIONS --------------------------------------------------

        /// <summary>
        /// Send a login request to the server.
        /// </summary>
        /// <param name="playerData">Player data containing player information.</param>
        public void sendLoginRequest(Player playerData)
        {
            if (playerData != null)
                sender.sendLoginRequest(playerData);
        }

        /// <summary>
        /// Send a game message to the server.
        /// </summary>
        /// <param name="message">The message to send to the server.</param>
        public void sendGameMessage(GameMessage message)
        {
            if (message != null)
                sender.addMessageToQueue(message);
            
        }

        /// <summary>
        /// Fetch a game message received from the server.
        /// </summary>
        /// <returns>Game message received from the server.</returns>
        public GameMessage getGameMessage()
        {
            return receiver.getGameMessage();
        }

        /// <summary>
        /// Send a game data to the server.
        /// </summary>
        /// <param name="data">The data to send to the server.</param>
        public void sendGameData(GameData data)
        {
            if (data != null && dataSender != null)
                dataSender.updateData(data);
        }

        /// <summary>
        /// Send a room list request to the server.
        /// </summary>
        /// <param name="player">The sender of the request.</param>
        public Data sendRoomListRequest(Player player)
        {
            sender.addMessageToQueue(new PlayerRequest(player));

            Thread.Sleep(ClientConstants.CLIENT_REQUEST_WAIT_TIME);

            RoomInfo roomInfo;
            return (roomInfo = receiver.getRoomInfo()) != null ? roomInfo : (Data)receiver.getRoomList();
        }

        /// <summary>
        /// Send a room creation request to the server.
        /// </summary>
        /// <param name="player"></param>
        public Data sendRoomCreationRequest(Player player, string roomName)
        {
            sender.addMessageToQueue(new PlayerRequest(player, roomName));

            
            Thread.Sleep(ClientConstants.CLIENT_REQUEST_WAIT_TIME);

            RoomInfo roomInfo;
            return (roomInfo = receiver.getRoomInfo()) != null ? roomInfo : (Data)receiver.getRoomList();
        }

        /// <summary>
        /// Send a room join request to the server.
        /// </summary>
        /// <param name="player"></param>
        public Data sendRoomJoinRequest(Player player, int roomNumber)
        {
            sender.addMessageToQueue(new PlayerRequest(player, roomNumber, Constants.PLAYER_REQUEST_ROOMJOIN));

            Thread.Sleep(ClientConstants.CLIENT_REQUEST_WAIT_TIME);

            RoomInfo roomInfo;
            return (roomInfo = receiver.getRoomInfo()) != null ? roomInfo : (Data)receiver.getRoomList();
        }

        public Data sendRoomInfoRequest(Player player, int roomNumber)
        {
            sender.addMessageToQueue(new PlayerRequest(player, roomNumber, Constants.PLAYER_REQUEST_ROOMINFO));

            Thread.Sleep(ClientConstants.CLIENT_REQUEST_WAIT_TIME);

            return receiver.getRoomInfo();
        }

        public void sendRoomExitRequest(Player player, int roomNumber)
        {
            sender.addMessageToQueue(new PlayerRequest(player, roomNumber, Constants.PLAYER_REQUEST_ROOMEXIT));
        }

        /*public void sendPlayerRequest(Player player, Byte requestType)
        {
            PlayerRequest request = new PlayerRequest(player, requestType);
            sender.addMessageToQueue(request);
        }*/

        // GET FUNCTIONS ----------------------------------------------------

        /// <summary>
        /// Fetch a game data received from the server.
        /// </summary>
        /// <returns>Game data received from the server.</returns>
        private GameFrame getGameData()
        {
            if (dataReceiver != null)
                return dataReceiver.Data;
            return null;
        }

        /// <summary>
        /// Gets the current IP address.
        /// </summary>
        /// <returns>Returns the current IP address.</returns>
        public string getLocalIPv4Address()
        {
            return ClientHandlerHelper.getLocalIPv4Address();
        }
    }
}
