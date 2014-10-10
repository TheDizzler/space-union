using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SpaceUnionDatabase
{
    /// <summary>
    /// Contains the settings to connect to the space union database
    /// and a single method to return a new connection to the database.
    
    /// Author:       Robert Purdey
    /// Last updated: 03/10/14 (dd/mm/yy)
    /// </summary>
    class SpaceUnionConnectSettings
    {
        /// <summary>
        /// Server http address for the hosted database
        /// </summary>
        private const string server = "sql3.freemysqlhosting.net";

        /// <summary>
        /// Database to connect to
        /// </summary>
        private const string database = "sql353997";

        /// <summary>
        /// UserID to allow connection to the database
        /// </summary>
        private const string uid = "sql353997";
        
        /// <summary>
        /// Password to allow connection to the database
        /// </summary>
        private const string password = "aY9!iD6!";

        /// <summary>
        /// The connection string used to connect to the space union database
        /// </summary>
        private const string connectionString = "SERVER="   + server   + ";" +
                                                "DATABASE=" + database + ";" +
                                                "UID="      + uid      + ";" +
                                                "PASSWORD=" + password + ";";

        /// <summary>
        /// Sets up a new connection to the database and returns it
        /// </summary>
        /// <returns>A new database connection</returns>
        public MySqlConnection Connect()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
