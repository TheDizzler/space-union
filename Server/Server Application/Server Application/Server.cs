using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//complete game room creation system
namespace Server_Application
{
    /// <summary>
    /// This is the main server application. It will tie together
    /// the DataReceiving and DataTransmission classes, ensuring
    /// fluid communication between the two. It should be set up
    /// in a fashion so that instantiating this class starts the
    /// server running.
    /// </summary>
    class Server
    {
        DataReceiving receiving;
        DataTransmission transmission;
        List<Gameroom> gamerooms;
        /// <summary>
        /// Total list of online players.
        /// </summary>
        List<Player> onlineplayers;
        /// <summary>
        /// Only players who are currently looking for a game match.
        /// </summary>
        List<Player> searchingplayers;
        public Server()
        {
            gamerooms = new List<Gameroom>();
            onlineplayers = new List<Player>();
            searchingplayers = new List<Player>();
            //receiving = new DataReceiving(this);
            transmission = new DataTransmission(this);
        }

        public Gameroom getGameroom(int roomnum)
        {
            foreach (Gameroom room in gamerooms)
            {
                if (room.RoomNumber == roomnum)
                {
                    return room;
                }
            }
            return null;
        }

        public void getNumberOfRooms()
        {
            Console.WriteLine("Number of currently active game rooms: " + gamerooms.Count);
        }

        public void getNumberOfOnlinePlayers()
        {
            Console.WriteLine("Number of online players: " + onlineplayers.Count);
        }

        public void getNumberOfSearchingPlayers()
        {
            Console.WriteLine("Number of players searching for games: " + searchingplayers.Count);
        }
    }
}
