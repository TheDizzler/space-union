using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib;

namespace Server_Application
{
    /// <summary>
    /// An initial message indicating the game has been
    /// created and provides the game client with the
    /// necessary connection information.
    /// </summary>
    class GameSetupMessage : Data
    {
        int roomnumber;
        short incomingport;
        short outgoingport;

        public GameSetupMessage(byte type) : base(type) { }
    }
}
