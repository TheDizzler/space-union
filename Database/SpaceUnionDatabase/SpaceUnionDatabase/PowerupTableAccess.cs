using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceUnionDatabase
{
    public class PowerupTableAccess
    {
        private SpaceUnionEntities db = new SpaceUnionEntities();
        private Powerup pwrup = new Powerup();

        public bool addPowerup(string pwrName, int value)
        {
            bool success = false;
            db = new SpaceUnionEntities();
            pwrup = new Powerup
            {
                PowerupName = pwrName,
                PowerupValue = value
            };

            try
            {
                db.Powerups.Add(pwrup);
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

        public Powerup getPowerup(string pwrName)
        {
            var db = new SpaceUnionEntities();

            try
            {
                pwrup = db.Powerups.FirstOrDefault(u => u.pwrName == pwrName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                db.Dispose();
            }

            return pwrup;
        }

        public bool setPowerup(string pwrName, int value)
        {
            bool success = false;
            var db = new SpaceUnionEntities();

            try
            {
                pwrup = db.Powerups.FirstOrDefault(u => u.pwrName == pwrName);

                pwrup.pwrValue = value;
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
    }
}
