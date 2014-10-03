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
            receiving = new DataReceiving(this);
            transmission = new DataTransmission(this);
        }

        /// <summary>
        /// Retrieve the gameroom matching the given room number.
        /// </summary>
        /// <param name="roomnum">The room number of the gameroom.</param>
        /// <returns>The gameroom matching the room number.</returns>
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

        /// <summary>
        /// Add the given player to the list of online players.
        /// </summary>
        /// <param name="player">The player to mark as active.</param>
        public void addOnlinePlayer(Player player)
        {
            if (player != null)
            {
                onlineplayers.Add(player);
            }
        }

        /// <summary>
        /// Add the given message to the transmission queue.
        /// </summary>
        /// <param name="message">The message to add to the transmission queue.</param>
        public void addMessageToQueue(Data message)
        {
            if (message != null)
            {
                transmission.addMessageToQueue(message);
            }
        }
    }
}
