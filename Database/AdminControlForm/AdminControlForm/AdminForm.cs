using System;
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
<<<<<<< HEAD
            int    errCode    = 0;
=======
            
            int errCode = 0;
>>>>>>> 9a1638b5695bbd263182668fff46011ac7ed9ae4

            AcceptCancelBlockActionForm acceptBlock =
                    new AcceptCancelBlockActionForm();
            acceptBlock.TxtMsg = "Are you sure you want to block " + txtbUserEditing.Text;

            AcceptCancelBlockActionForm acceptUnblock =
                    new AcceptCancelBlockActionForm();
            acceptUnblock.TxtMsg = "Are you sure you want to unblock " + txtbUserEditing.Text;

<<<<<<< HEAD
            if (chkbBlockUnblockUser.Checked)
            {
                if (txtbCurrentBlockStatus.Text.Equals("NOT BLOCKED") )
                {   // User blocking dialog
                    if (acceptBlock.ShowDialog() == DialogResult.OK)
                    {
=======
            if (chkbBlockUnblockUser.Checked) {
                if (txtbCurrentBlockStatus.Text.Equals("NOT BLOCKED") ) {
                    // User blocking dialog
                    if (acceptBlock.ShowDialog() == DialogResult.OK) {
>>>>>>> 9a1638b5695bbd263182668fff46011ac7ed9ae4
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
        /// <summary>
        /// Occurs when the text in the block/unblock textfield changes. 
        /// Validates the current text in the field input as its being typed
        /// and if there is an error displays the error as to why or no error
        /// if the field is valid.
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
       /* private void validBlockInput(object sender, EventArgs e)
        {
            string blockErrMsg = null;
            bool   isBlocked = txtbBlockedStatus.Text;

            if (!blockValidation.ValidateBlockUnblockUser2(chkbBlockUnblockUser.Checked,
                                                          ref blockErrMsg) ) {
                isUserBlockActionValid = false;
            }
            else {
                isUserBlockActionValid = true;
            }
        }*/

=======
>>>>>>> 9a1638b5695bbd263182668fff46011ac7ed9ae4
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
        }
    }
}
