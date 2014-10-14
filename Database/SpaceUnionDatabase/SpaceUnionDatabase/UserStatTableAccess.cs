using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceUnionDatabase
{
    /// <summary>
    /// Helper class for use with the UserStat table in SpaceUnion.
    /// 
    /// Author: Michael Gordon
    /// </summary>
    class UserStatTableAccess
    {
        /// <summary>
        /// Returns the UserStat entity with users stats.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <returns></returns>
        public UserStat GetUserStats(string username)
        {
            var db = new SpaceUnionEntities();

            var userStats = db.UserStats.FirstOrDefault(u => u.userName == username);

            db.Dispose();

            return userStats;
        }

        /// <summary>
        /// Updates all stats of the UserStat entry.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="win">number of new wins</param>
        /// <param name="lose">number of new loses</param>
        /// <param name="shotsfired">number of new shots fired</param>
        /// <param name="hits">number of new hits</param>
        /// <param name="kills">number of new kills</param>
        /// <param name="died">number of new deaths</param>
        /// <param name="ship1">number of new times ship 1 was used</param>
        /// <param name="ship2">number of new times ship 2 was used</param>
        /// <param name="ship3">number of new times ship 3 was used</param>
        /// <param name="flags">number of new flags captured</param>
        public void SetUserStats(string username, int win = 0, int lose = 0, int shotsfired = 0, int hits = 0, int kills = 0, int died = 0, int ship1 = 0, int ship2 = 0, int ship3 = 0, int flags = 0)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            stats.userstatDied += died;
            stats.userstatFlagsCaptured += flags;
            stats.userstatHits += hits;
            stats.userstatKills += kills;
            stats.userstatLose += lose;
            stats.userstatShipUsed_1 += ship1;
            stats.userstatShipUsed_2 += ship2;
            stats.userstatShipUsed_3 += ship3;
            stats.userstatShotsFired += shotsfired;
            stats.userstatWin += win;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }
        }

        /// <summary>
        /// Updates all stats of the UserStat entry.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="userStats">Array of ten ints representing amounts to update</param>
        public void SetUserStats(string username, int[] userStats)
        {

            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            stats.userstatDied += userStats[0];
            stats.userstatFlagsCaptured += userStats[1];
            stats.userstatHits += userStats[2];
            stats.userstatKills += userStats[3];
            stats.userstatLose += userStats[4];
            stats.userstatShipUsed_1 += userStats[5];
            stats.userstatShipUsed_2 += userStats[6];
            stats.userstatShipUsed_3 += userStats[7];
            stats.userstatShotsFired += userStats[8];
            stats.userstatWin += userStats[9];

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            db.Dispose();
        }

        /// <summary>
        /// Updates the number of deaths for the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="newStat">new amount to update stat by</param>
        public void SetUserStatDied(string username, int newStat)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            stats.userstatDied += newStat;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            db.Dispose();
        }

        /// <summary>
        /// Returns the number of deaths of the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <returns></returns>
        public int GetUserStatDied(string username)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            return stats.userstatDied;
        }

        /// <summary>
        /// Updates the number of Flags Captured by the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="newStat">new amount to update stat by</param>
        public void SetUserStatFlagsCaptured(string username, int newStat)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            stats.userstatFlagsCaptured += newStat;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            db.Dispose();
        }

        /// <summary>
        /// Returns the number of Flags Captured by the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <returns></returns>
        public int GetUserStatFlagsCaptured(string username)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            return stats.userstatFlagsCaptured;
        }

        /// <summary>
        /// Updates the number of shots that hit an enemy player for the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="newStat">new amount to update stat by</param>
        public void SetUserStatHits(string username, int newStat)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            stats.userstatHits += newStat;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            db.Dispose();
        }

        /// <summary>
        /// Returns the number of shots that hit an enemy player for the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <returns></returns>
        public int GetUserStatHits(string username)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            return stats.userstatHits;
        }

        /// <summary>
        /// Updates the number of enemy kills from the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="newStat">new amount to update stat by</param>
        public void SetUserStatKills(string username, int newStat)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            stats.userstatKills += newStat;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            db.Dispose();
        }

        /// <summary>
        /// Returns the number of kills made by the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <returns></returns>
        public int GetUserStatKills(string username)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            return stats.userstatKills;
        }

        /// <summary>
        /// Updates the number of loses for the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="newStat">new amount to update stat by</param>
        public void SetUserStatLose(string username, int newStat)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            stats.userstatLose += newStat;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            db.Dispose();
        }

        /// <summary>
        /// Returns the number of loses of the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <returns></returns>
        public int GetUserStatLose(string username)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            return stats.userstatLose;
        }

        /// <summary>
        /// Updates the number of times Ship 1 was used by the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="newStat">new amount to update stat by</param>
        public void SetUserStatShipUsed_1(string username, int newStat)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            stats.userstatShipUsed_1 += newStat;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            db.Dispose();
        }

        /// <summary>
        /// Returns the number of times Ship 1 was used by the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <returns></returns>
        public int GetUserStatShipUsed_1(string username)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            return stats.userstatShipUsed_1;
        }

        /// <summary>
        /// Updates the number of times Ship 2 was used by the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="newStat">new amount to update stat by</param>
        public void SetUserStatShipUsed_2(string username, int newStat)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            stats.userstatShipUsed_2 += newStat;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            db.Dispose();
        }

        /// <summary>
        /// Returns the number of times Ship 2 was used by the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <returns></returns>
        public int GetUserStatShipUsed_2(string username)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            return stats.userstatShipUsed_2;
        }

        /// <summary>
        /// Updates the number of times Ship 3 was used by the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="newStat">new amount to update stat by</param>
        public void SetUserStatShipUsed_3(string username, int newStat)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            stats.userstatShipUsed_3 += newStat;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            db.Dispose();
        }

        /// <summary>
        /// Returns the number of times Ship 3 was used by the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <returns></returns>
        public int GetUserStatShipUsed_3(string username)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            return stats.userstatShipUsed_3;
        }

        /// <summary>
        /// Updates the number of shots fired by the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="newStat">new amount to update stat by</param>
        public void SetUserStatShotsFired(string username, int newStat)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            stats.userstatShotsFired += newStat;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            db.Dispose();
        }

        /// <summary>
        /// Returns the number of shots fired by the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <returns></returns>
        public int GetUserStatShotsFired(string username)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            return stats.userstatShotsFired;
        }

        /// <summary>
        /// Updates the number of wins of the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <param name="newStat">new amount to update stat by</param>
        public void SetUserStatWin(string username, int newStat)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            stats.userstatWin += newStat;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            db.Dispose();
        }

        /// <summary>
        /// Returns the number of wins of the user.
        /// </summary>
        /// <param name="username">name of the user</param>
        /// <returns></returns>
        public int GetUserStatWin(string username)
        {
            var db = new SpaceUnionEntities();

            var stats = db.UserStats.First(u => u.userName == username);
            return stats.userstatWin;
        }

    }
}
