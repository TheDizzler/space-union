using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SpaceUnionDatabase
{
    class SpaceUnionUsersDatabaseHelper
    {
        /// <summary>
        /// Allows for opening a new connection to the database
        /// </summary>
        private SpaceUnionConnectSettings dbConnect = new SpaceUnionConnectSettings();

        /// <summary>
        /// Used to call queries that will be called on the database
        /// </summary>
        private SpaceUnionUsersQueries userQuery = new SpaceUnionUsersQueries();
 

        /// <summary>
        /// Adds a new user to the database (for user registration)
        /// </summary>
        /// <param name="username">username of the new user</param>
        /// <param name="password">password of the user</param>
        /// <param name="email">email address of the new user</param>
        /// <returns>True if the user was added, false otheriwse</returns>
        public bool
        addNewUser(string username, string password, string email)
        {
            using (MySqlConnection conn = dbConnect.Connect() )
            {
                try
                {
                    string sql = userQuery.AddNewUser(username, password, email);
                    
                    using (MySqlCommand execSql = new MySqlCommand(sql, conn) )
                    {
                        conn.Open();
                        execSql.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }

        /// <summary>
        /// Validates that a users login information for that user is correct,
        /// if so logs the user in. Also gets user information if the user has
        /// logged in correctly.
        /// </summary>
        /// <param name="username">name of the user to login</param>
        /// <param name="password">pasword of the user</param>
        /// <param name="userInfo"-ref> string the info will be stored to</param>
        /// <returns>true if user login is successful and data was
        ///          pulled, false otheriwse</returns>
        public bool
        userLogin(string username, string password, ref string[] userInfo)
        {

            using (MySqlConnection conn = dbConnect.Connect() )
            {
                try
                {    
                    string sql = userQuery.AttemptUserLogin(username, password);

                    using (MySqlCommand execSql = new MySqlCommand(sql, conn) )
                    {
                        conn.Open();
                        execSql.ExecuteNonQuery();

                        using (MySqlDataReader reader = execSql.ExecuteReader() )
                        {
                            if (!reader.HasRows)
                            {
                                //incorrect login info
                                return false;
                            }
                            extractUserData(ref userInfo, reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }

        /// <summary>
        /// Reads user data from the db into the userData string
        /// </summary>
        /// <param name="userData">User data retrieved from the database</param>
        /// <param name="reader">Reader used to read back userdata from the database</param>
        private void
        extractUserData(ref string[] userData, MySqlDataReader reader)
        {
            //MessageBox.Show("start of extract data");       
            //while (reader.Read())
            //{
                for (int i = 0; i < 4; i++)
                    userData[i] = (string)reader.GetValue(i);                
           // }
        }
    }
}
