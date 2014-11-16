using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structures;
using SpaceUnionDatabase;

namespace Server_Application
{
    /// <summary>
    /// A utility class which contains static functions
    /// that handle login related functionalities.
    /// </summary>
    class LoginRequests
    {
        /// <summary>
        /// Used to read/write to the user table in the SpaceUnion database
        /// </summary>
        private UserTableAccess userTable = new UserTableAccess();
        /// <summary>
        /// Handle the login request.
        /// </summary>
        /// <param name="loginData">Login request data to process.</param>
        /// <param name="owner">The </param>
        public void handleLoginRequest(Player loginData, Server owner)
        {
            if (validateUserData(loginData, owner))
            {
                owner.addOnlinePlayer(loginData);
                Console.WriteLine("Logged in user");
            }
            else
            {
                Console.WriteLine("Failed to login player: " + loginData.Username + "  " + loginData.Password);
            }   
        }

        /// <summary>
        /// Validate the user login data by matching it against information in the database.
        /// </summary>
        /// <param name="loginData">The login data to validate.</param>
        /// /// <param name="owner">The server sending the error message.</param>
        /// <returns>True if the login data was successfully validated.</returns>
        private bool validateUserData(Player playerData, Server owner)
        {
            User user = new User();
            bool isUserValid = false;
            string username = playerData.Username;
            string password = playerData.Password;
            int loginErrCode = 0;
            int updateOnlineErrCode = 0;
            int isOnline = 1;

            // User name validation failed
            if (userTable.UserLogin(username, password, ref loginErrCode, ref user))
            {
                isUserValid = true;
                /* DONT PUT IN UNTIL UPDATING USER IS OFFLINE IS IMPLEMENTED */
                if (userTable.UpdateUserIsOnline(username, isOnline, ref updateOnlineErrCode))
                {
                    isUserValid = true;
                }
                else
                {
                    ErrorMessage message = new ErrorMessage();
                    message.Player = playerData;
                    message.MessageCode = updateOnlineErrCode;
                }
            }
            else
            {
                ErrorMessage message = new ErrorMessage();
                message.Player = playerData;
                message.MessageCode = loginErrCode;
            }

            // call function that updates the user's online status in the database (for friend list).
            return isUserValid;
        }
    }
}
