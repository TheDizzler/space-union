using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceUnionDatabaseAccess
{
    class UserStatTableAccess
    {
        private UserStat userStat = new UserStat();
        private SpaceUnionEntities db = new SpaceUnionEntities();

        /// <summary>
        /// Creates a new empty UserStat entry for a user.
        /// </summary>
        /// <param name="username"></param>
        public bool AddNewUserStat(string username)
        {
            bool success = false;
            db = new SpaceUnionEntities();
            userStat = new UserStat
            {
                userName = username,
                userstatWin = 0,
                userstatLose = 0,
                userstatShotsFired = 0,
                userstatHits = 0,
                userstatKills = 0,
                userstatDied = 0,
                userstatShipUsed_1 = 0,
                userstatShipUsed_2 = 0,
                userstatShipUsed_3 = 0,
                userstatFlagsCaptured = 0
            };

            try
            {
                db.UserStats.Add(userStat);
                db.SaveChanges();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            return success;
        }

        /// <summary>
        /// Returns the entire UserStat entity belonging to a users name.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserStat GetNewUserStat(string username)
        {
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            return userStat;
        }

        /// <summary>
        /// Updates the current number of wins for a user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="value"></param>
        public bool setUserStatWin(string username, int value)
        {
            bool success = false;
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);

                userStat.userstatWin += value;
                db.SaveChanges();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }
            return success;
        }

        /// <summary>
        /// Returns the current number of wins for a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getUserStatWin(string username)
        {
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            return userStat.userstatWin;
        }

        /// <summary>
        /// Updates the current number of loses for a user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="value"></param>
        public bool setUserStatLose(string username, int value)
        {
            bool success = false;
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);

                userStat.userstatLose += value;
                db.SaveChanges();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }
            return success;
        }

        /// <summary>
        /// Returns the current number of loses for a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getUserStatLose(string username)
        {
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            return userStat.userstatLose;
        }

        /// <summary>
        /// Updates the current number of shots fired by a user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="value"></param>
        public bool setUserStatShotsFired(string username, int value)
        {
            bool success = false;
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);

                userStat.userstatShotsFired += value;
                db.SaveChanges();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }
            return success;
        }

        /// <summary>
        /// Returns the current number of shots fired by a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getUserStatShotsFired(string username)
        {
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            return userStat.userstatShotsFired;
        }

        /// <summary>
        /// Updates the current number of hits made by a user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="value"></param>
        public bool setUserStatHits(string username, int value)
        {
            bool success = false;
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);

                userStat.userstatHits += value;
                db.SaveChanges();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }
            return success;
        }

        /// <summary>
        /// Returns the current number of hits made by a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getUserStatHits(string username)
        {
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            return userStat.userstatHits;
        }

        /// <summary>
        /// Updates the current number of kills made by a user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="value"></param>
        public bool setUserStatKills(string username, int value)
        {
            bool success = false;
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);

                userStat.userstatKills += value;
                db.SaveChanges();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }
            return success;
        }

        /// <summary>
        /// Returns the current number of kills made by a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getUserStatKills(string username)
        {
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            return userStat.userstatKills;
        }

        /// <summary>
        /// Updates the current number of time a user has died.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="value"></param>
        public bool setUserStatDied(string username, int value)
        {
            bool success = false;
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);

                userStat.userstatDied += value;
                db.SaveChanges();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }
            return success;
        }

        /// <summary>
        /// Returns the current number of time a user has died.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getUserStatDied(string username)
        {
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            return userStat.userstatDied;
        }

        /// <summary>
        /// Updates the current number of time a user has used Ship1.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="value"></param>
        public bool setUserStatShip1(string username, int value)
        {
            bool success = false;
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);

                userStat.userstatShipUsed_1 += value;
                db.SaveChanges();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }
            return success;
        }

        /// <summary>
        /// Returns the current number of time a user has used Ship1.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getUserStatShip1(string username)
        {
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            return userStat.userstatShipUsed_1;
        }

        /// <summary>
        /// Updates the current number of time a user has used Ship2.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="value"></param>
        public bool setUserStatShip2(string username, int value)
        {
            bool success = false;
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);

                userStat.userstatShipUsed_2 += value;
                db.SaveChanges();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }
            return success;
        }

        /// <summary>
        /// Returns the current number of time a user has used Ship2.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getUserStatShip2(string username)
        {
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            return userStat.userstatShipUsed_2;
        }

        /// <summary>
        /// Updates the current number of time a user has used Ship3.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="value"></param>
        public bool setUserStatShip3(string username, int value)
        {
            bool success = false;
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);

                userStat.userstatShipUsed_3 += value;
                db.SaveChanges();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }
            return success;
        }

        /// <summary>
        /// Returns the current number of time a user has used Ship3.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getUserStatShip3(string username)
        {
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            return userStat.userstatShipUsed_3;
        }

        /// <summary>
        /// Updates the current number of flags captured by a user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="value"></param>
        public bool setUserStatFlagsCaptured(string username, int value)
        {
            bool success = false;
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);

                userStat.userstatFlagsCaptured += value;
                db.SaveChanges();
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }
            return success;
        }

        /// <summary>
        /// Updates the current number of flags captured by a user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int getUserStatFlagsCaptured(string username)
        {
            var db = new SpaceUnionEntities();

            try
            {
                userStat = db.UserStats.FirstOrDefault(u => u.userName == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            return userStat.userstatFlagsCaptured;
        }
    }
}
