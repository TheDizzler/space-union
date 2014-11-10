using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordHash;

namespace SpaceUnionDatabase
{
    /// <summary>
    /// Contains functionality to read/write to the user
    /// table.
    /// </summary>
    public class UserTableAccess
    {
        /// <summary>
        /// Adds a new user to the users table. The password for the user is
        /// saved as a hash using the PasswordHash class to do so.
        /// 
        /// author - Robert Purdey
        /// </summary>
        /// <param name="username">Name the user will use to login and be seen as in-game</param>
        /// <param name="password">The password the user has chosen (before being hased)</param>
        /// <param name="email">The email the user has chosen</param>
        /// <returns>True if the user was added, false otherwise</returns>
        public bool
        AddNewUser(string username, string password, string email)
        {
            var db           = new SpaceUnionEntities();
            var newUser      = new User();
            bool isUserAdded = false;

            // ready new user to be added to the user tables
            newUser.userName      = username;
            newUser.userPassword  = PasswordHash.PasswordHash.CreateHash(password);
            newUser.userEmail     = email;
            newUser.userImage     = "noimage";
            newUser.userIsOnline  = 0;
            newUser.userIsBlocked = 0;
            newUser.userIsAdmin   = 0;

            try {
                db.Users.Add(newUser);
                db.SaveChanges();
                isUserAdded = true;
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            finally {
                db.Dispose();
            }

            return isUserAdded;
        }

        /// <summary>
        /// Sets whether the user is online or offline. If using this function
        /// to set the user to be online, only use it after the UserLogin
        /// function from this class used to the log-in the user returns true.
        /// 
        /// author - robert purdey
        /// </summary>
        /// <param name="username">Name of the user logging in</param>
        /// <param name="onlineState">States what to set the user's online status</param>
        /// <param name="errCode">The errcode describing why the func failed
        ///                      (only changes if this function returns false)</param>
        /// <returns>True of the user's online status was updated, false otherwise</returns>
        public bool
        UpdateUserIsOnline(string username, bool onlineState, ref int errCode)
        {
            bool isUpdated    = false;
            var  db           = new SpaceUnionEntities();
            byte onlineStatus = 0;
            
            if (onlineState)
                onlineStatus = 1;
            
            try {
                var user = db.Users
                    .FirstOrDefault(u => u.userName == username);

                if (user == null)
                    errCode = 4;//incorrect username
                else {
                    user.userIsOnline = onlineStatus;
                    db.SaveChanges();
                    isUpdated = true;    
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            finally {
                db.Dispose();
            }

            return isUpdated;
        }

        /// <summary>
        /// Updates whether a user is an admin or not.
        /// 
        /// author - robert purdey
        /// </summary>
        /// <param name="username">User to update admin status for</param>
        /// <param name="adminState">States what to update admin status to</param>
        /// <param name="errCode">The errcode describing why the func failed
        ///                       (only changes if this function returns false)</param>
        /// <returns>True if the user's admin status was updated, false otherwise</returns>
        public bool
        UpdateUserIsAdmin(string username, bool adminState, ref int errCode)
        {
            bool isUpdated   = false;
            var  db          = new SpaceUnionEntities();
            byte adminStatus = 0;
 
            if (adminState)
                adminStatus = 1;

            try {
                var user = db.Users
                    .FirstOrDefault(u => u.userName == username);

                if (user == null)
                    errCode = 4;//incorrect username
                else
                {
                    user.userIsOnline = adminStatus;
                    db.SaveChanges();
                    isUpdated         = true;
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            finally {
                db.Dispose();
            }

            return isUpdated;
        }

        /// <summary>
        /// Updates if the user is blocked or un blocked from being able to
        /// log into the game.
        /// 
        /// author - robert purdey
        /// </summary>
        /// <param name="username">Name of the user to block or unblock</param>
        /// <param name="blockedState">States what to update the user's blocked state to</param>
        /// <param name="errCode">The errcode describing why this func failed 
        ///                       (only changes if this function returns false)</param>
        /// <returns>True if the users blocked status was updated, false otherwise</returns>
        public bool
        UpdateUserIsBlocked(string username, bool blockState, ref int errCode)
        {
            bool isUpdated   = false;
            var  db          = new SpaceUnionEntities();
            byte blockStatus = 0;

            if (blockState)
                blockStatus = 1;
            
            try {
                var user = db.Users
                    .FirstOrDefault(u => u.userName == username);

                if (user == null)
                    errCode = 4;//incorrect username
                else {
                    user.userIsBlocked = blockStatus;
                    db.SaveChanges();
                    isUpdated = true;
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            finally {
                db.Dispose();
            }

            return isUpdated;
        }

        /// <summary>
        /// Validates that the correct username / password has been entered
        /// in order to allow the admin to log in.
        /// </summary>
        /// <param name="username">name of the admin logging in</param>
        /// <param name="password">password of the admin logging in</param>
        /// <param name="errCode">The errcode describing why this func failed 
        ///                      (only changes if this function returns false)</param>
        /// <returns>True if the admin login information is correct, false otherwise</returns>
        public bool
        AdminLogin(string username, string password, ref int errCode)
        {
            bool isValidUser = false;
            var  db          = new SpaceUnionEntities();

            try {
                var user = db.Users
                    .FirstOrDefault(u => u.userName     == username);

                if (user == null)
                    errCode = 4;//incorrect username/pass
                else if (!PasswordHash.PasswordHash.ValidatePassword(password, user.userPassword) )
                    errCode = 4;//incorrect username/pass
                else if (user.userIsBlocked == 1)
                    errCode = 1;//user is blocked
                else if (user.userIsAdmin == 0)
                    errCode = 2;//user is not an admin
                else if (user.userIsOnline == 1)
                    errCode = 3;//user is already logged in
                else
                    isValidUser = true;
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            finally {
                db.Dispose();
            }

            return isValidUser;
        }

        /// <summary>
        /// Validates that the correct username / password has been entered
        /// in order to allow the user to log in.
        /// </summary>
        /// <param name="username">name of the user logging in</param>
        /// <param name="password">password of the user logging in</param>
        /// <param name="errCode">The errcode describing why this func failed 
        ///                      (only changes if this function returns false)</param>
        /// <returns>True if the user login information is correct, false otherwise</returns>
        public bool
        UserLogin(string username, string password, ref int errCode, ref User userInfo)
        {
            bool isValidUser = false;
            var  db          = new SpaceUnionEntities();
 
            try {
                var user = db.Users
                    .FirstOrDefault(u => u.userName == username);

                if (user == null)
                    errCode = 4;//incorrect username/pass
                else if (!PasswordHash.PasswordHash.ValidatePassword(password, user.userPassword) )
                    errCode = 4;//incorrect username/pass
                else if (user.userIsBlocked == 1)
                    errCode = 1;//user is blocked
                else if (user.userIsOnline == 1)
                    errCode = 3;//user is already online
                else {
                    isValidUser = true;
                    userInfo    = user;
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            finally {
                db.Dispose();
            }

            return isValidUser;
        }

        /// <summary>
        /// Allows an admin to get the info of a user without requiring a password
        /// </summary>
        /// <param name="username">user to get the info for</param>
        /// <param name="errCode">Code that matches an errorCode on the clientside
        ///                       that describes why the func failed (only changes if
        ///                       this function fails)</param>
        /// <param name="userInfo">stores the user information retrieved (only use if this
        ///                        function returns true)</param>
        /// <returns>True if the user's information was retrieved, false otherwise</returns>
        public bool
        AdminGetUserInfo(string username, ref int errCode, ref User userInfo)
        {
            bool isValidUser = false;
            var  db          = new SpaceUnionEntities();
 
            try {
                var user = db.Users
                    .FirstOrDefault(u => u.userName == username);

                if (user == null)
                    errCode = 4;//incorrect user/pass
                else {
                    isValidUser = true;
                    userInfo = user;
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            finally {
                db.Dispose();
            }

            return isValidUser;
        }
    }
}
