using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    /// <summary>
    /// A utility class which contains static functions
    /// that handle login related functionalities.
    /// </summary>
    class LoginRequests
    {
        /// <summary>
        /// Handle the login request.
        /// </summary>
        /// <param name="loginData">Login request data to process.</param>
        public static void handleLoginRequest(Object loginData)
        {

        }

        /// <summary>
        /// Validate the user login data by matching it against information in the database.
        /// </summary>
        /// <param name="loginData">The login data to validate.</param>
        /// <returns>True if the login data was successfully validated.</returns>
        private static Boolean validateUserData(Player loginData)
        {
            string username = loginData.Username;
            string password = loginData.Password;

            // User name validation failed
            if (!checkUsername(username))
            {
                // Send error message "username does not exist" to the client.
                return false;
            }

            if (!checkUserPW(username, password))
            {
                // Send error message "incorrect password" to the client.
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check the user ID by matching it against the database.
        /// </summary>
        /// <param name="userID">The user ID to search for in the database.</param>
        /// <returns>True if the username exists in the database.</returns>
        private static Boolean checkUsername(string username)
        {
            // (Placeholder) Call a function from the DatabaseRequests class.

            return false;
        }

        /// <summary>
        /// Check the user password by matching it against the user name's associated password.
        /// </summary>
        /// <returns></returns>
        private static Boolean checkUserPW(string username, string password)
        {
            // (Placeholder) Call a function from the DatabaseRequests class.

            return false;
        }
    }
}
