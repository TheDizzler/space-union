﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceUnionDatabase
{
    /// <summary>
    /// Contains settings to connect to the space union database.
    /// This class is only to be used within this solution.
    /// </summary>
    class SpaceUnionConnectSettings
    {
        /// <summary>
        /// Server http address for the hosted database
        /// </summary>
        private const string server = "mysql1.000webhost.com";

        /// <summary>
        /// Database to connect to
        /// </summary>
        private const string database = "a3188200_SpaceUn";

        /// <summary>
        /// UserID to allow connection to the database
        /// </summary>
        private const string uid = "a3188200_SpaceUn";
        
        /// <summary>
        /// Password to allow connection to the database
        /// </summary>
        private const string password = "4lSpaceunion";

        /// <summary>
        /// Returns the connection string used to connect to the space union database
        /// </summary>
        public static string CONNECTION_STRING
        {
            get 
            {
                return "SERVER="   + server   + ";" +
                       "DATABASE=" + database + ";" +
                       "UID="      + uid      + ";" +
                       "PASSWORD=" + password + ";";
            }
        }
    }
}
