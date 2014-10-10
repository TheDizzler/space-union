using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace SpaceUnionDatabase
{
    /// <summary>
    /// Containts functionality to read and write to the Users table
    /// </summary>
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
        AddNewUser(string username, string password, string email)
        {
            using (MySqlConnection conn = dbConnect.Connect() )
            {
                try
                {
                    string sql = userQuery.AddNewUser();
                    
                    using (MySqlCommand execSql = new MySqlCommand(sql, conn) )
                    {
                        execSql.Parameters.AddWithValue("@userName"    , username); 
                        execSql.Parameters.AddWithValue("@userPassword", password);
                        execSql.Parameters.AddWithValue("@userEmail"   , email);
                        
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
        /// <param name="userInfo">string the info will be stored to, pass in
        ///                        string[] of size 5</param>
        /// <returns>true if user login is successful and data was
        ///          pulled, false otheriwse.
        ///          
        ///          The data stored in userInfo will be ordered;
        ///          username, email, image path, if they are blocked and if they
        ///          are an admin user</returns>
        public bool
        UserLogin(string username, string password, string[] userInfo)
        {
            using (MySqlConnection conn = dbConnect.Connect() )
            {
                try
                {    
                    string sql = userQuery.AttemptUserLogin();

                    using (MySqlCommand execSql = new MySqlCommand(sql, conn) )
                    {
                        execSql.Parameters.AddWithValue("@userName"    , username); 
                        execSql.Parameters.AddWithValue("@userPassword", password);

                        conn.Open();
                        execSql.ExecuteNonQuery();

                        using (MySqlDataReader reader = execSql.ExecuteReader() )
                        {
                            if (!reader.HasRows)
                            {
                                //incorrect login info
                                return false;
                            }
                            this.ExtractUserData(userInfo, reader);
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
        ExtractUserData(string[] userData, MySqlDataReader reader)
        {
            while (reader.Read() )
                for (int i = 0; i < 5; i++)
                    userData[i] = (string)reader.GetValue(i);
        }

        /// <summary>
        /// Edits a user's avatar
        /// </summary>
        /// <param name="username">user to change avatar for</param>
        /// <param name="password">User password to verify correct user</param>
        /// <param name="userImage">image path for users avatar</param>
        /// <returns>True if the users image path was edited, false otheriwse</returns>
        public bool
        EditUserImage(string username,  string password, string imagePath)
        {
            using (MySqlConnection conn = dbConnect.Connect() )
            {
                try
                {
                    string sql = userQuery.EditUserImage();

                    using (MySqlCommand execSql = new MySqlCommand(sql, conn) )
                    {
                        execSql.Parameters.AddWithValue("@userName"    , username); 
                        execSql.Parameters.AddWithValue("@userPassword", password);
                        execSql.Parameters.AddWithValue("@userImage"   , imagePath);

                        conn.Open();
                        execSql.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
        /// Edits a user's blocked status (ADMIN USE ONLY FUNCTION)
        /// </summary>
        /// <param name="username">user to change block status for</param>
        /// <param name="blockStatus">block status to change to</param>
        /// <returns>True if the users block status was changed, false otheriwse</returns>
        public bool
        EditUserBlockStatus(string username, string blockStatus)
        {
            using (MySqlConnection conn = dbConnect.Connect() )
            {
                try
                {
                    string sql = userQuery.EditUserBlockStatus();

                    using (MySqlCommand execSql = new MySqlCommand(sql, conn) )
                    {
                        execSql.Parameters.AddWithValue("@userName"   , username); 
                        execSql.Parameters.AddWithValue("@blockStatus", blockStatus);

                        conn.Open();
                        execSql.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
        /// Query to change a users password
        /// </summary>
        /// <param name="username">User to edit password for</param>
        /// <param name="oldPassword">The users old password</param>
        /// <param name="newPassword">The users new password</param>
        /// <returns>Query string to update user's password</returns>
        public bool
        EditUserPassword(string username, string oldPassword, string newPassword)
        {
            using (MySqlConnection conn = dbConnect.Connect() )
            {
                try
                {
                    string sql = userQuery.EditUserPassword();

                    using (MySqlCommand execSql = new MySqlCommand(sql, conn) )
                    {
                        execSql.Parameters.AddWithValue("@userName"   , username); 
                        execSql.Parameters.AddWithValue("@oldPassword", oldPassword);
                        execSql.Parameters.AddWithValue("@newPassword", newPassword);

                        conn.Open();
                        execSql.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }      
            return true;
        }
    }
}
