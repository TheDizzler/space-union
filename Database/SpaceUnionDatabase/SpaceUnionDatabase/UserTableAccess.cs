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
        public bool AddNewUser(string username, string password, string email)
        {
            var db           = new SpaceUnionEntities();
            var newUser      = new User();
            bool isUserAdded = false;

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

        public bool
        UpdateUserIsOnline(string username, int isOnline, ref int errCode)
        {
            bool isUpdated = false;
            var  db        = new SpaceUnionEntities();
            
            try {
                var user = db.Users
                    .FirstOrDefault(u => u.userName == username);

                if (user == null)
                    errCode = 0;//incorrect username
                else {
                    user.userIsOnline = (byte)isOnline;
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

        public bool
        UpdateUserIsAdmin(string username, int isAdmin, ref int errCode)
        {
            bool isUpdated = false;
            var  db        = new SpaceUnionEntities();
            
            try {
                var user = db.Users
                    .FirstOrDefault(u => u.userName == username);

                if (user == null)
                    errCode = 0;//incorrect username
                else
                {
                    user.userIsOnline = (byte)isAdmin;
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

        public bool
        UpdateUserIsBlocked(string username, int isBlocked, ref int errCode)
        {
            bool isUpdated = false;
            var  db        = new SpaceUnionEntities();
            
            try {
                var user = db.Users
                    .FirstOrDefault(u => u.userName == username);

                if (user == null)
                    errCode = 0;//incorrect username
                else {
                    user.userIsBlocked = (byte)isBlocked;
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

        public bool
        AdminLogin(string username, string password, ref int errCode)
        {
            bool isValidUser = false;
            var  db          = new SpaceUnionEntities();
            
            try {
                var user = db.Users
                    .FirstOrDefault(u => u.userName     == username && 
                                         u.userPassword == password);

                if (user == null)
                    errCode = 0;//incorrect username/pass
                else if (!PasswordHash.PasswordHash.ValidatePassword(password, user.userPassword) )
                    errCode = 0;//incorrect username/pass
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

        public bool
        UserLogin(string username, string password, ref int errCode, List<User> userInfo)
        {
            bool isValidUser = false;
            var  db          = new SpaceUnionEntities();
 
            try {
                var user = db.Users
                    .FirstOrDefault(u => u.userName == username);

                if (user == null)
                    errCode = 0;//incorrect username/pass
                else if (!PasswordHash.PasswordHash.ValidatePassword(password, user.userPassword) )
                    errCode = 0;//incorrect username/pass
                else if (user.userIsBlocked == 1)
                    errCode = 1;//user is blocked
                else if (user.userIsOnline == 1)
                    errCode = 3;//user is already online
                else {
                    isValidUser = true;
                    userInfo.Add(user);
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

        public bool
        AdminGetUserInfo(string username, ref int errCode, List<User> info)
        {
            bool isValidUser = false;
            var  db          = new SpaceUnionEntities();
 
            try {
                var user = db.Users
                    .FirstOrDefault(u => u.userName == username);

                if (user == null)
                    errCode = 0;//incorrect user/pass
                else {
                    isValidUser = true;
                    info.Add(user);
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
