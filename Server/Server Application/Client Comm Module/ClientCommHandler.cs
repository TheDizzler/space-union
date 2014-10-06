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
        private ClientDataTransmission sender;
        private ClientDataReceiving receiver;

        public ClientCommHandler()
        {
            sender = new ClientDataTransmission();
            receiver = new ClientDataReceiving();
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
        /// Send registration information to the server.
        /// </summary>
        /// <param name="d"></param>
        public void sendRegistrationInfo(Data d)
        {
            // Place holder.
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
