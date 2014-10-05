using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceUnionDatabase
{
    /// <summary>
    /// Contains the queries used to read/write to the Users table
    /// These queries are to be called within SpaceUnionUsersDatabaseHelper class
    /// 
    /// Author:       Robert Purdey
    /// Last updated: 03/10/14 (dd/mm/yy)
    /// </summary>
    class SpaceUnionUsersQueries
    {
        /// <summary>
        /// Query to insert a new user, their password and email into the Users table
        /// </summary>
        /// <param name="username">username of the new user</param>
        /// <param name="password">password of the username</param>
        /// <param name="email">email of the new user</param>
        /// <returns>Query string to insert user into the UserProfile table</returns>
        public string
        AddNewUser(string username, string password, string email)
        {
            string query = "INSERT INTO Users (userName, userPassword, userEmail)" +
                                " VALUES (  '" + username + "', " +
                                           "'" + password + "', " +
                                           "'" + email    + "')";
            return query;
        }

        /// <summary>
        /// Query to retrieve all user's profile information
        /// </summary>
        /// <param name="username">user to retrieve profile information for</param>
        /// <param name="password">users password to validate its the actual user</param>
        /// <returns>Query string to retrieve all users profile info</returns>
        public string
        AttemptUserLogin(string username, string password)
        {
            string query = "SELECT userName, userEmail, userImage, isBlocked, isAdmin FROM Users"  +
                                " WHERE userName     = '" + username + "' AND" +
                                      " userPassword = '" + password + "'";
            return query;
        }

        /// <summary>
        /// Query to update a users avatar
        /// </summary>
        /// <param name="username">User to edit profile avatar for</param>
        /// <param name="imagePath">path to users image for their avatar</param>
        /// <returns>Query string to update user's avatar</returns>
        public string
        EditUserImage(string username, string imagePath)
        {
            string mySql = "UPDATE Users" +
                             " SET userImage  = '" + imagePath + "'" +
                             " WHERE Username = '" + username  + "'";
            return mySql;
        }

        /// <summary>
        /// Query to update a users blocked status
        /// </summary>
        /// <param name="username">User to edit blocked status for</param>
        /// <param name="blockStatus">What to change the blockStatus to</param>
        /// <returns>Query string to update user's blocked status</returns>
        public string
        EditUserBlockStatus(string username, string blockStatus)
        {
            string mySql = "UPDATE Users" +
                             " SET isBlocked  = '" + blockStatus + "'" +
                             " WHERE Username = '" + username    + "'";
            return mySql;
        }

        /// <summary>
        /// Query to change a users password
        /// </summary>
        /// <param name="username">User to edit password for</param>
        /// <param name="oldPassword">The users old password (verifies correct user)</param>
        /// <param name="newPassword">The users new password</param>
        /// <returns>Query string to update user's password</returns>
        public string
        EditUserPassword(string username, string oldPassword, string newPassword)
        {
            string mySql = "UPDATE Users" +
                             " SET userPassword   = '" + newPassword + "'" +
                             " WHERE userName     = '" + username    + "' AND " +
                             "       userPassword = '" + oldPassword + "'";
            return mySql;
        }
    }
}
