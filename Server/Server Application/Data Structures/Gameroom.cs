using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void addPlayer(Player player)
        {
            GameData data = new GameData();
            data.Player = player;
            data.Player.GameRoom = RoomNumber;
            data.XPosition = 500;
            data.YPosition = 500;
            data.Angle = 0;
            data.Health = 100;
            data.Kills = 0;
            data.Deaths = 0;
        }

        public List<GameData> getPlayerList()
        {
            return list;
        }

        public void removePlayer(GameData user)
        {
            foreach (GameData player in list)
            {
                if (player.Player.Username == user.Player.Username)
                {
                    list.Remove(player);
                    return;
                }
            }
        }
    }
}
