using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SpaceUnionDatabase
{
    /// <summary>
    /// Used to access read/write methods for all tables
    /// </summary>
    class SpaceUnionDatabaseAccess
    {
        /// <summary>
        /// Allows access to reading/writing methods for the Users table
        /// </summary>
        SpaceUnionUsersDatabaseHelper usersTableAccess = new SpaceUnionUsersDatabaseHelper();

        /// <summary>
        /// Adds a new user to the Users table (for user registration)
        /// </summary>
        /// <param name="username">username of the new user</param>
        /// <param name="password">password of the user</param>
        /// <param name="email">email address of the new user</param>
        /// <returns>True if the user was added, false otheriwse</returns>
        public bool
        AddNewUser(string username, string password, string email)
        {
            return usersTableAccess.AddNewUser(username, password, email);
        }

        /// <summary>
        /// Validates that a users login information for that user is correct,
        /// if so logs the user in. Also gets user information if the user has
        /// logged in correctly.
        /// </summary>
        /// <param name="username">name of the user to login</param>
        /// <param name="password">pasword of the user</param>
        /// <param name="userInfo">string the info will be stored to, pass in
        ///                        string[] of size 3</param>
        /// <returns>true if user login is successful and data was
        ///          pulled, false otheriwse.
        ///          
        ///          The data stored in userInfo will be ordered;
        ///          username, email, image path</returns>
        public bool
        UserLogin(string username, string password, string[] userInfo)
        {
            return usersTableAccess.UserLogin(username, password, userInfo);
        }

        /// <summary>
        /// Edits a user's avatar
        /// </summary>
        /// <param name="username">user to change avatar for</param>
        /// <param name="userImage">image path for users avatar</param>
        /// <returns>True if the users image path was edited, false otheriwse</returns>
        public bool
        EditUserImage(string username, string imagePath)
        {
            return usersTableAccess.EditUserImage(username, imagePath);
        }

        /// <summary>
        /// Edits a user's blocked status
        /// </summary>
        /// <param name="username">user to change block status for</param>
        /// <param name="blockStatus">block status to change to</param>
        /// <returns>True if the users block status was changed, false otheriwse</returns>
        public bool
        EditUserBlockStatus(string username, string blockStatus)
        {
            return usersTableAccess.EditUserBlockStatus(username, blockStatus);
        }

        /// <summary>
        /// Query to change a users password
        /// </summary>
        /// <param name="username">User to edit password for</param>
        /// <param name="oldPassword">The users old password (verifies correct user)</param>
        /// <param name="newPassword">The users new password</param>
        /// <returns>Query string to update user's password</returns>
        public bool
        EditUserPassword(string username, string oldPassword, string newPassword)
        {
            return usersTableAccess.EditUserPassword(username, oldPassword, newPassword);
        }
    }
}
