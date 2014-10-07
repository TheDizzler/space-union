using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib;

namespace Server_Application
{
    /// <summary>
    /// An actual gameroom containing a list of players and any other necessary data.
    /// </summary>
    class Gameroom
    {
        List<GameData> list;
        public int RoomNumber { get; set; }

        public Gameroom()
        {
            list = new List<GameData>();
        }

        public List<GameData> getPlayerList()
        {
            return list;
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
