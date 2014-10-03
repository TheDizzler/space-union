using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SpaceUnionDatabase
{
    /// <summary>
    /// Provides static methods to connect and disconnect to the 
    /// database
    /// </summary>
    protected class SpaceUnionConnect
    {
        /// <summary>
        /// Sets up a new connection to the database
        /// </summary>
        /// <returns>A new database connection</returns>
        public static MySqlConnection dbConnect()
        {
            return new MySqlConnection(SpaceUnionConnectSettings.CONNECTION_STRING);
        }
    }
}