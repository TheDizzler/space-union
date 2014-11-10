using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminControlForm
{
    /// <summary>
    /// Helper class to validate ship information fields
    /// when adding or editing a ship
    /// </summary>
    class ShipValidation
    {
        /// <summary>
        /// Validates that the ship name has been entered. (error message is set only if
        /// this function returns false)
        /// </summary>
        /// <param name="shipName">name of the ship to validate</param>
        /// <param name="errMsg">error message explaining the error</param>
        /// <returns>True if the shipname is valid, false otherwise</returns>
        public bool
        ValidateShipName(string shipName, ref string errMsg) {
            bool isValid = false;

            if (String.IsNullOrEmpty(shipName) )
                errMsg = "The ship name must be filled in.";
            else
                isValid = true;

            return isValid;
        }

        /// <summary>
        /// Validates the turn speed of the ship. (error message is set only if
        /// this function returns false)
        /// </summary>
        /// <param name="turnSpeed">turn speed of the ship to validate</param>
        /// <param name="errMsg">error message explaining the error</param>
        /// <returns>True if the turn speed is valid, false otherwise</returns>
        public bool
        ValidateTurnSpeed(string turnSpeed, ref string errMsg) {
            const float MIN_TURN = 0.0f;
            const float MAX_TURN = 100.0f;
            
            float shipTurnSpd = 0;
            bool  isValid     = false;
            
            if (!float.TryParse(turnSpeed, out shipTurnSpd) ) {
                errMsg = "The turn speed must be a float";
            }
            else if (shipTurnSpd > MAX_TURN || shipTurnSpd < MIN_TURN) {
                errMsg = "The turn speed must be between " + MIN_TURN +
                         " and " + MAX_TURN;
            }
            else {
                isValid = true;
            }
            
            return isValid;
        }

        /// <summary>
        /// Validates the acceleration for the ship. (error message is set only if
        /// this function returns false)
        /// </summary>
        /// <param name="accelSpeed">acceleration of the ship to validate</param>
        /// <param name="errMsg">error message explaning the error</param>
        /// <returns>True if the acceleration is valid, false otherwise</returns>
        public bool
        ValidateAcceleration(string accelSpeed, ref string errMsg) {
            const float MIN_ACCEL = 0.0f;
            const float MAX_ACCEL = 100.0f;
            
            float shipAccel = 0;
            bool  isValid   = false;

            if (!float.TryParse(accelSpeed, out shipAccel) ) {
                errMsg = "The acceleration speed must be a float";
            }
            else if (shipAccel > MAX_ACCEL || shipAccel < MIN_ACCEL) {
                errMsg = "The turn speed must be between " + MIN_ACCEL +
                         " and " + MAX_ACCEL;
            }
            else {
                isValid = true;
            }
            
            return isValid;
        }

        /// <summary>
        /// Validates the max speed for the ship. (error message is set only if
        /// this function returns false)
        /// </summary>
        /// <param name="maxSpeed">max speed of the ship to validate</param>
        /// <param name="errMsg">error message explaining the error</param>
        /// <returns>True if the max speed is valid, false otherwise</returns>
        public bool
        ValidateMaxSpeed(string maxSpeed, ref string errMsg) {
            const float MIN_SPEED = 0.0f;
            const float MAX_SPEED = 100.0f;

            float shipMaxSpd = 0;
            bool  isValid = false;

            if (!float.TryParse(maxSpeed, out shipMaxSpd) ) {
                errMsg = "The max speed must be a float";
            }
            else if (shipMaxSpd > MAX_SPEED || shipMaxSpd < MIN_SPEED) {
                errMsg = "The max speed must be between " + MIN_SPEED +
                         " and " + MAX_SPEED;
            }
            else {
                isValid = true;
            }
            
            return isValid;
        }

    }
}
