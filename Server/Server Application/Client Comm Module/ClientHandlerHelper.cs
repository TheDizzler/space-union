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
        public delegate GameFrame GameDataDelegate();

        private volatile GameFrame gameDataPlayers;

        private Object Locker = new Object();

        public ClientHandlerHelper(GameDataDelegate getGameData)
        {
            new Thread(() => updatePlayerList(getGameData)).Start();
        }

        /// <summary>
        /// Gets the other players in the gameroom as an array.
        /// </summary>
        /// <returns>Gets an array of data about the positions of the other players.</returns>
        public GameFrame getPlayersData()
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
                GameFrame player = getGameData();
                if (player == null)
                {
                    Thread.Sleep(ClientConstants.DATA_SEND_INTERVAL);
                    continue;
                }
                lock (Locker)
                    gameDataPlayers = player;
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
