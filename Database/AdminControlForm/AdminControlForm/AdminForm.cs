﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpaceUnionDatabase;

namespace AdminControlForm
{
    /// <summary>
    /// An admin application to be distrubeted to administrators of Space Union.
    /// 
    /// It will allow administrators to:
    ///     - create new users for the game without having to go through the webApp.
    ///     - lock up and block/unblock users of the game that have been reported
    ///     - add new power-ups to the database or edit existing ones (NOT COMPLETE)
    ///     - add new ships to the database or edit exisiting ones (NOT COMPLETE)
    ///     ...
    ///     
    /// Author: Robert Purdey
    /// </summary>
    public partial class AdminForm : Form
    {
        /// <summary>
        /// Helper class to validate input when a new user is being created
        /// </summary>
        private UserValidation userValidation = new UserValidation();

        /// <summary>
        /// Allows access to read/write to the user table in the Space Union database
        /// </summary>
        private UserTableAccess userTable = new UserTableAccess();
        
        /// <summary>
        /// Helper class to validate input when a new user is being blocked/unblocked
        /// </summary>
        private BlockUnblockUserValidation blockValidation = new BlockUnblockUserValidation();
        
        /// <summary>
        /// States if a username is input correctly
        /// </summary>
        private bool isUsernameValid = false;
        
        /// <summary>
        /// States if a password is input correctly
        /// </summary>
        private bool isPasswordValid = false;
        
        /// <summary>
        /// States if a confirmation password is the same as the original password
        /// </summary>
        private bool isConfPassValid = false;

        /// <summary>
        /// States if the message the admin type to block/unblock the user is valid
        /// </summary>
        private bool isUserBlockActionValid = false;

        /// <summary>
        /// Inits the form application
        /// </summary>
        public AdminForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when the admin clicks on the create  new user button
        /// 
        /// Checks if the input into the textfields is valid and if so,
        /// adds the new user into the database. If the database fails
        /// to write to the database a message why is displayed to the
        /// admin. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttnCreateUser_Click(object sender, EventArgs e)
        {
            bool isvalidUserInfo;
            
            validateUsername(sender, e);
            validatePassword(sender, e);
            validateConfPassword(sender,e);

            isvalidUserInfo = isUsernameValid
                           && isPasswordValid
                           && isConfPassValid;
            
            if (isvalidUserInfo) {
                // if a user isnt added to the database
                if (!userTable.AddNewUser(txtbUsername.Text,
                                          txtbPassword.Text,
                                          txtbEmail.Text) ) {
                    MessageBox.Show("Sorry, the Username is already taken.");
                }
                else {
                    MessageBox.Show("User was added to the database successfully");
                }
            }
        }

        /// <summary>
        /// Occurs when the text in the username textfield changes. 
        /// Validates the current username input as its being typed
        /// and if there is an error displays an error as to why or no
        /// error if the username is valid.
        /// 
        /// Uses the isUsernameValid boolean to track the state the
        /// username is in
        /// - true if valid
        /// - false otherwise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validateUsername(object sender, EventArgs e)
        {
            string userErrMsg = null;

            if (!userValidation.ValidateUsername(txtbUsername.Text, ref userErrMsg)) {
                lablUsernameErrMsg.Text    = userErrMsg;
                lablUsernameErrMsg.Visible = true;
                isUsernameValid            = false;
            }
            else {
                lablUsernameErrMsg.Visible = false;
                isUsernameValid            = true;
            }
        }

        /// <summary>
        /// Occurs when the text in the password textfield changes. 
        /// Validates the current password input as its being typed
        /// and if there is an error displays an error as to why or no
        /// error if the password is valid.
        /// 
        /// Uses the isPasswordValid boolean to track the state the password
        /// is in.
        /// - true if valid
        /// - false otherwise
        /// 
        /// Author: Robert Purdey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validatePassword(object sender, EventArgs e)
        {
            string passErrMsg = null;

            if (!userValidation.ValidatePassword(txtbPassword.Text, ref passErrMsg) ) {
                lablPasswordErrMsg.Text    = passErrMsg;
                lablPasswordErrMsg.Visible = true;
                isPasswordValid            = false;
            }
            else {
                lablPasswordErrMsg.Visible = false;
                isPasswordValid            = true;
            }
        }

