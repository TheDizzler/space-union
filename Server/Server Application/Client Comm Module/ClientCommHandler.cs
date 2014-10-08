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
        private ClientDataReceiving receiver;
        private ClientDataTransmission sender;

        public ClientCommHandler()
        {
            receiver = new ClientDataReceiving();
            sender = new ClientDataTransmission();
        }

        // SEND FUNCTIONS --------------------------------------------------

        /// <summary>
        /// Send a login request to the server.
        /// </summary>
        /// <param name="playerData">Player data containing player information.</param>
        public void sendLoginRequest(Player playerData)
        {
            if(playerData != null)
                sender.sendLoginRequest(playerData);
        }

        /// <summary>
        /// Send a registration request to the server.
        /// </summary>
        /// <param name="d">Registration data containing player information.</param>
        public void sendRegistrationInfo(RegistrationData data)
        {
            if(data != null)
                sender.sendRegistrationInfo(data);
        }

        /// <summary>
        /// Send a game message to the server.
        /// </summary>
        /// <param name="message">The message to send to the server.</param>
        public void sendGameMessage(GameMessage message)
        {
            if(message != null)
                sender.addMessageToQueue(message);
        }

        /// <summary>
        /// Send a game data to the server.
        /// </summary>
        /// <param name="data">The data to send to the server.</param>
        public void sendGameData(GameData data)
        {
            if(data != null)
                sender.addMessageToQueue(data);
        }

        // GET FUNCTIONS ----------------------------------------------------

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
