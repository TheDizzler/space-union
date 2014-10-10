using System;
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
        private ClientMessageReceiving receiver;
        private ClientMessageTransmission sender;
        
        private ClientDataReceiving dataReceiver;
        private ClientDataTransmission dataSender;

        private ClientHandlerHelper helper;

        public ClientCommHandler()
        {
            receiver = new ClientMessageReceiving();
            sender = new ClientMessageTransmission();
            helper = new ClientHandlerHelper(getGameData);

            receiver.gameStart += new ClientMessageReceiving.GameStartEventHandler(initializeDataTransmission);
        }

        /// <summary>
        /// Returns the login confirmation data to the client with
        /// port assignment data.
        /// </summary>
        /// <returns>The login confirmation data.</returns>
        public Player getLoginConfirmation()
        {
            return receiver.receiveLoginConfirmation();
        }

        /// <summary>
        /// Gets the other players in the gameroom as an array.
        /// </summary>
        /// <returns>Gets an array of data about the positions of the other players.</returns>
        public GameData[] getPlayersData()
        {
            return helper.getPlayersData();
        }

        /// <summary>
        /// Called when a GameSetupMessage is received.
        /// </summary>
        /// <param name="setupMessage">The received setup message.</param>
        private void initializeDataTransmission(Player setupMessage)
        {
            dataReceiver = new ClientDataReceiving(setupMessage.PortReceive);
            dataSender = new ClientDataTransmission(setupMessage.PortSend);
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
        /// Send a registration request to the server.
        /// </summary>
        /// <param name="d">Registration data containing player information.</param>
        public void sendRegistrationInfo(RegistrationData data)
        {
            if (data != null)
                sender.sendRegistrationInfo(data);
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
            if (data != null)
                dataSender.addDataToQueue(data);
        }

        // GET FUNCTIONS ----------------------------------------------------

        /// <summary>
        /// Fetch a game data received from the server.
        /// </summary>
        /// <returns>Game data received from the server.</returns>
        private GameData getGameData()
        {
            return dataReceiver.getGameData();
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
