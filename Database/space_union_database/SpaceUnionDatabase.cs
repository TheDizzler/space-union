using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SpaceUnionDatabase
{
    class SpaceUnionDatabase
    {
        /// <summary>
        /// Sets up a new connection to the database
        /// </summary>
        /// <returns>A new database connection</returns>
        public static MySqlConnection Connect()
        {
            return new MySqlConnection(SpaceUnionConnectSettings.CONNECTION_STRING);
        }
    }
}
