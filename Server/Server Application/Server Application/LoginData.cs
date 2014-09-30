using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    /// <summary>
    /// This is the Datagram to be used for transmission from
    /// the client to the server. This data will be used for
    /// validation with the database.
    /// </summary>
    class LoginData
    {
        string username;
        string password;
    }
}
