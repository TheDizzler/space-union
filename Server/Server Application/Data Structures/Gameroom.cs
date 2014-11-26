using System;
using System.Collections.Concurrent;
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
        ConcurrentDictionary<string, GameData> players;
        public int RoomNumber { get; set; }
        public string RoomName { get; set; }
        public bool InGame { get; set; }
        public Player Host { get; set; }
        public int Players { get { return players.Count; } }
        public DateTime GameStart { get; set; }

        public Gameroom() 
        {
            players = new ConcurrentDictionary<string, GameData>();
        }

        public Gameroom(int roomNumber, string roomName, Player host)
        {
            players = new ConcurrentDictionary<string, GameData>();
            RoomNumber = roomNumber;
            RoomName = roomName;
            Host = host;
            InGame = false;
            GameData data = new GameData();
            data.Player = host;
            data.Player.GameRoom = roomNumber;
            Console.WriteLine(host.Username);
            players.TryAdd(host.Username, data);
        }

        public GameFrame getGameFrame()
        {
            return new GameFrame(players.Values.ToArray());
        }

        /// <summary>
        /// Add the given player to the room.
        /// </summary>
        /// <param name="player">The player to add to the room.</param>
        /// <returns>True if the player was successfully added. False otherwise.</returns>
        public bool addPlayer(Player player)
        {
            if (player == null)
                return false;
            if (Players < 6)
            {
                GameData data = new GameData();
                data.Player = player;
                data.Player.GameRoom = RoomNumber;
                players.TryAdd(player.Username, data);
                data.Team = (Players % 2 == 0) ? true : false;
                return true;
            }
            return false;
        }

        public void updatePlayer(GameData player)
        {
            players[player.Player.Username] = player;
        }

        public GameData getPlayer(string username)
        {
            if (!players.ContainsKey(username))
                return null;
            return players[username];

        }

        public GameData[] getPlayerList()
        {
            return players.Values.ToArray();
        }

        public void removePlayer(Player user)
        {
            if (user == null)
                return;
            if (Players > 0)
            {
                GameData temp;
                players.TryRemove(user.Username, out temp);
            }
        }

        public ConcurrentDictionary<string, GameData> getPlayers()
        {
            return players;
        }

        public void stopGame()
        {
            //send updates to database on player data
            InGame = false;
            foreach (GameData player in players.Values.ToArray())
            {
                player.Angle = 0;
                player.Deaths = 0;
                player.Health = 100;
                player.Kills = 0;
                player.XPosition = 0;
                player.YPosition = 0;
            }
        }
    }
}