        /// <summary>
        /// Occurs when the text in the confirm password textfield changes. 
        /// Validates the current confirm password input as its being typed
        /// and if there is an error displays an error as to why or no error
        /// if the confirm password is valid.
        /// 
        /// Uses the isPasswordValid boolean to track the state the password
        /// is in.
        /// - true if valid
        /// - false otherwise
        /// 
        /// Author:Robert Purdey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validateConfPassword(object sender, EventArgs e)
        {
            string passConfErrMsg = null;

            if (!userValidation.ValidateConfirmPassword(txtbPassword.Text,
                                                        txtbConfirmPassword.Text,
                                                        ref passConfErrMsg) ) {
                lablConfPasswordErrMsg.Text    = passConfErrMsg;
                lablConfPasswordErrMsg.Visible = true;
                isConfPassValid                = false;
            }
            else {
                lablConfPasswordErrMsg.Visible = false;
                isConfPassValid                = true;
            }
        }

        /// <summary>
        /// Gets the username of the user searched for and whether
        /// or not they are blocked from playing the game.
        /// 
        /// Author: Robert Purdey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttnGetUserInfo_Click(object sender, EventArgs e)
        {
            const string BLOCKED = "1";

            int      errCode  = 0;
            string[] userInfo = new string[7];
            

            if (userTable.AdminGetUserInfo(txtbUserToEdit.Text, ref errCode, userInfo) ) {
                txtbUserEditing.Text = userInfo[0];

                if (userInfo[1].Equals(BLOCKED) ) {
                    txtbCurrentBlockStatus.Text = "BLOCKED";
                    chkbBlockUnblockUser.Text   = "Do you want to UNBLOCK " + txtbUserEditing.Text;
                }
                else {
                    txtbCurrentBlockStatus.Text = "NOT BLOCKED";
                    chkbBlockUnblockUser.Text   = "Do you want to BLOCK " + txtbUserEditing.Text;
                }            
            }
            else {
                MessageBox.Show("Username may not exist");
            }
        }

