﻿using System;
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
        public string RoomName { get; set; }

        public int Players { get { return players.Count; } }

        public Gameroom() { }

        /// <summary>
        /// Add the given player to the room.
        /// </summary>
        /// <param name="player">The player to add to the room.</param>
        /// <returns>True if the player was successfully added. False otherwise.</returns>
        public bool addPlayer(Player player)
        {
            if (Players < 6)
            {
                GameData data = new GameData();
                data.Player = player;
                data.Player.GameRoom = RoomNumber;
                players.Add(player.Username, data);
                return true;
            }
            return false;
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
            if (Players > 0)
            {
                players.Remove(user.Player.Username);
            }
        }
    }
}
