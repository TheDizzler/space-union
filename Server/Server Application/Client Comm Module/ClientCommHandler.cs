using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structures;
using Data_Manipulation;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client_Comm_Module
{
    public class ClientCommHandler
    {
        private ClientDataReceiving receiver;
        private ClientDataTransmission sender;
        private GameData[] gameDataPlayers = new GameData[ClientConstants.NumberOfPlayers];

        public ClientCommHandler()
        {
            receiver = new ClientDataReceiving();
            sender = new ClientDataTransmission();
            for (int x = 0; x < ClientConstants.NumberOfPlayers; x++)
                gameDataPlayers[x] = null;
            new Thread(updatePlayerList).Start();
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
            return gameDataPlayers;
        }

        /// <summary>
        /// Updates the array of players with the most current data.
        /// </summary>
        private void updatePlayerList()
        {
            while (true)
            {
                GameData player = receiver.getGameData();
                if (player == null)
                {
                    Thread.Sleep(1);
                    continue;
                }
                Console.WriteLine(receiver.getGameDataQueueSize());
                for (int x = 0; x < ClientConstants.NumberOfPlayers; x++)
                {
                    if (gameDataPlayers[x] == null)
                    {
                        for (int y = 0; y < ClientConstants.NumberOfPlayers; y++)
                        {
                            if (gameDataPlayers[y] != null && gameDataPlayers[y].Player.Username == player.Player.Username)
                            {
                                gameDataPlayers[y] = player;
                                break;
                            }
                        }
                    }
                    if (player.Player.Username == gameDataPlayers[x].Player.Username)
                    {
                        gameDataPlayers[x] = player;
                        break;
                    }
                }
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
        /// Send a game data to the server.
        /// </summary>
        /// <param name="data">The data to send to the server.</param>
        public void sendGameData(GameData data)
        {
            if (data != null)
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

        /// <summary>
        /// Gets the current IP address.
        /// </summary>
        /// <returns>Returns the current IP address.</returns>
        public static string getLocalIPv4Address()
        {
            IPHostEntry host = null;
            try
            {
                host = Dns.GetHostEntry(Dns.GetHostName());
            }
            catch (ArgumentNullException e) { Console.WriteLine(e.ToString()); return null; }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine(e.ToString()); return null; }
            catch (ArgumentException e) { Console.WriteLine(e.ToString()); return null; }
            catch (SocketException e) { Console.WriteLine(e.ToString()); return null; }
            foreach (IPAddress ipv4 in host.AddressList)
            {
                if (ipv4.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipv4.ToString();
                }
            }
            return null;
        }
    }
}