        /// <summary>
        /// Occurs when the admin clicks the button to block or unblock
        /// the user from playing space union. Successfully blocks or
        /// unblocks the user if input is valid, otherwise displays an
        /// error message why.
        /// 
        /// Author: Robert Purdey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttnBlockUnblock_Click(object sender, EventArgs e)
        {
            const int BLOCK   = 1;
            const int UNBLOCK = 0;
            
            int errCode = 0;

            AcceptCancelBlockActionForm acceptBlock =
                    new AcceptCancelBlockActionForm();
            acceptBlock.TxtMsg = "Are you sure you want to block " + txtbUserEditing.Text;

            AcceptCancelBlockActionForm acceptUnblock =
                    new AcceptCancelBlockActionForm();
            acceptUnblock.TxtMsg = "Are you sure you want to unblock " + txtbUserEditing.Text;

            if (chkbBlockUnblockUser.Checked) {
                if (txtbCurrentBlockStatus.Text.Equals("NOT BLOCKED") ) {
                    // User blocking dialog
                    if (acceptBlock.ShowDialog() == DialogResult.OK) {
                        userTable.UpdateUserIsBlocked(txtbUserEditing.Text, BLOCK, ref errCode);
                        MessageBox.Show("User was blocked.");
                    }
                }// User unblocking dialog
                else if (acceptUnblock.ShowDialog() == DialogResult.OK)
                {
                    userTable.UpdateUserIsBlocked(txtbUserEditing.Text, UNBLOCK, ref errCode);
                    MessageBox.Show("User was unblocked");
                }
            }
        }

<<<<<<< HEAD
        private void button1_Click(object sender, EventArgs e)
        {
            string[] info = new string[7];
            int i = 3;
            userTable.AdminGetUserInfo(logintext.Text, ref i, info);
 

            MessageBox.Show("username : " + info[0] + "\n" +
                            "blocked  : " + info[1] + "\n" +
                            "admin    : " + info[2] + "\n" +
                            "online   : " + info[3] + "\n" +
                            "image    : " + info[4] + "\n" +
                            "password : " + info[5] + "\n" +
                            "email    : " + info[6]);
=======
        /// <summary>
        /// Adds the new ship to the ship table in the database.
        /// First checks if all fields are valid and if so, attempts
        /// to add the ship to the table. Shows a message of
        /// success or failure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttnAddShip_Click(object sender, EventArgs e)
        {
            bool isShipValid = false;

            validateAllShipAddFields(sender, e);

            isShipValid = isMaxSpeedValid
                       && isTurnSpeedValid
                       && isAccelerationValid
                       && isShipNameValid;

            if (isShipValid)
            {
                // if a user isnt added to the database
                if (!shipTable.AddNewShip(txtbNewShipName.Text,
                                          txtbTurnSpeed.Text,
                                          txtbMaxSpeed.Text,
                                          txtbAccelerate.Text))
                {
                    MessageBox.Show("Sorry, the Ship name is already in use.");
                }
                else
                {
                    MessageBox.Show("Ship was added to the database successfully.");
                }
            }
        }

        /// <summary>
        /// Helper funticon for when the ship add button is clicked.
        /// 
        /// Validates all fields before adding the ship by calling
        /// all validation functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validateAllShipAddFields(object sender, EventArgs e)
        {
            validateTurnSpeed(sender, e);
            validateMaxSpeed(sender, e);
            validateAcceleration(sender, e);
            validateShipName(sender, e);
        }

        private void validateTurnSpeed(object sender, EventArgs e)
        {
            string turnSpeedErrMsg = null;

            if (!shipValidation.ValidateTurnSpeed(txtbTurnSpeed.Text,
                                                  ref turnSpeedErrMsg))
            {
                lablTurnSpeedErrMsg.Text = turnSpeedErrMsg;
                lablTurnSpeedErrMsg.Visible = true;
                isTurnSpeedValid = false;
            }
            else
            {
                lablTurnSpeedErrMsg.Visible = false;
                isTurnSpeedValid = true;
            }
        }

        private void validateMaxSpeed(object sender, EventArgs e)
        {
            string turnMaxSpeedErrMsg = null;

            if (!shipValidation.ValidateMaxSpeed(txtbMaxSpeed.Text,
                                                 ref turnMaxSpeedErrMsg))
            {
                lablMaxSpeedErrMsg.Text = turnMaxSpeedErrMsg;
                lablMaxSpeedErrMsg.Visible = true;
                isMaxSpeedValid = false;
            }
            else
            {
                lablMaxSpeedErrMsg.Visible = false;
                isMaxSpeedValid = true;
            }
        }

        private void validateShipName(object sender, EventArgs e)
        {
            string shipNameErrMsg = null;

            if (!shipValidation.ValidateShipName(txtbNewShipName.Text,
                                                 ref shipNameErrMsg))
            {
                lablNewShipNameErrMsg.Text = shipNameErrMsg;
                lablNewShipNameErrMsg.Visible = true;
                isShipNameValid = false;
            }
            else
            {
                lablNewShipNameErrMsg.Visible = false;
                isShipNameValid = true;
            }
        }

        private void validateAcceleration(object sender, EventArgs e)
        {
            string turnAccelerateErrMsg = null;

            if (!shipValidation.ValidateAcceleration(txtbAccelerate.Text,
                                                     ref turnAccelerateErrMsg))
            {
                lablAccelErrMsg.Text = turnAccelerateErrMsg;
                lablAccelErrMsg.Visible = true;
                isAccelerationValid = false;
            }
            else
            {
                lablAccelErrMsg.Visible = false;
                isAccelerationValid = true;
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'spaceUnionDataSet.Powerups' table. You can move, or remove it, as needed.
            this.powerupTableAdapter.Fill(this.spaceUnionDataSet.Powerup);
        }

        /// <summary>
        /// updates the userstats with the data entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UserStat oldStat = userStatTable.getUserStat(this.tbUserStatName.Text);

            if (!string.IsNullOrWhiteSpace(this.tbUserStatName.Text))
            {
                userStatTable.setUserStatWin(oldStat.userName.ToString(), (int)this.nudWins.Value - oldStat.userstatWin);
                userStatTable.setUserStatLose(oldStat.userName.ToString(), (int)this.nudLoses.Value - oldStat.userstatLose);
                userStatTable.setUserStatDied(oldStat.userName.ToString(), (int)this.nudDied.Value - oldStat.userstatDied);
                userStatTable.setUserStatHits(oldStat.userName.ToString(), (int)this.nudHits.Value - oldStat.userstatHits);
                userStatTable.setUserStatKills(oldStat.userName.ToString(), (int)this.nudKills.Value - oldStat.userstatKills);
                userStatTable.setUserStatShip1(oldStat.userName.ToString(), (int)this.nudShip1.Value - oldStat.userstatShipUsed_1);
                userStatTable.setUserStatShip2(oldStat.userName.ToString(), (int)this.nudShip2.Value - oldStat.userstatShipUsed_2);
                userStatTable.setUserStatShip3(oldStat.userName.ToString(), (int)this.nudShip3.Value - oldStat.userstatShipUsed_3);
                userStatTable.setUserStatFlagsCaptured(oldStat.userName.ToString(), (int)this.nudFlagsCaptured.Value - oldStat.userstatFlagsCaptured);
                userStatTable.setUserStatShotsFired(oldStat.userName.ToString(), (int)this.nudShotsFired.Value - oldStat.userstatShotsFired);
            }
        }


        private void btnGetUserStats_Click(object sender, EventArgs e)
        {
            UserStat oldStat = userStatTable.getUserStat(this.tbUserStatName.Text);

            if (oldStat != null)
            {
                this.nudDied.Value = oldStat.userstatDied;
                this.nudFlagsCaptured.Value = oldStat.userstatFlagsCaptured;
                this.nudHits.Value = oldStat.userstatHits;
                this.nudKills.Value = oldStat.userstatKills;
                this.nudLoses.Value = oldStat.userstatLose;
                this.nudShip1.Value = oldStat.userstatShipUsed_1;
                this.nudShip2.Value = oldStat.userstatShipUsed_2;
                this.nudShip3.Value = oldStat.userstatShipUsed_3;
                this.nudShotsFired.Value = oldStat.userstatShotsFired;
                this.nudWins.Value = oldStat.userstatWin;

                this.btnUpdate.Enabled = true;
            }
        }

        /// <summary>
        /// checks to make sure a valid username is entered before allowing an update.
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </summary>
        private void tbUserStatName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.tbUserStatName.Text))
                this.btnGetUserStats.Enabled = true;
            else
                this.btnGetUserStats.Enabled = false;

        }

        private void bttnShipToEdit_Click(object sender, EventArgs e)
        {
            List<Ship> shipInfo = new List<Ship>();
            string shipname = txtbShipEditing.Text;
            int errCode = -1;

            if (shipTable.GetShipInfo(shipname, ref errCode, shipInfo))
            {
                Ship ship = shipInfo.First();

                rtxtCurrentShipStats.Text = "Ship Name: " + ship.shipName.ToString() + "\n";
                rtxtCurrentShipStats.Text += "Turn Speed: " + ship.turnSpeed.ToString() + "\n";
                rtxtCurrentShipStats.Text += "Acceleration: " + ship.accelerateSpeed.ToString() + "\n";
                rtxtCurrentShipStats.Text += "Max Speed: " + ship.maxSpeed.ToString() + "\n";

                shipToEdit = ship.shipName.ToString();
            }
            else
            {
                rtxtCurrentShipStats.Text = "The ship was not found.";
            }
        }

        /// <summary>
        /// Helper funticon for when the ship add button is clicked.
        /// 
        /// Validates all fields before adding the ship by calling
        /// all validation functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validateAllShipEditFields(object sender, EventArgs e)
        {
            validateTurnSpeedEdit(sender, e);
            validateMaxSpeedEdit(sender, e);
            validateAccelerationEdit(sender, e);
        }

        private void validateTurnSpeedEdit(object sender, EventArgs e)
        {
            string turnSpeedErrMsg = null;

            if (!shipValidation.ValidateTurnSpeed(txtbNewTurnSpeed.Text,
                                                  ref turnSpeedErrMsg))
            {
                lablShipEditTurnSpdErrMsg.Text = turnSpeedErrMsg;
                lablShipEditTurnSpdErrMsg.Visible = true;
                isTurnSpeedEditValid = false;
            }
            else
            {
                lablShipEditTurnSpdErrMsg.Visible = false;
                isTurnSpeedEditValid = true;
            }
        }

        private void validateMaxSpeedEdit(object sender, EventArgs e)
        {
            string maxSpeedErrMsg = null;

            if (!shipValidation.ValidateMaxSpeed(txtbNewMaxSpeed.Text,
                                                 ref maxSpeedErrMsg))
            {
                lablShipEditMaxSpeed.Text = maxSpeedErrMsg;
                lablShipEditMaxSpeed.Visible = true;
                isMaxSpeedEditValid = false;
            }
            else
            {
                lablShipEditMaxSpeed.Visible = false;
                isMaxSpeedEditValid = true;
            }
        }

        private void validateAccelerationEdit(object sender, EventArgs e)
        {
            string accelerateErrMsg = null;

            if (!shipValidation.ValidateAcceleration(txtbNewAccelerate.Text,
                                                     ref accelerateErrMsg))
            {
                lablShipEditAccelerateErrMsg.Text = accelerateErrMsg;
                lablShipEditAccelerateErrMsg.Visible = true;
                isAccelerationEditValid = false;
            }
            else
            {
                lablShipEditAccelerateErrMsg.Visible = false;
                isAccelerationEditValid = true;
            }
        }

        private void bttnShipUpdate_Click(object sender, EventArgs e)
        {
            bool isShipEditValid = false;
            int errCode = -1;

            AcceptCancelBlockActionForm acceptShipEdit =
                    new AcceptCancelBlockActionForm();

            validateAllShipEditFields(sender, e);

            isShipEditValid = isMaxSpeedEditValid
                           && isTurnSpeedEditValid
                           && isAccelerationEditValid;

            if (isShipEditValid && !String.IsNullOrEmpty(shipToEdit))
            {
                acceptShipEdit.TxtMsg = "Are you sure you want to update " + shipToEdit;

                // check to see if they really want to edit the ship
                if (acceptShipEdit.ShowDialog() == DialogResult.OK)
                {
                    shipTable.UpdateShipStats(shipToEdit,
                                              txtbNewTurnSpeed.Text,
                                              txtbNewMaxSpeed.Text,
                                              txtbNewAccelerate.Text,
                                              ref errCode);

                    MessageBox.Show(shipToEdit + " has been updated.");
                }
            }
            else if (String.IsNullOrEmpty(shipToEdit))
            {
                MessageBox.Show("A ship to edit has not been chosen yet.");
            }
        }

        /// <summary>
        /// checks to make sure a valid powerup is entered before allowing an update.
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </summary>
        private void tbPowerupName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.tbPowerupName.Text))
                this.btnUpdatePwr.Enabled = true;
            else
                this.btnUpdatePwr.Enabled = false;

        }

        private void btnUpdatePwr_Click(object sender, EventArgs e)
        {
            Powerup oldPwrup = powerupTable.getPowerup(this.tbPowerupName.Text);

            if (!string.IsNullOrWhiteSpace(this.tbPowerupName.Text) && oldPwrup != null)
            {
                powerupTable.setPowerup(oldPwrup.PowerupName.ToString(), (int)this.nudPwrValue.Value);
            }
            else
            {
                powerupTable.addPowerup(this.tbPowerupName.Text, (int)this.nudPwrValue.Value);
            }
            this.powerupTableAdapter.Fill(this.spaceUnionDataSet.Powerup);
            this.dgvPwrup.Refresh();
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
        }
    }
}
