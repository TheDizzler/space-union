using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    /// <summary>
    /// An initial message indicating the game has been
    /// created and provides the game client with the
    /// necessary connection information.
    /// </summary>
    [Serializable]
    public class GameSetupMessage : Data
    {
        int roomnumber;
        short incomingport;
        short outgoingport;

        public GameSetupMessage()
        {
            Type = 4;
        }

        public GameSetupMessage(byte type) : base(type) { }
    }
}
