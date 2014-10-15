using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Data_Structures;
using Data_Manipulation;

namespace Client_Comm_Module
{
    public class ClientHandlerHelper
    {
        public delegate GameData GameDataDelegate();

        //private GameData[] gameDataPlayers = new GameData[ClientConstants.NumberOfPlayers];
        private volatile Dictionary<string, GameData> gameDataPlayers = new Dictionary<string, GameData>();

        private Object listLock = new Object();

        public ClientHandlerHelper(GameDataDelegate getGameData)
        {
            /*for (int x = 0; x < ClientConstants.NumberOfPlayers; x++)
            {
                gameDataPlayers. = null;
            }*/
                
            ThreadStart ts = new ThreadStart(() => updatePlayerList(getGameData));
            new Thread(ts).Start();
        }

        /// <summary>
        /// Gets the other players in the gameroom as an array.
        /// </summary>
        /// <returns>Gets an array of data about the positions of the other players.</returns>
        public Dictionary<string, GameData> getPlayersData()
        {
            return gameDataPlayers;
        }

        /// <summary>
        /// Updates the array of players with the most current data.
        /// </summary>
        private void updatePlayerList(GameDataDelegate getGameData)
        {
            while (true)
            {
                GameData player = getGameData();
                if (player == null)
                {
                    Thread.Sleep(ClientConstants.DATA_SEND_INTERVAL);
                    continue;
                }

                if (gameDataPlayers.ContainsKey(player.Player.Username))
                {
                    lock (listLock)
                    {
                        gameDataPlayers[player.Player.Username] = player;
                    }
                }
                else
                {
                    lock (listLock)
                    {
                        gameDataPlayers.Add(player.Player.Username, player);
                    }
                }
                
                /*



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
                    gameDataPlayers.get = player;
                    break;
                }
                 * */
            }
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
