using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Manipulation;

namespace Data_Structures
{
    /// <summary>
    /// An actual gameroom containing a list of players and any other necessary data.
    /// </summary>
    public class Gameroom
    {
        List<GameData> list;
        public int RoomNumber { get; set; }

        public Gameroom()
        {
            list = new List<GameData>();
        }

        public int getNumberOfPlayers()
        {
            return list.Count;
        }
        
        public List<GameData> getPlayerList()
        {
            return list;
        }

        public void addPlayer(Player user)
        {
            GameData player = new GameData(Constants.GAME_DATA);
            player.GameRoom = RoomNumber;
            player.Username = user.Username;
            player.IP = user.IPAddress;
            player.Angle = 0;
            player.Deaths = 0;
            player.Health = 100;
            player.Kills = 0;
            player.XPosition = 220;
            player.YPosition = 220;
            player.PortReceive = Constants.UDPOutPortOne + list.Count;
            player.PortSend = Constants.UDPInPortOne + list.Count;
            list.Add(player);
        }

        public void removePlayer(GameData user)
        {
            foreach (GameData player in list)
            {
                if (player.Username == user.Username)
                {
                    list.Remove(player);
                    return;
                }
            }
        }
    }
}
