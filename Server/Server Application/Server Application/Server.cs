using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            receiving = new DataReceiving(this);
            transmission = new DataTransmission(this);
            gamerooms = new List<Gameroom>();
            onlineplayers = new List<Player>();
            searchingplayers = new List<Player>();
        }

        public Gameroom getGameroom(int roomnum)
        {
            foreach (Gameroom room in gamerooms)
            {
                if (room.Gameroom == roomnum)
                {
                    return room;
                }
            }
            return null;
        }
    }
}
