namespace AdminControlForm
{
    partial class AdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.createUserTab = new System.Windows.Forms.TabPage();
            this.lablConfPasswordErrMsg = new System.Windows.Forms.Label();
            this.bttnCreateUser = new System.Windows.Forms.Button();
            this.lablUsernameSpec2 = new System.Windows.Forms.Label();
            this.lablPasswordSpec2 = new System.Windows.Forms.Label();
            this.lablPasswordSpec = new System.Windows.Forms.Label();
            this.lablUsernameSpec = new System.Windows.Forms.Label();
            this.lablEmailErrMsg = new System.Windows.Forms.Label();
            this.lablPasswordErrMsg = new System.Windows.Forms.Label();
            this.lablUsernameErrMsg = new System.Windows.Forms.Label();
            this.txtbEmail = new System.Windows.Forms.TextBox();
            this.lablEmail = new System.Windows.Forms.Label();
            this.txtbConfirmPassword = new System.Windows.Forms.TextBox();
            this.lablConfirmPassword = new System.Windows.Forms.Label();
            this.txtbPassword = new System.Windows.Forms.TextBox();
            this.lablPassword = new System.Windows.Forms.Label();
            this.txtbUsername = new System.Windows.Forms.TextBox();
            this.lablUsername = new System.Windows.Forms.Label();
            this.editUserTab = new System.Windows.Forms.TabPage();
            this.chkbBlockUnblockUser = new System.Windows.Forms.CheckBox();
            this.bttnBlockUnblock = new System.Windows.Forms.Button();
            this.lablUserEditErrMsg = new System.Windows.Forms.Label();
            this.txtbUserEditing = new System.Windows.Forms.TextBox();
            this.lablUsernameToEdit = new System.Windows.Forms.Label();
            this.bttnGetUserInfo = new System.Windows.Forms.Button();
            this.lablUserToEdit = new System.Windows.Forms.Label();
            this.txtbUserToEdit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.logintext = new System.Windows.Forms.TextBox();
            this.txtbCurrentBlockStatus = new System.Windows.Forms.TextBox();
            this.lablCurrentBlockStatus = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.createUserTab.SuspendLayout();
            this.editUserTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.createUserTab);
            this.tabControl.Controls.Add(this.editUserTab);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(496, 512);
            this.tabControl.TabIndex = 0;
            // 
            // createUserTab
            // 
            this.createUserTab.BackColor = System.Drawing.Color.Transparent;
            this.createUserTab.BackgroundImage = global::AdminControlForm.Properties.Resources.dark_space;
            this.createUserTab.Controls.Add(this.lablConfPasswordErrMsg);
            this.createUserTab.Controls.Add(this.bttnCreateUser);
            this.createUserTab.Controls.Add(this.lablUsernameSpec2);
            this.createUserTab.Controls.Add(this.lablPasswordSpec2);
            this.createUserTab.Controls.Add(this.lablPasswordSpec);
            this.createUserTab.Controls.Add(this.lablUsernameSpec);
            this.createUserTab.Controls.Add(this.lablEmailErrMsg);
            this.createUserTab.Controls.Add(this.lablPasswordErrMsg);
            this.createUserTab.Controls.Add(this.lablUsernameErrMsg);
            this.createUserTab.Controls.Add(this.txtbEmail);
            this.createUserTab.Controls.Add(this.lablEmail);
            this.createUserTab.Controls.Add(this.txtbConfirmPassword);
            this.createUserTab.Controls.Add(this.lablConfirmPassword);
            this.createUserTab.Controls.Add(this.txtbPassword);
            this.createUserTab.Controls.Add(this.lablPassword);
            this.createUserTab.Controls.Add(this.txtbUsername);
            this.createUserTab.Controls.Add(this.lablUsername);
            this.createUserTab.Location = new System.Drawing.Point(4, 22);
            this.createUserTab.Name = "createUserTab";
            this.createUserTab.Padding = new System.Windows.Forms.Padding(3);
            this.createUserTab.Size = new System.Drawing.Size(488, 486);
            this.createUserTab.TabIndex = 0;
            this.createUserTab.Text = "Create New User";
            // 
            // lablConfPasswordErrMsg
            // 
            this.lablConfPasswordErrMsg.AutoSize = true;
            this.lablConfPasswordErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablConfPasswordErrMsg.Location = new System.Drawing.Point(188, 203);
            this.lablConfPasswordErrMsg.Name = "lablConfPasswordErrMsg";
            this.lablConfPasswordErrMsg.Size = new System.Drawing.Size(139, 13);
            this.lablConfPasswordErrMsg.TabIndex = 17;
            this.lablConfPasswordErrMsg.Text = "Confirm Password Error Msg";
            this.lablConfPasswordErrMsg.Visible = false;
            // 
            // bttnCreateUser
            // 
            this.bttnCreateUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.bttnCreateUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnCreateUser.Location = new System.Drawing.Point(48, 397);
            this.bttnCreateUser.Name = "bttnCreateUser";
            this.bttnCreateUser.Size = new System.Drawing.Size(154, 42);
            this.bttnCreateUser.TabIndex = 16;
            this.bttnCreateUser.Text = "Create New User";
            this.bttnCreateUser.UseVisualStyleBackColor = true;
            this.bttnCreateUser.Click += new System.EventHandler(this.bttnCreateUser_Click);
            // 
            // lablUsernameSpec2
            // 
            this.lablUsernameSpec2.AutoSize = true;
            this.lablUsernameSpec2.ForeColor = System.Drawing.Color.White;
            this.lablUsernameSpec2.Location = new System.Drawing.Point(45, 73);
            this.lablUsernameSpec2.Name = "lablUsernameSpec2";
            this.lablUsernameSpec2.Size = new System.Drawing.Size(181, 13);
            this.lablUsernameSpec2.TabIndex = 15;
            this.lablUsernameSpec2.Text = "Can only contain letters and numbers";
            // 
            // lablPasswordSpec2
            // 
            this.lablPasswordSpec2.AutoSize = true;
            this.lablPasswordSpec2.ForeColor = System.Drawing.Color.White;
            this.lablPasswordSpec2.Location = new System.Drawing.Point(45, 163);
            this.lablPasswordSpec2.Name = "lablPasswordSpec2";
            this.lablPasswordSpec2.Size = new System.Drawing.Size(309, 13);
            this.lablPasswordSpec2.TabIndex = 14;
            this.lablPasswordSpec2.Text = "Must contain one digit as well as an upper- and lower-case letter";
            // 
            // lablPasswordSpec
            // 
            this.lablPasswordSpec.AutoSize = true;
            this.lablPasswordSpec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablPasswordSpec.ForeColor = System.Drawing.Color.White;
            this.lablPasswordSpec.Location = new System.Drawing.Point(45, 150);
            this.lablPasswordSpec.Name = "lablPasswordSpec";
            this.lablPasswordSpec.Size = new System.Drawing.Size(81, 13);
            this.lablPasswordSpec.TabIndex = 13;
            this.lablPasswordSpec.Text = "4-32 characters";
            // 
            // lablUsernameSpec
            // 
            this.lablUsernameSpec.AutoSize = true;
            this.lablUsernameSpec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablUsernameSpec.ForeColor = System.Drawing.Color.White;
            this.lablUsernameSpec.Location = new System.Drawing.Point(45, 60);
            this.lablUsernameSpec.Name = "lablUsernameSpec";
            this.lablUsernameSpec.Size = new System.Drawing.Size(81, 13);
            this.lablUsernameSpec.TabIndex = 12;
            this.lablUsernameSpec.Text = "4-16 characters";
            // 
            // lablEmailErrMsg
            // 
            this.lablEmailErrMsg.AutoSize = true;
            this.lablEmailErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablEmailErrMsg.Location = new System.Drawing.Point(98, 269);
            this.lablEmailErrMsg.Name = "lablEmailErrMsg";
            this.lablEmailErrMsg.Size = new System.Drawing.Size(78, 13);
            this.lablEmailErrMsg.TabIndex = 11;
            this.lablEmailErrMsg.Text = "Email error msg";
            this.lablEmailErrMsg.Visible = false;
            // 
            // lablPasswordErrMsg
            // 
            this.lablPasswordErrMsg.AutoSize = true;
            this.lablPasswordErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablPasswordErrMsg.Location = new System.Drawing.Point(125, 109);
            this.lablPasswordErrMsg.Name = "lablPasswordErrMsg";
            this.lablPasswordErrMsg.Size = new System.Drawing.Size(99, 13);
            this.lablPasswordErrMsg.TabIndex = 10;
            this.lablPasswordErrMsg.Text = "Password error msg";
            this.lablPasswordErrMsg.Visible = false;
            // 
            // lablUsernameErrMsg
            // 
            this.lablUsernameErrMsg.AutoSize = true;
            this.lablUsernameErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablUsernameErrMsg.Location = new System.Drawing.Point(132, 19);
            this.lablUsernameErrMsg.Name = "lablUsernameErrMsg";
            this.lablUsernameErrMsg.Size = new System.Drawing.Size(101, 13);
            this.lablUsernameErrMsg.TabIndex = 9;
            this.lablUsernameErrMsg.Text = "Username error msg";
            this.lablUsernameErrMsg.Visible = false;
            // 
            // txtbEmail
            // 
            this.txtbEmail.Location = new System.Drawing.Point(48, 287);
            this.txtbEmail.Name = "txtbEmail";
            this.txtbEmail.Size = new System.Drawing.Size(223, 20);
            this.txtbEmail.TabIndex = 7;
            // 
            // lablEmail
            // 
            this.lablEmail.AutoSize = true;
            this.lablEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablEmail.ForeColor = System.Drawing.Color.White;
            this.lablEmail.Location = new System.Drawing.Point(45, 267);
            this.lablEmail.Name = "lablEmail";
            this.lablEmail.Size = new System.Drawing.Size(47, 17);
            this.lablEmail.TabIndex = 6;
            this.lablEmail.Text = "Email";
            // 
            // txtbConfirmPassword
            // 
            this.txtbConfirmPassword.Location = new System.Drawing.Point(48, 221);
            this.txtbConfirmPassword.Name = "txtbConfirmPassword";
            this.txtbConfirmPassword.Size = new System.Drawing.Size(223, 20);
            this.txtbConfirmPassword.TabIndex = 5;
            this.txtbConfirmPassword.TextChanged += new System.EventHandler(this.validateConfPassword);
            // 
            // lablConfirmPassword
            // 
            this.lablConfirmPassword.AutoSize = true;
            this.lablConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablConfirmPassword.ForeColor = System.Drawing.Color.White;
            this.lablConfirmPassword.Location = new System.Drawing.Point(45, 201);
            this.lablConfirmPassword.Name = "lablConfirmPassword";
            this.lablConfirmPassword.Size = new System.Drawing.Size(137, 17);
            this.lablConfirmPassword.TabIndex = 4;
            this.lablConfirmPassword.Text = "Confirm Password";
            // 
            // txtbPassword
            // 
            this.txtbPassword.Location = new System.Drawing.Point(48, 127);
            this.txtbPassword.Name = "txtbPassword";
            this.txtbPassword.Size = new System.Drawing.Size(223, 20);
            this.txtbPassword.TabIndex = 3;
            this.txtbPassword.TextChanged += new System.EventHandler(this.validatePassword);
            // 
            // lablPassword
            // 
            this.lablPassword.AutoSize = true;
            this.lablPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablPassword.ForeColor = System.Drawing.Color.White;
            this.lablPassword.Location = new System.Drawing.Point(45, 107);
            this.lablPassword.Name = "lablPassword";
            this.lablPassword.Size = new System.Drawing.Size(77, 17);
            this.lablPassword.TabIndex = 2;
            this.lablPassword.Text = "Password";
            // 
            // txtbUsername
            // 
            this.txtbUsername.Location = new System.Drawing.Point(48, 37);
            this.txtbUsername.Name = "txtbUsername";
            this.txtbUsername.Size = new System.Drawing.Size(223, 20);
            this.txtbUsername.TabIndex = 1;
            this.txtbUsername.TextChanged += new System.EventHandler(this.validateUsername);
            // 
            // lablUsername
            // 
            this.lablUsername.AutoSize = true;
            this.lablUsername.BackColor = System.Drawing.Color.Transparent;
            this.lablUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablUsername.ForeColor = System.Drawing.Color.White;
            this.lablUsername.Location = new System.Drawing.Point(45, 17);
            this.lablUsername.Name = "lablUsername";
            this.lablUsername.Size = new System.Drawing.Size(81, 17);
            this.lablUsername.TabIndex = 0;
            this.lablUsername.Text = "Username";
            // 
            // editUserTab
            // 
            this.editUserTab.BackColor = System.Drawing.Color.Transparent;
            this.editUserTab.BackgroundImage = global::AdminControlForm.Properties.Resources.dark_space;
            this.editUserTab.Controls.Add(this.lablCurrentBlockStatus);
            this.editUserTab.Controls.Add(this.txtbCurrentBlockStatus);
            this.editUserTab.Controls.Add(this.chkbBlockUnblockUser);
            this.editUserTab.Controls.Add(this.bttnBlockUnblock);
            this.editUserTab.Controls.Add(this.lablUserEditErrMsg);
            this.editUserTab.Controls.Add(this.txtbUserEditing);
            this.editUserTab.Controls.Add(this.lablUsernameToEdit);
            this.editUserTab.Controls.Add(this.bttnGetUserInfo);
            this.editUserTab.Controls.Add(this.lablUserToEdit);
            this.editUserTab.Controls.Add(this.txtbUserToEdit);
            this.editUserTab.Controls.Add(this.label1);
            this.editUserTab.Location = new System.Drawing.Point(4, 22);
            this.editUserTab.Name = "editUserTab";
            this.editUserTab.Padding = new System.Windows.Forms.Padding(3);
            this.editUserTab.Size = new System.Drawing.Size(488, 486);
            this.editUserTab.TabIndex = 1;
            this.editUserTab.Text = "User block/unblock";
            // 
            // chkbBlockUnblockUser
            // 
            this.chkbBlockUnblockUser.AutoSize = true;
            this.chkbBlockUnblockUser.ForeColor = System.Drawing.Color.White;
            this.chkbBlockUnblockUser.Location = new System.Drawing.Point(20, 339);
            this.chkbBlockUnblockUser.Name = "chkbBlockUnblockUser";
            this.chkbBlockUnblockUser.Size = new System.Drawing.Size(168, 17);
            this.chkbBlockUnblockUser.TabIndex = 12;
            this.chkbBlockUnblockUser.Text = "Do you want to block the user";
            this.chkbBlockUnblockUser.UseVisualStyleBackColor = true;
            // 
            // bttnBlockUnblock
            // 
            this.bttnBlockUnblock.Location = new System.Drawing.Point(82, 387);
            this.bttnBlockUnblock.Name = "bttnBlockUnblock";
            this.bttnBlockUnblock.Size = new System.Drawing.Size(99, 37);
            this.bttnBlockUnblock.TabIndex = 11;
            this.bttnBlockUnblock.Text = "Block/Unblock User";
            this.bttnBlockUnblock.UseVisualStyleBackColor = true;
            this.bttnBlockUnblock.Click += new System.EventHandler(this.bttnBlockUnblock_Click);
            // 
            // lablUserEditErrMsg
            // 
            this.lablUserEditErrMsg.AutoSize = true;
            this.lablUserEditErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablUserEditErrMsg.Location = new System.Drawing.Point(251, 70);
            this.lablUserEditErrMsg.Name = "lablUserEditErrMsg";
            this.lablUserEditErrMsg.Size = new System.Drawing.Size(132, 13);
            this.lablUserEditErrMsg.TabIndex = 9;
            this.lablUserEditErrMsg.Text = "Error entering in Username";
            this.lablUserEditErrMsg.Visible = false;
            // 
            // txtbUserEditing
            // 
            this.txtbUserEditing.Location = new System.Drawing.Point(20, 225);
            this.txtbUserEditing.Name = "txtbUserEditing";
            this.txtbUserEditing.ReadOnly = true;
            this.txtbUserEditing.Size = new System.Drawing.Size(161, 20);
            this.txtbUserEditing.TabIndex = 5;
            // 
            // lablUsernameToEdit
            // 
            this.lablUsernameToEdit.AutoSize = true;
            this.lablUsernameToEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablUsernameToEdit.ForeColor = System.Drawing.Color.White;
            this.lablUsernameToEdit.Location = new System.Drawing.Point(17, 207);
            this.lablUsernameToEdit.Name = "lablUsernameToEdit";
            this.lablUsernameToEdit.Size = new System.Drawing.Size(73, 15);
            this.lablUsernameToEdit.TabIndex = 4;
            this.lablUsernameToEdit.Text = "Username";
            // 
            // bttnGetUserInfo
            // 
            this.bttnGetUserInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnGetUserInfo.Location = new System.Drawing.Point(125, 93);
            this.bttnGetUserInfo.Name = "bttnGetUserInfo";
            this.bttnGetUserInfo.Size = new System.Drawing.Size(120, 28);
            this.bttnGetUserInfo.TabIndex = 3;
            this.bttnGetUserInfo.Text = "Retrieve User Info";
            this.bttnGetUserInfo.UseVisualStyleBackColor = true;
            this.bttnGetUserInfo.Click += new System.EventHandler(this.bttnGetUserInfo_Click);
            // 
            // lablUserToEdit
            // 
            this.lablUserToEdit.AutoSize = true;
            this.lablUserToEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablUserToEdit.ForeColor = System.Drawing.Color.White;
            this.lablUserToEdit.Location = new System.Drawing.Point(16, 178);
            this.lablUserToEdit.Name = "lablUserToEdit";
            this.lablUserToEdit.Size = new System.Drawing.Size(356, 20);
            this.lablUserToEdit.TabIndex = 2;
            this.lablUserToEdit.Text = "Info of the User you Want to Block/Unblock";
            // 
            // txtbUserToEdit
            // 
            this.txtbUserToEdit.Location = new System.Drawing.Point(13, 67);
            this.txtbUserToEdit.Name = "txtbUserToEdit";
            this.txtbUserToEdit.Size = new System.Drawing.Size(232, 20);
            this.txtbUserToEdit.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(447, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter the username of the user you want to block or unblock";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.logintext);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(488, 486);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Username";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(80, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Read info from Database";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // logintext
            // 
            this.logintext.Location = new System.Drawing.Point(77, 49);
            this.logintext.Name = "logintext";
            this.logintext.Size = new System.Drawing.Size(161, 20);
            this.logintext.TabIndex = 0;
            // 
            // txtbCurrentBlockStatus
            // 
            this.txtbCurrentBlockStatus.Location = new System.Drawing.Point(20, 286);
            this.txtbCurrentBlockStatus.Name = "txtbCurrentBlockStatus";
            this.txtbCurrentBlockStatus.ReadOnly = true;
            this.txtbCurrentBlockStatus.Size = new System.Drawing.Size(161, 20);
            this.txtbCurrentBlockStatus.TabIndex = 13;
            // 
            // lablCurrentBlockStatus
            // 
            this.lablCurrentBlockStatus.AutoSize = true;
            this.lablCurrentBlockStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablCurrentBlockStatus.ForeColor = System.Drawing.Color.White;
            this.lablCurrentBlockStatus.Location = new System.Drawing.Point(17, 268);
            this.lablCurrentBlockStatus.Name = "lablCurrentBlockStatus";
            this.lablCurrentBlockStatus.Size = new System.Drawing.Size(137, 15);
            this.lablCurrentBlockStatus.TabIndex = 14;
            this.lablCurrentBlockStatus.Text = "Current Block Status";
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(496, 512);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AdminForm";
            this.Text = "Admin Controls";
            this.tabControl.ResumeLayout(false);
            this.createUserTab.ResumeLayout(false);
            this.createUserTab.PerformLayout();
            this.editUserTab.ResumeLayout(false);
            this.editUserTab.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage createUserTab;
        private System.Windows.Forms.TabPage editUserTab;
        private System.Windows.Forms.TextBox txtbUsername;
        private System.Windows.Forms.Label lablUsername;
        private System.Windows.Forms.TextBox txtbConfirmPassword;
        private System.Windows.Forms.Label lablConfirmPassword;
        private System.Windows.Forms.TextBox txtbPassword;
        private System.Windows.Forms.Label lablPassword;
        private System.Windows.Forms.Label lablPasswordSpec2;
        private System.Windows.Forms.Label lablPasswordSpec;
        private System.Windows.Forms.Label lablUsernameSpec;
        private System.Windows.Forms.Label lablEmailErrMsg;
        private System.Windows.Forms.Label lablPasswordErrMsg;
        private System.Windows.Forms.Label lablUsernameErrMsg;
        private System.Windows.Forms.TextBox txtbEmail;
        private System.Windows.Forms.Label lablEmail;
        private System.Windows.Forms.Label lablUsernameSpec2;
        private System.Windows.Forms.Button bttnCreateUser;
        private System.Windows.Forms.Button bttnGetUserInfo;
        private System.Windows.Forms.Label lablUserToEdit;
        private System.Windows.Forms.TextBox txtbUserToEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lablUserEditErrMsg;
        private System.Windows.Forms.TextBox txtbUserEditing;
        private System.Windows.Forms.Label lablUsernameToEdit;
        private System.Windows.Forms.Label lablConfPasswordErrMsg;
        private System.Windows.Forms.Button bttnBlockUnblock;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox logintext;
        private System.Windows.Forms.CheckBox chkbBlockUnblockUser;
        private System.Windows.Forms.Label lablCurrentBlockStatus;
        private System.Windows.Forms.TextBox txtbCurrentBlockStatus;
    }
}

