using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    class Constants
    {
        /// <summary>
        /// Player data type = 0.
        /// </summary>
        public static const byte PLAYER = 0;

        /// <summary>
        /// Game data type = 1.
        /// </summary>
        public static const byte GAME_DATA = 1;

        /// <summary>
        /// Game message type = 2.
        /// </summary>
        public static const byte GAME_MESSAGE = 2;

        /// <summary>
        /// Error message type = 3.
        /// </summary>
        public static const byte ERROR_MESSAGE = 3;

        /// <summary>
        /// Delay between each UDP transmission in milliseconds = 5.
        /// </summary>
        public static const int UDP_TRANSMISSION_DELAY = 5;
    }
}
