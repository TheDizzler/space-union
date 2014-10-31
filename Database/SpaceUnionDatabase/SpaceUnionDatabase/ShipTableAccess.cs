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

            float.TryParse(turnSpeed, out turnSpd);
            float.TryParse(maxSpeed, out maxSpd);
            float.TryParse(accelerateSpeed, out accelerateSpd);

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
    }
}
