using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceUnionDatabase
{
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
            string mySql = "SELECT userName FROM Users" +
                             " WHERE Username = '" + username + "' AND" +
                                   " Password = '" + password + "'";
            return mySql;
        }
    }
}
