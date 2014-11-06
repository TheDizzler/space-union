using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceUnionDatabase
{
    public class ShipTableAccess
    {
        public bool
        AddNewShip(string shipName, string turnSpeed, string maxSpeed, string accelerateSpeed)
        {
            var db           = new SpaceUnionEntities();
            var newShip      = new Ship();
            bool isShipAdded = false;

            float turnSpd;
            float maxSpd;
            float accelerateSpd;

            convertShipInfoToFloats(turnSpeed, maxSpeed, accelerateSpeed,
                                    out turnSpd, out maxSpd, out accelerateSpd);

            newShip.shipName        = shipName;
            newShip.turnSpeed       = turnSpd;
            newShip.maxSpeed        = maxSpd;
            newShip.accelerateSpeed = accelerateSpd;

            try {
                db.Ships.Add(newShip);
                db.SaveChanges();
                isShipAdded = true;
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            finally {
                db.Dispose();
            }

            return isShipAdded;
        }

        private static void convertShipInfoToFloats(string turnSpeed, string maxSpeed, string accelerateSpeed, out float turnSpd, out float maxSpd, out float accelerateSpd)
        {
            float.TryParse(turnSpeed, out turnSpd);
            float.TryParse(maxSpeed, out maxSpd);
            float.TryParse(accelerateSpeed, out accelerateSpd);
        }

        public bool
        UpdateShipStats(string shipname,string turnSpeed,
                        string maxSpeed, string accelerateSpeed,
                        ref int errCode)
        {
            bool isUpdated = false;
            var  db        = new SpaceUnionEntities();
            
            float turnSpd;
            float maxSpd;
            float accelerateSpd;

            convertShipInfoToFloats(turnSpeed, maxSpeed, accelerateSpeed,
                                    out turnSpd, out maxSpd, out accelerateSpd);

            try {
                var ship = db.Ships
                    .FirstOrDefault(s => s.shipName == shipname);

                if (ship == null)
                    errCode = 0;//incorrect shipname
                else {
                    ship.turnSpeed       = turnSpd;
                    ship.accelerateSpeed = accelerateSpd;
                    ship.maxSpeed        = maxSpd;

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
        GetShipInfo(string shipName, ref int errCode, List<Ship> shipInfo)
        {
            bool isValidShip = false;
            var  db          = new SpaceUnionEntities();
 
            try {
                var ship = db.Ships
                    .FirstOrDefault(u => u.shipName == shipName);

                if (ship == null)
                    errCode = 0;//incorrect shipname
                else {
                    isValidShip = true;
                    shipInfo.Add(ship);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            finally {
                db.Dispose();
            }

            return isValidShip;
        }
    }
}
