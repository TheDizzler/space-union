using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceUnionDatabase
{
    /// <summary>
    /// Containts query strings to read and write to the Users table
    /// </summary>
    class SpaceUnionUsersQueries
    {
        /// <summary>
        /// Query to insert a new user, their password and email into the Users table
        /// </summary>
        /// <returns>Query string to insert user into the UserProfile table</returns>
        public string
        AddNewUser()
        {
            string query = "INSERT INTO Users (userName, userPassword, userEmail)" +
                                " VALUES (@userName, @userPassword, @userEmail)";
            return query;
        }

        /// <summary>
        /// Query to retrieve all user's profile information
        /// </summary>
        /// <returns>Query string to retrieve all users profile info</returns>
        public string
        AttemptUserLogin()
        {
            string query = "SELECT userName, userEmail, userImage, isBlocked, isAdmin FROM Users" +
                                " WHERE userName     = @userName AND" +
                                      " userPassword = @userPassword";
            return query;
        }

        /// <summary>
        /// Query to update a users avatar
        /// </summary>
        /// <returns>Query string to update user's avatar</returns>
        public string
        EditUserImage()
        {
            string mySql = "UPDATE Users" +
                             " SET   userImage    = @userImage" +
                             " WHERE userName     = @userName AND" +
                                   " userPassword = @userPassword";
            return mySql;
        }

        /// <summary>
        /// Query to update a users blocked status (ADMIN USE ONLY FUNCTION)
        /// </summary>
        /// <returns>Query string to update user's blocked status</returns>
        public string
        EditUserBlockStatus()
        {
            string mySql = "UPDATE Users" +
                             " SET isBlocked  = @blockStatus" +
                             " WHERE userName = @userName";
            return mySql;
        }

        /// <summary>
        /// Query to change a users password
        /// </summary>
        /// <returns>Query string to update user's password</returns>
        public string
        EditUserPassword()
        {
            string mySql = "UPDATE Users" +
                             " SET   userPassword = @newPassword"  +
                             " WHERE userName     = @userName AND" +
                                   " userPassword = @oldPassword";
            return mySql;
        }
    }
}
