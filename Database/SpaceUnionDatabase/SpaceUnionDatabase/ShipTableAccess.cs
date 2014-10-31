using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceUnionDatabase
{
    class ShipTableAccess
    {
        public bool
        AddNewShip(string username, string password, string email)
        {
            var db           = new SpaceUnionEntities();
            //var newUser      = new User();
            bool isShipAdded = false;

            //newUser.userName      = username;

            try {
                //db.Users.Add(newUser);
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
