using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Structures
{
    /// <summary>
    /// Contains the information of a room.
    /// </summary>
    [Serializable]
    public class RoomInfo : Data
    {
        public GameData[] Players { get; set; }
        public int RoomNumber { get; set; }
        public string RoomName { get; set; }
        public string Host { get; set; }
        public bool InGame { get; set; }
        public string RequesterIP { get; set; }

        public RoomInfo(GameData[] players, 
                        int roomNumber, 
                        string roomName, 
                        string host, 
                        bool inGame)
        {
            Type = 10;
            Players    = players;
            RoomNumber = roomNumber;
            RoomName   = roomName;
            Host       = host;
            InGame     = inGame;
        }

        public RoomInfo(GameData[] players,
                        int roomNumber,
                        string roomName,
                        string host,
                        bool inGame,
                        string requester)
        {
            Type = 10;
            Players = players;
            RoomNumber = roomNumber;
            RoomName = roomName;
            Host = host;
            InGame = inGame;
            RequesterIP = requester;
        }
    }
}
