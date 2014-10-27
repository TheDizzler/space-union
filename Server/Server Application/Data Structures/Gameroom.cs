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
        Dictionary<string, GameData> players = new Dictionary<string, GameData>();
        public int RoomNumber { get; set; }
        public int Players { get { return players.Count; } }

        public Gameroom() { }

        public void addPlayer(Player player)
        {
            GameData data = new GameData();
            data.Player = player;
            data.Player.GameRoom = RoomNumber;
            players.Add(player.Username, data);
        }

        public void updatePlayer(GameData player)
        {
            players[player.Player.Username] = player;
        }

        public GameData[] getPlayerList()
        {
            return players.Values.ToArray();
        }

        public void removePlayer(GameData user)
        {
            players.Remove(user.Player.Username);
        }
    }
}
