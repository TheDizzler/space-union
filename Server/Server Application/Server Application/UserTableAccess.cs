using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Application
{
    public class UserTableAccess
    {
        public void AddNewUser(string username, string password, string email)
        {
            var db      = new SpaceUnionEntities();
            var newUser = new User();

            newUser.userName      = username;
            newUser.userPassword  = password;
            newUser.userEmail     = email;
            newUser.userImage     = "noimage";
            newUser.userIsOnline  = 0;
            newUser.userIsBlocked = 0;
            newUser.userIsAdmin   = 0;

            try {
                db.Users.Add(newUser);
                db.SaveChanges();
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            finally {
                db.Dispose();
            }
        }

        public bool UpdateUserIsOnline(string username, int isOnline, ref int errCode)
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
            }
            finally {
                db.Dispose();
            }

            return isUpdated;
        }

        public bool UpdateUserIsAdmin(string username, int isAdmin, ref int errCode)
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
                    isUpdated         = true;
                }
            }
            catch (Exception e) {
            }
            finally {
                db.Dispose();
            }

            return isUpdated;
        }

        public bool UpdateUserIsBlocked(string username, int isBlocked, ref int errCode)
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
            catch (Exception e)
            {
            }
            finally {
                db.Dispose();
            }

            return isUpdated;
        }

        public bool AdminLogin(string username, string password, ref int errCode)
        {
            bool isValidUser = false;
            var  db          = new SpaceUnionEntities();
            
            try {
                var user = db.Users
                    .FirstOrDefault(u => u.userName     == username && 
                                         u.userPassword == password);

                if (user.userIsBlocked == 1)
                    errCode = 1;//user is blocked
                else if (user.userIsAdmin == 0)
                    errCode = 2;//user is not an admin
                else if (user.userIsOnline == 1)
                    errCode = 3;//user is already logged in
                else if (user == null)
                    errCode = 0;//incorrect user/pass
                else
                    isValidUser = true;
            }
            catch (Exception e) {
            }
            finally {
                db.Dispose();
            }

            return isValidUser;
        }

        public bool UserLogin(string username, string password, ref int errCode, string[] info)
        {
            bool isValidUser = false;
            var  db          = new SpaceUnionEntities();
 
            try
            {
                var user = db.Users
                    .FirstOrDefault(u => u.userName     == username && 
                                         u.userPassword == password);

                if (user.userIsBlocked == 1)
                    errCode = 1;//user is blocked
                else if (user == null)
                    errCode = 0;//incorrect user/pass
                else {
                    isValidUser = true;
                    info[0] = user.userName;
                    info[1] = user.userIsBlocked.ToString();
                    info[2] = user.userIsAdmin.ToString();
                }
            }
            catch (Exception e) {
            }
            finally {
                db.Dispose();
            }

            return isValidUser;
        }

        public bool
        AdminGetUserInfo(string username, ref int errCode, string[] info)
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
                    info[0] = user.userName;
                    info[1] = user.userIsBlocked.ToString();
                    info[2] = user.userIsAdmin.ToString();
                    info[3] = user.userIsOnline.ToString();
                    info[4] = user.userImage;
                    info[5] = user.userPassword;
                    info[6] = user.userEmail;
                }
            }
            catch (Exception e) {
            }
            finally {
                db.Dispose();
            }

            return isValidUser;
        }
    }
}
