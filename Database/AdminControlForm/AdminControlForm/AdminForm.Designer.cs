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
<<<<<<< HEAD
            this.tabControl = new System.Windows.Forms.TabControl();
            this.createUserTab = new System.Windows.Forms.TabPage();
=======
            this.components = new System.ComponentModel.Container();
            this.spaceUnionDataSet = new AdminControlForm.SpaceUnionDataSet();
            this.powerupBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.powerupTableAdapter = new AdminControlForm.SpaceUnionDataSetTableAdapters.PowerupTableAdapter();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnUpdatePwr = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.nudPwrValue = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.tbPowerupName = new System.Windows.Forms.TextBox();
            this.dgvPwrup = new System.Windows.Forms.DataGridView();
            this.powerupNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.powerupValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lablShipEditMaxSpeed = new System.Windows.Forms.Label();
            this.lablShipEditAccelerateErrMsg = new System.Windows.Forms.Label();
            this.lablShipEditTurnSpdErrMsg = new System.Windows.Forms.Label();
            this.bttnShipUpdate = new System.Windows.Forms.Button();
            this.bttnShipToEdit = new System.Windows.Forms.Button();
            this.txtbNewMaxSpeed = new System.Windows.Forms.TextBox();
            this.txtbNewAccelerate = new System.Windows.Forms.TextBox();
            this.txtbNewTurnSpeed = new System.Windows.Forms.TextBox();
            this.rtxtCurrentShipStats = new System.Windows.Forms.RichTextBox();
            this.txtbShipEditing = new System.Windows.Forms.TextBox();
            this.lablNewShipMaxSpeed = new System.Windows.Forms.Label();
            this.lablNewShipAccelerate = new System.Windows.Forms.Label();
            this.lablNewShipTurnSpeed = new System.Windows.Forms.Label();
            this.lablCurrentShipStats = new System.Windows.Forms.Label();
            this.lablShipEditing = new System.Windows.Forms.Label();
            this.lablEditShip = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnGetUserStats = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.nudFlagsCaptured = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.nudShip3 = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.nudShip2 = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudShip1 = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nudDied = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.nudKills = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudHits = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudShotsFired = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudLoses = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudWins = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.tbUserStatName = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lablMaxSpeedErrMsg = new System.Windows.Forms.Label();
            this.lablAccelErrMsg = new System.Windows.Forms.Label();
            this.lablTurnSpeedErrMsg = new System.Windows.Forms.Label();
            this.bttnAddShip = new System.Windows.Forms.Button();
            this.lablMaxSpeed = new System.Windows.Forms.Label();
            this.txtbMaxSpeed = new System.Windows.Forms.TextBox();
            this.txtbAccelerate = new System.Windows.Forms.TextBox();
            this.txtbTurnSpeed = new System.Windows.Forms.TextBox();
            this.txtbNewShipName = new System.Windows.Forms.TextBox();
            this.lablAccelerate = new System.Windows.Forms.Label();
            this.lablTurnSpeed = new System.Windows.Forms.Label();
            this.lablNewShipNameErrMsg = new System.Windows.Forms.Label();
            this.lablNewShipName = new System.Windows.Forms.Label();
            this.lablAddNewShip = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.editUserTab = new System.Windows.Forms.TabPage();
            this.lablCurrentBlockStatus = new System.Windows.Forms.Label();
            this.txtbCurrentBlockStatus = new System.Windows.Forms.TextBox();
            this.txtbUserEditing = new System.Windows.Forms.TextBox();
            this.txtbUserToEdit = new System.Windows.Forms.TextBox();
            this.chkbBlockUnblockUser = new System.Windows.Forms.CheckBox();
            this.bttnBlockUnblock = new System.Windows.Forms.Button();
            this.lablUserEditErrMsg = new System.Windows.Forms.Label();
            this.lablUsernameToEdit = new System.Windows.Forms.Label();
            this.bttnGetUserInfo = new System.Windows.Forms.Button();
            this.lablUserToEdit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.createUserTab = new System.Windows.Forms.TabPage();
            this.lablCreateNewUser = new System.Windows.Forms.Label();
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
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
<<<<<<< HEAD
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
=======
            this.txtbConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtbPassword = new System.Windows.Forms.TextBox();
            this.txtbUsername = new System.Windows.Forms.TextBox();
            this.lablEmail = new System.Windows.Forms.Label();
            this.lablConfirmPassword = new System.Windows.Forms.Label();
            this.lablPassword = new System.Windows.Forms.Label();
            this.lablUsername = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.lblUserStatError = new System.Windows.Forms.Label();
            this.btnDelPower = new System.Windows.Forms.Button();
            this.lblPwrError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spaceUnionDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerupBindingSource)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPwrValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPwrup)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlagsCaptured)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShip3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShip2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShip1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDied)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKills)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShotsFired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWins)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.editUserTab.SuspendLayout();
            this.createUserTab.SuspendLayout();
            this.tabControl.SuspendLayout();
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            this.SuspendLayout();
            // 
            // spaceUnionDataSet
            // 
<<<<<<< HEAD
            this.tabControl.Controls.Add(this.createUserTab);
            this.tabControl.Controls.Add(this.editUserTab);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(496, 512);
            this.tabControl.TabIndex = 0;
=======
            this.spaceUnionDataSet.DataSetName = "SpaceUnionDataSet";
            this.spaceUnionDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // powerupBindingSource
            // 
<<<<<<< HEAD
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
=======
            this.powerupBindingSource.DataMember = "Powerup";
            this.powerupBindingSource.DataSource = this.spaceUnionDataSet;
            // 
            // powerupTableAdapter
            // 
            this.powerupTableAdapter.ClearBeforeFill = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.lblPwrError);
            this.tabPage5.Controls.Add(this.btnDelPower);
            this.tabPage5.Controls.Add(this.btnUpdatePwr);
            this.tabPage5.Controls.Add(this.label16);
            this.tabPage5.Controls.Add(this.nudPwrValue);
            this.tabPage5.Controls.Add(this.label17);
            this.tabPage5.Controls.Add(this.tbPowerupName);
            this.tabPage5.Controls.Add(this.dgvPwrup);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(488, 486);
            this.tabPage5.TabIndex = 6;
            this.tabPage5.Text = "Powerups";
            this.tabPage5.UseVisualStyleBackColor = true;
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // btnUpdatePwr
            // 
            this.btnUpdatePwr.Enabled = false;
            this.btnUpdatePwr.Location = new System.Drawing.Point(262, 69);
            this.btnUpdatePwr.Name = "btnUpdatePwr";
            this.btnUpdatePwr.Size = new System.Drawing.Size(98, 23);
            this.btnUpdatePwr.TabIndex = 26;
            this.btnUpdatePwr.Text = "Update Powerup";
            this.btnUpdatePwr.UseVisualStyleBackColor = true;
            this.btnUpdatePwr.Click += new System.EventHandler(this.btnUpdatePwr_Click);
            // 
            // label16
            // 
<<<<<<< HEAD
            this.lablUsernameSpec2.AutoSize = true;
            this.lablUsernameSpec2.ForeColor = System.Drawing.Color.White;
            this.lablUsernameSpec2.Location = new System.Drawing.Point(45, 73);
            this.lablUsernameSpec2.Name = "lablUsernameSpec2";
            this.lablUsernameSpec2.Size = new System.Drawing.Size(181, 13);
            this.lablUsernameSpec2.TabIndex = 15;
            this.lablUsernameSpec2.Text = "Can only contain letters and numbers";
=======
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(259, 49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Powerup Value";
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // nudPwrValue
            // 
<<<<<<< HEAD
            this.lablPasswordSpec2.AutoSize = true;
            this.lablPasswordSpec2.ForeColor = System.Drawing.Color.White;
            this.lablPasswordSpec2.Location = new System.Drawing.Point(45, 163);
            this.lablPasswordSpec2.Name = "lablPasswordSpec2";
            this.lablPasswordSpec2.Size = new System.Drawing.Size(309, 13);
            this.lablPasswordSpec2.TabIndex = 14;
            this.lablPasswordSpec2.Text = "Must contain one digit as well as an upper- and lower-case letter";
=======
            this.nudPwrValue.Location = new System.Drawing.Point(362, 43);
            this.nudPwrValue.Name = "nudPwrValue";
            this.nudPwrValue.Size = new System.Drawing.Size(120, 20);
            this.nudPwrValue.TabIndex = 8;
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // label17
            // 
<<<<<<< HEAD
            this.lablPasswordSpec.AutoSize = true;
            this.lablPasswordSpec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablPasswordSpec.ForeColor = System.Drawing.Color.White;
            this.lablPasswordSpec.Location = new System.Drawing.Point(45, 150);
            this.lablPasswordSpec.Name = "lablPasswordSpec";
            this.lablPasswordSpec.Size = new System.Drawing.Size(81, 13);
            this.lablPasswordSpec.TabIndex = 13;
            this.lablPasswordSpec.Text = "4-32 characters";
=======
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(259, 18);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 13);
            this.label17.TabIndex = 7;
            this.label17.Text = "Powerup Name";
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // tbPowerupName
            // 
<<<<<<< HEAD
            this.lablUsernameSpec.AutoSize = true;
            this.lablUsernameSpec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablUsernameSpec.ForeColor = System.Drawing.Color.White;
            this.lablUsernameSpec.Location = new System.Drawing.Point(45, 60);
            this.lablUsernameSpec.Name = "lablUsernameSpec";
            this.lablUsernameSpec.Size = new System.Drawing.Size(81, 13);
            this.lablUsernameSpec.TabIndex = 12;
            this.lablUsernameSpec.Text = "4-16 characters";
=======
            this.tbPowerupName.Location = new System.Drawing.Point(339, 11);
            this.tbPowerupName.Name = "tbPowerupName";
            this.tbPowerupName.Size = new System.Drawing.Size(143, 20);
            this.tbPowerupName.TabIndex = 6;
            this.tbPowerupName.TextChanged += new System.EventHandler(this.tbPowerupName_TextChanged);
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // dgvPwrup
            // 
<<<<<<< HEAD
<<<<<<< HEAD
            this.lablEmailErrMsg.AutoSize = true;
            this.lablEmailErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablEmailErrMsg.Location = new System.Drawing.Point(98, 269);
            this.lablEmailErrMsg.Name = "lablEmailErrMsg";
            this.lablEmailErrMsg.Size = new System.Drawing.Size(78, 13);
            this.lablEmailErrMsg.TabIndex = 11;
            this.lablEmailErrMsg.Text = "Email error msg";
            this.lablEmailErrMsg.Visible = false;
=======
=======
            this.dgvPwrup.AllowUserToAddRows = false;
            this.dgvPwrup.AllowUserToDeleteRows = false;
            this.dgvPwrup.AllowUserToResizeColumns = false;
            this.dgvPwrup.AllowUserToResizeRows = false;
>>>>>>> fb46d967fe33a8cf5f815b9106581044f6fde28b
            this.dgvPwrup.AutoGenerateColumns = false;
            this.dgvPwrup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPwrup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.powerupNameDataGridViewTextBoxColumn,
            this.powerupValueDataGridViewTextBoxColumn});
            this.dgvPwrup.DataSource = this.powerupBindingSource;
            this.dgvPwrup.Enabled = false;
            this.dgvPwrup.Location = new System.Drawing.Point(9, 7);
            this.dgvPwrup.MultiSelect = false;
            this.dgvPwrup.Name = "dgvPwrup";
            this.dgvPwrup.ReadOnly = true;
            this.dgvPwrup.Size = new System.Drawing.Size(244, 471);
            this.dgvPwrup.TabIndex = 0;
<<<<<<< HEAD
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
=======
            this.dgvPwrup.TabStop = false;
            // 
            // powerupNameDataGridViewTextBoxColumn
            // 
            this.powerupNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.powerupNameDataGridViewTextBoxColumn.DataPropertyName = "PowerupName";
            this.powerupNameDataGridViewTextBoxColumn.HeaderText = "PowerupName";
            this.powerupNameDataGridViewTextBoxColumn.Name = "powerupNameDataGridViewTextBoxColumn";
            this.powerupNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // powerupValueDataGridViewTextBoxColumn
            // 
            this.powerupValueDataGridViewTextBoxColumn.DataPropertyName = "PowerupValue";
            this.powerupValueDataGridViewTextBoxColumn.HeaderText = "PowerupValue";
            this.powerupValueDataGridViewTextBoxColumn.Name = "powerupValueDataGridViewTextBoxColumn";
            this.powerupValueDataGridViewTextBoxColumn.ReadOnly = true;
>>>>>>> fb46d967fe33a8cf5f815b9106581044f6fde28b
            // 
            // tabPage4
            // 
<<<<<<< HEAD
            this.lablPasswordErrMsg.AutoSize = true;
            this.lablPasswordErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablPasswordErrMsg.Location = new System.Drawing.Point(125, 109);
            this.lablPasswordErrMsg.Name = "lablPasswordErrMsg";
            this.lablPasswordErrMsg.Size = new System.Drawing.Size(99, 13);
            this.lablPasswordErrMsg.TabIndex = 10;
            this.lablPasswordErrMsg.Text = "Password error msg";
            this.lablPasswordErrMsg.Visible = false;
=======
            this.tabPage4.BackgroundImage = global::AdminControlForm.Properties.Resources.dark_space;
            this.tabPage4.Controls.Add(this.lablShipEditMaxSpeed);
            this.tabPage4.Controls.Add(this.lablShipEditAccelerateErrMsg);
            this.tabPage4.Controls.Add(this.lablShipEditTurnSpdErrMsg);
            this.tabPage4.Controls.Add(this.bttnShipUpdate);
            this.tabPage4.Controls.Add(this.bttnShipToEdit);
            this.tabPage4.Controls.Add(this.txtbNewMaxSpeed);
            this.tabPage4.Controls.Add(this.txtbNewAccelerate);
            this.tabPage4.Controls.Add(this.txtbNewTurnSpeed);
            this.tabPage4.Controls.Add(this.rtxtCurrentShipStats);
            this.tabPage4.Controls.Add(this.txtbShipEditing);
            this.tabPage4.Controls.Add(this.lablNewShipMaxSpeed);
            this.tabPage4.Controls.Add(this.lablNewShipAccelerate);
            this.tabPage4.Controls.Add(this.lablNewShipTurnSpeed);
            this.tabPage4.Controls.Add(this.lablCurrentShipStats);
            this.tabPage4.Controls.Add(this.lablShipEditing);
            this.tabPage4.Controls.Add(this.lablEditShip);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(488, 486);
            this.tabPage4.TabIndex = 5;
            this.tabPage4.Text = "Edit Ship";
            this.tabPage4.UseVisualStyleBackColor = true;
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // lablShipEditMaxSpeed
            // 
<<<<<<< HEAD
            this.lablUsernameErrMsg.AutoSize = true;
            this.lablUsernameErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablUsernameErrMsg.Location = new System.Drawing.Point(132, 19);
            this.lablUsernameErrMsg.Name = "lablUsernameErrMsg";
            this.lablUsernameErrMsg.Size = new System.Drawing.Size(101, 13);
            this.lablUsernameErrMsg.TabIndex = 9;
            this.lablUsernameErrMsg.Text = "Username error msg";
            this.lablUsernameErrMsg.Visible = false;
=======
            this.lablShipEditMaxSpeed.AutoSize = true;
            this.lablShipEditMaxSpeed.ForeColor = System.Drawing.Color.Red;
            this.lablShipEditMaxSpeed.Location = new System.Drawing.Point(173, 346);
            this.lablShipEditMaxSpeed.Name = "lablShipEditMaxSpeed";
            this.lablShipEditMaxSpeed.Size = new System.Drawing.Size(110, 13);
            this.lablShipEditMaxSpeed.TabIndex = 16;
            this.lablShipEditMaxSpeed.Text = "err msg for max speed";
            this.lablShipEditMaxSpeed.Visible = false;
            this.lablShipEditMaxSpeed.TextChanged += new System.EventHandler(this.validateMaxSpeedEdit);
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // lablShipEditAccelerateErrMsg
            // 
<<<<<<< HEAD
            this.txtbEmail.Location = new System.Drawing.Point(48, 287);
            this.txtbEmail.Name = "txtbEmail";
            this.txtbEmail.Size = new System.Drawing.Size(223, 20);
            this.txtbEmail.TabIndex = 7;
=======
            this.lablShipEditAccelerateErrMsg.AutoSize = true;
            this.lablShipEditAccelerateErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablShipEditAccelerateErrMsg.Location = new System.Drawing.Point(235, 283);
            this.lablShipEditAccelerateErrMsg.Name = "lablShipEditAccelerateErrMsg";
            this.lablShipEditAccelerateErrMsg.Size = new System.Drawing.Size(109, 13);
            this.lablShipEditAccelerateErrMsg.TabIndex = 15;
            this.lablShipEditAccelerateErrMsg.Text = "err msg for accelerate";
            this.lablShipEditAccelerateErrMsg.Visible = false;
            this.lablShipEditAccelerateErrMsg.TextChanged += new System.EventHandler(this.validateAccelerationEdit);
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // lablShipEditTurnSpdErrMsg
            // 
<<<<<<< HEAD
            this.lablEmail.AutoSize = true;
            this.lablEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablEmail.ForeColor = System.Drawing.Color.White;
            this.lablEmail.Location = new System.Drawing.Point(45, 267);
            this.lablEmail.Name = "lablEmail";
            this.lablEmail.Size = new System.Drawing.Size(47, 17);
            this.lablEmail.TabIndex = 6;
            this.lablEmail.Text = "Email";
=======
            this.lablShipEditTurnSpdErrMsg.AutoSize = true;
            this.lablShipEditTurnSpdErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablShipEditTurnSpdErrMsg.Location = new System.Drawing.Point(179, 222);
            this.lablShipEditTurnSpdErrMsg.Name = "lablShipEditTurnSpdErrMsg";
            this.lablShipEditTurnSpdErrMsg.Size = new System.Drawing.Size(109, 13);
            this.lablShipEditTurnSpdErrMsg.TabIndex = 14;
            this.lablShipEditTurnSpdErrMsg.Text = "err msg for turn speed";
            this.lablShipEditTurnSpdErrMsg.Visible = false;
            this.lablShipEditTurnSpdErrMsg.TextChanged += new System.EventHandler(this.validateTurnSpeedEdit);
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // bttnShipUpdate
            // 
<<<<<<< HEAD
            this.txtbConfirmPassword.Location = new System.Drawing.Point(48, 221);
            this.txtbConfirmPassword.Name = "txtbConfirmPassword";
            this.txtbConfirmPassword.Size = new System.Drawing.Size(223, 20);
            this.txtbConfirmPassword.TabIndex = 5;
            this.txtbConfirmPassword.TextChanged += new System.EventHandler(this.validateConfPassword);
=======
            this.bttnShipUpdate.Location = new System.Drawing.Point(148, 402);
            this.bttnShipUpdate.Name = "bttnShipUpdate";
            this.bttnShipUpdate.Size = new System.Drawing.Size(91, 35);
            this.bttnShipUpdate.TabIndex = 13;
            this.bttnShipUpdate.Text = "Update Ship";
            this.bttnShipUpdate.UseVisualStyleBackColor = true;
            this.bttnShipUpdate.Click += new System.EventHandler(this.bttnShipUpdate_Click);
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // bttnShipToEdit
            // 
<<<<<<< HEAD
            this.lablConfirmPassword.AutoSize = true;
            this.lablConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablConfirmPassword.ForeColor = System.Drawing.Color.White;
            this.lablConfirmPassword.Location = new System.Drawing.Point(45, 201);
            this.lablConfirmPassword.Name = "lablConfirmPassword";
            this.lablConfirmPassword.Size = new System.Drawing.Size(137, 17);
            this.lablConfirmPassword.TabIndex = 4;
            this.lablConfirmPassword.Text = "Confirm Password";
=======
            this.bttnShipToEdit.Location = new System.Drawing.Point(124, 128);
            this.bttnShipToEdit.Name = "bttnShipToEdit";
            this.bttnShipToEdit.Size = new System.Drawing.Size(115, 23);
            this.bttnShipToEdit.TabIndex = 12;
            this.bttnShipToEdit.Text = "Retrieve Ship Stats";
            this.bttnShipToEdit.UseVisualStyleBackColor = true;
            this.bttnShipToEdit.Click += new System.EventHandler(this.bttnShipToEdit_Click);
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // txtbNewMaxSpeed
            // 
<<<<<<< HEAD
            this.txtbPassword.Location = new System.Drawing.Point(48, 127);
            this.txtbPassword.Name = "txtbPassword";
            this.txtbPassword.Size = new System.Drawing.Size(223, 20);
            this.txtbPassword.TabIndex = 3;
            this.txtbPassword.TextChanged += new System.EventHandler(this.validatePassword);
=======
            this.txtbNewMaxSpeed.Location = new System.Drawing.Point(45, 364);
            this.txtbNewMaxSpeed.Name = "txtbNewMaxSpeed";
            this.txtbNewMaxSpeed.Size = new System.Drawing.Size(194, 20);
            this.txtbNewMaxSpeed.TabIndex = 11;
            this.txtbNewMaxSpeed.TextChanged += new System.EventHandler(this.validateMaxSpeedEdit);
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // txtbNewAccelerate
            // 
<<<<<<< HEAD
            this.lablPassword.AutoSize = true;
            this.lablPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablPassword.ForeColor = System.Drawing.Color.White;
            this.lablPassword.Location = new System.Drawing.Point(45, 107);
            this.lablPassword.Name = "lablPassword";
            this.lablPassword.Size = new System.Drawing.Size(77, 17);
            this.lablPassword.TabIndex = 2;
            this.lablPassword.Text = "Password";
=======
            this.txtbNewAccelerate.Location = new System.Drawing.Point(45, 301);
            this.txtbNewAccelerate.Name = "txtbNewAccelerate";
            this.txtbNewAccelerate.Size = new System.Drawing.Size(194, 20);
            this.txtbNewAccelerate.TabIndex = 9;
            this.txtbNewAccelerate.TextChanged += new System.EventHandler(this.validateAccelerationEdit);
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // txtbNewTurnSpeed
            // 
<<<<<<< HEAD
            this.txtbUsername.Location = new System.Drawing.Point(48, 37);
            this.txtbUsername.Name = "txtbUsername";
            this.txtbUsername.Size = new System.Drawing.Size(223, 20);
            this.txtbUsername.TabIndex = 1;
            this.txtbUsername.TextChanged += new System.EventHandler(this.validateUsername);
=======
            this.txtbNewTurnSpeed.Location = new System.Drawing.Point(45, 240);
            this.txtbNewTurnSpeed.Name = "txtbNewTurnSpeed";
            this.txtbNewTurnSpeed.Size = new System.Drawing.Size(194, 20);
            this.txtbNewTurnSpeed.TabIndex = 7;
            this.txtbNewTurnSpeed.TextChanged += new System.EventHandler(this.validateTurnSpeedEdit);
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // rtxtCurrentShipStats
            // 
<<<<<<< HEAD
            this.lablUsername.AutoSize = true;
            this.lablUsername.BackColor = System.Drawing.Color.Transparent;
            this.lablUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablUsername.ForeColor = System.Drawing.Color.White;
            this.lablUsername.Location = new System.Drawing.Point(45, 17);
            this.lablUsername.Name = "lablUsername";
            this.lablUsername.Size = new System.Drawing.Size(81, 17);
            this.lablUsername.TabIndex = 0;
            this.lablUsername.Text = "Username";
=======
            this.rtxtCurrentShipStats.Location = new System.Drawing.Point(277, 81);
            this.rtxtCurrentShipStats.Name = "rtxtCurrentShipStats";
            this.rtxtCurrentShipStats.ReadOnly = true;
            this.rtxtCurrentShipStats.Size = new System.Drawing.Size(194, 70);
            this.rtxtCurrentShipStats.TabIndex = 5;
            this.rtxtCurrentShipStats.Text = "";
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // txtbShipEditing
            // 
            this.txtbShipEditing.Location = new System.Drawing.Point(45, 82);
            this.txtbShipEditing.Name = "txtbShipEditing";
            this.txtbShipEditing.Size = new System.Drawing.Size(194, 20);
            this.txtbShipEditing.TabIndex = 3;
            // 
<<<<<<< HEAD
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
=======
            // lablNewShipMaxSpeed
            // 
            this.lablNewShipMaxSpeed.AutoSize = true;
            this.lablNewShipMaxSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablNewShipMaxSpeed.ForeColor = System.Drawing.Color.White;
            this.lablNewShipMaxSpeed.Location = new System.Drawing.Point(45, 344);
            this.lablNewShipMaxSpeed.Name = "lablNewShipMaxSpeed";
            this.lablNewShipMaxSpeed.Size = new System.Drawing.Size(122, 17);
            this.lablNewShipMaxSpeed.TabIndex = 10;
            this.lablNewShipMaxSpeed.Text = "New Max Speed";
            // 
            // lablNewShipAccelerate
            // 
            this.lablNewShipAccelerate.AutoSize = true;
            this.lablNewShipAccelerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablNewShipAccelerate.ForeColor = System.Drawing.Color.White;
            this.lablNewShipAccelerate.Location = new System.Drawing.Point(45, 281);
            this.lablNewShipAccelerate.Name = "lablNewShipAccelerate";
            this.lablNewShipAccelerate.Size = new System.Drawing.Size(184, 17);
            this.lablNewShipAccelerate.TabIndex = 8;
            this.lablNewShipAccelerate.Text = "New Acceleration Speed";
            // 
            // lablNewShipTurnSpeed
            // 
            this.lablNewShipTurnSpeed.AutoSize = true;
            this.lablNewShipTurnSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablNewShipTurnSpeed.ForeColor = System.Drawing.Color.White;
            this.lablNewShipTurnSpeed.Location = new System.Drawing.Point(45, 220);
            this.lablNewShipTurnSpeed.Name = "lablNewShipTurnSpeed";
            this.lablNewShipTurnSpeed.Size = new System.Drawing.Size(128, 17);
            this.lablNewShipTurnSpeed.TabIndex = 6;
            this.lablNewShipTurnSpeed.Text = "New Turn Speed";
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // lablCurrentShipStats
            // 
            this.lablCurrentShipStats.AutoSize = true;
            this.lablCurrentShipStats.BackColor = System.Drawing.Color.Transparent;
            this.lablCurrentShipStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablCurrentShipStats.ForeColor = System.Drawing.Color.White;
            this.lablCurrentShipStats.Location = new System.Drawing.Point(280, 61);
            this.lablCurrentShipStats.Name = "lablCurrentShipStats";
            this.lablCurrentShipStats.Size = new System.Drawing.Size(141, 17);
            this.lablCurrentShipStats.TabIndex = 4;
            this.lablCurrentShipStats.Text = "Current Ship Stats";
            // 
            // lablShipEditing
            // 
<<<<<<< HEAD
            this.lablUserEditErrMsg.AutoSize = true;
            this.lablUserEditErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablUserEditErrMsg.Location = new System.Drawing.Point(251, 70);
            this.lablUserEditErrMsg.Name = "lablUserEditErrMsg";
            this.lablUserEditErrMsg.Size = new System.Drawing.Size(132, 13);
            this.lablUserEditErrMsg.TabIndex = 9;
            this.lablUserEditErrMsg.Text = "Error entering in Username";
            this.lablUserEditErrMsg.Visible = false;
=======
            this.lablShipEditing.AutoSize = true;
            this.lablShipEditing.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablShipEditing.ForeColor = System.Drawing.Color.White;
            this.lablShipEditing.Location = new System.Drawing.Point(45, 61);
            this.lablShipEditing.Name = "lablShipEditing";
            this.lablShipEditing.Size = new System.Drawing.Size(185, 17);
            this.lablShipEditing.TabIndex = 2;
            this.lablShipEditing.Text = "Name of the Ship to Edit";
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // lablEditShip
            // 
            this.lablEditShip.AutoSize = true;
            this.lablEditShip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablEditShip.ForeColor = System.Drawing.Color.White;
            this.lablEditShip.Location = new System.Drawing.Point(19, 20);
            this.lablEditShip.Name = "lablEditShip";
            this.lablEditShip.Size = new System.Drawing.Size(97, 20);
            this.lablEditShip.TabIndex = 1;
            this.lablEditShip.Text = "Edit a Ship";
            // 
            // tabPage3
            // 
<<<<<<< HEAD
<<<<<<< HEAD
            this.lablUsernameToEdit.AutoSize = true;
            this.lablUsernameToEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablUsernameToEdit.ForeColor = System.Drawing.Color.White;
            this.lablUsernameToEdit.Location = new System.Drawing.Point(17, 207);
            this.lablUsernameToEdit.Name = "lablUsernameToEdit";
            this.lablUsernameToEdit.Size = new System.Drawing.Size(73, 15);
            this.lablUsernameToEdit.TabIndex = 4;
            this.lablUsernameToEdit.Text = "Username";
=======
=======
            this.tabPage3.Controls.Add(this.lblUserStatError);
>>>>>>> fb46d967fe33a8cf5f815b9106581044f6fde28b
            this.tabPage3.Controls.Add(this.btnGetUserStats);
            this.tabPage3.Controls.Add(this.btnUpdate);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.nudFlagsCaptured);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.nudShip3);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.nudShip2);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.nudShip1);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.nudDied);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.nudKills);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.nudHits);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.nudShotsFired);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.nudLoses);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.nudWins);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.tbUserStatName);
            this.tabPage3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(488, 486);
            this.tabPage3.TabIndex = 4;
            this.tabPage3.Text = "UserStats";
            this.tabPage3.UseVisualStyleBackColor = true;
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // btnGetUserStats
            // 
            this.btnGetUserStats.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGetUserStats.Location = new System.Drawing.Point(338, 142);
            this.btnGetUserStats.Name = "btnGetUserStats";
            this.btnGetUserStats.Size = new System.Drawing.Size(101, 23);
            this.btnGetUserStats.TabIndex = 26;
            this.btnGetUserStats.Text = "Get User Stats";
            this.btnGetUserStats.UseVisualStyleBackColor = true;
            this.btnGetUserStats.Click += new System.EventHandler(this.btnGetUserStats_Click);
            // 
            // btnUpdate
            // 
<<<<<<< HEAD
            this.bttnGetUserInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnGetUserInfo.Location = new System.Drawing.Point(125, 93);
            this.bttnGetUserInfo.Name = "bttnGetUserInfo";
            this.bttnGetUserInfo.Size = new System.Drawing.Size(120, 28);
            this.bttnGetUserInfo.TabIndex = 3;
            this.bttnGetUserInfo.Text = "Retrieve User Info";
            this.bttnGetUserInfo.UseVisualStyleBackColor = true;
            this.bttnGetUserInfo.Click += new System.EventHandler(this.bttnGetUserInfo_Click);
=======
            this.btnUpdate.Enabled = false;
            this.btnUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUpdate.Location = new System.Drawing.Point(200, 357);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(88, 23);
            this.btnUpdate.TabIndex = 25;
            this.btnUpdate.Text = "Update Stats";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // label14
            // 
<<<<<<< HEAD
            this.lablUserToEdit.AutoSize = true;
            this.lablUserToEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablUserToEdit.ForeColor = System.Drawing.Color.White;
            this.lablUserToEdit.Location = new System.Drawing.Point(16, 178);
            this.lablUserToEdit.Name = "lablUserToEdit";
            this.lablUserToEdit.Size = new System.Drawing.Size(356, 20);
            this.lablUserToEdit.TabIndex = 2;
            this.lablUserToEdit.Text = "Info of the User you Want to Block/Unblock";
=======
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(248, 321);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 15);
            this.label14.TabIndex = 23;
            this.label14.Text = "flags captured";
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // nudFlagsCaptured
            // 
<<<<<<< HEAD
            this.txtbUserToEdit.Location = new System.Drawing.Point(13, 67);
            this.txtbUserToEdit.Name = "txtbUserToEdit";
            this.txtbUserToEdit.Size = new System.Drawing.Size(232, 20);
            this.txtbUserToEdit.TabIndex = 1;
=======
            this.nudFlagsCaptured.Location = new System.Drawing.Point(338, 316);
            this.nudFlagsCaptured.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudFlagsCaptured.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.nudFlagsCaptured.Name = "nudFlagsCaptured";
            this.nudFlagsCaptured.Size = new System.Drawing.Size(120, 20);
            this.nudFlagsCaptured.TabIndex = 22;
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            // label13
            // 
<<<<<<< HEAD
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
=======
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(246, 295);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 15);
            this.label13.TabIndex = 21;
            this.label13.Text = "ship3";
            // 
            // nudShip3
            // 
            this.nudShip3.Location = new System.Drawing.Point(336, 290);
            this.nudShip3.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudShip3.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.nudShip3.Name = "nudShip3";
            this.nudShip3.Size = new System.Drawing.Size(120, 20);
            this.nudShip3.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(246, 269);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 15);
            this.label12.TabIndex = 19;
            this.label12.Text = "ship2";
            // 
            // nudShip2
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            this.nudShip2.Location = new System.Drawing.Point(336, 264);
            this.nudShip2.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudShip2.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.nudShip2.Name = "nudShip2";
            this.nudShip2.Size = new System.Drawing.Size(120, 20);
            this.nudShip2.TabIndex = 18;
            // 
<<<<<<< HEAD
            this.button1.Location = new System.Drawing.Point(80, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Read info from Database";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
=======
            // label11
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(246, 243);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 15);
            this.label11.TabIndex = 17;
            this.label11.Text = "ship1";
            // 
<<<<<<< HEAD
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
=======
            // nudShip1
            // 
            this.nudShip1.Location = new System.Drawing.Point(336, 238);
            this.nudShip1.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudShip1.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.nudShip1.Name = "nudShip1";
            this.nudShip1.Size = new System.Drawing.Size(120, 20);
            this.nudShip1.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(246, 217);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 15);
            this.label10.TabIndex = 15;
            this.label10.Text = "died";
            // 
            // nudDied
            // 
            this.nudDied.Location = new System.Drawing.Point(336, 212);
            this.nudDied.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudDied.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.nudDied.Name = "nudDied";
            this.nudDied.Size = new System.Drawing.Size(120, 20);
            this.nudDied.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(39, 321);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 15);
            this.label9.TabIndex = 13;
            this.label9.Text = "kills";
            // 
            // nudKills
            // 
            this.nudKills.Location = new System.Drawing.Point(108, 316);
            this.nudKills.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudKills.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.nudKills.Name = "nudKills";
            this.nudKills.Size = new System.Drawing.Size(120, 20);
            this.nudKills.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(39, 295);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 15);
            this.label8.TabIndex = 11;
            this.label8.Text = "hits";
            // 
            // nudHits
            // 
            this.nudHits.Location = new System.Drawing.Point(108, 290);
            this.nudHits.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudHits.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.nudHits.Name = "nudHits";
            this.nudHits.Size = new System.Drawing.Size(120, 20);
            this.nudHits.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(39, 269);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 15);
            this.label7.TabIndex = 9;
            this.label7.Text = "shots fired";
            // 
            // nudShotsFired
            // 
            this.nudShotsFired.Location = new System.Drawing.Point(108, 264);
            this.nudShotsFired.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudShotsFired.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.nudShotsFired.Name = "nudShotsFired";
            this.nudShotsFired.Size = new System.Drawing.Size(120, 20);
            this.nudShotsFired.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(39, 243);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "loses";
            // 
            // nudLoses
            // 
            this.nudLoses.Location = new System.Drawing.Point(108, 238);
            this.nudLoses.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudLoses.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.nudLoses.Name = "nudLoses";
            this.nudLoses.Size = new System.Drawing.Size(120, 20);
            this.nudLoses.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(39, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "wins";
            // 
            // nudWins
            // 
            this.nudWins.Location = new System.Drawing.Point(108, 212);
            this.nudWins.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudWins.Minimum = new decimal(new int[] {
            999999,
            0,
            0,
            -2147483648});
            this.nudWins.Name = "nudWins";
            this.nudWins.Size = new System.Drawing.Size(120, 20);
            this.nudWins.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(39, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "username";
            // 
            // tbUserStatName
            // 
            this.tbUserStatName.Location = new System.Drawing.Point(108, 145);
            this.tbUserStatName.Name = "tbUserStatName";
            this.tbUserStatName.Size = new System.Drawing.Size(224, 20);
            this.tbUserStatName.TabIndex = 2;
            this.tbUserStatName.TextChanged += new System.EventHandler(this.tbUserStatName_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackgroundImage = global::AdminControlForm.Properties.Resources.dark_space;
            this.tabPage2.Controls.Add(this.lablMaxSpeedErrMsg);
            this.tabPage2.Controls.Add(this.lablAccelErrMsg);
            this.tabPage2.Controls.Add(this.lablTurnSpeedErrMsg);
            this.tabPage2.Controls.Add(this.bttnAddShip);
            this.tabPage2.Controls.Add(this.lablMaxSpeed);
            this.tabPage2.Controls.Add(this.txtbMaxSpeed);
            this.tabPage2.Controls.Add(this.txtbAccelerate);
            this.tabPage2.Controls.Add(this.txtbTurnSpeed);
            this.tabPage2.Controls.Add(this.txtbNewShipName);
            this.tabPage2.Controls.Add(this.lablAccelerate);
            this.tabPage2.Controls.Add(this.lablTurnSpeed);
            this.tabPage2.Controls.Add(this.lablNewShipNameErrMsg);
            this.tabPage2.Controls.Add(this.lablNewShipName);
            this.tabPage2.Controls.Add(this.lablAddNewShip);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(488, 486);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Add Ship";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lablMaxSpeedErrMsg
            // 
            this.lablMaxSpeedErrMsg.AutoSize = true;
            this.lablMaxSpeedErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablMaxSpeedErrMsg.Location = new System.Drawing.Point(190, 341);
            this.lablMaxSpeedErrMsg.Name = "lablMaxSpeedErrMsg";
            this.lablMaxSpeedErrMsg.Size = new System.Drawing.Size(95, 13);
            this.lablMaxSpeedErrMsg.TabIndex = 14;
            this.lablMaxSpeedErrMsg.Text = "max speed err msg";
            this.lablMaxSpeedErrMsg.Visible = false;
            // 
            // lablAccelErrMsg
            // 
            this.lablAccelErrMsg.AutoSize = true;
            this.lablAccelErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablAccelErrMsg.Location = new System.Drawing.Point(200, 242);
            this.lablAccelErrMsg.Name = "lablAccelErrMsg";
            this.lablAccelErrMsg.Size = new System.Drawing.Size(70, 13);
            this.lablAccelErrMsg.TabIndex = 13;
            this.lablAccelErrMsg.Text = "accel err msg";
            this.lablAccelErrMsg.Visible = false;
            // 
            // lablTurnSpeedErrMsg
            // 
            this.lablTurnSpeedErrMsg.AutoSize = true;
            this.lablTurnSpeedErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablTurnSpeedErrMsg.Location = new System.Drawing.Point(197, 148);
            this.lablTurnSpeedErrMsg.Name = "lablTurnSpeedErrMsg";
            this.lablTurnSpeedErrMsg.Size = new System.Drawing.Size(94, 13);
            this.lablTurnSpeedErrMsg.TabIndex = 12;
            this.lablTurnSpeedErrMsg.Text = "turn speed err msg";
            this.lablTurnSpeedErrMsg.Visible = false;
            // 
            // bttnAddShip
            // 
            this.bttnAddShip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnAddShip.Location = new System.Drawing.Point(140, 417);
            this.bttnAddShip.Name = "bttnAddShip";
            this.bttnAddShip.Size = new System.Drawing.Size(114, 40);
            this.bttnAddShip.TabIndex = 11;
            this.bttnAddShip.Text = "Add Ship";
            this.bttnAddShip.UseVisualStyleBackColor = true;
            this.bttnAddShip.Click += new System.EventHandler(this.bttnAddShip_Click);
            // 
            // lablMaxSpeed
            // 
            this.lablMaxSpeed.AutoSize = true;
            this.lablMaxSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablMaxSpeed.ForeColor = System.Drawing.Color.White;
            this.lablMaxSpeed.Location = new System.Drawing.Point(48, 337);
            this.lablMaxSpeed.Name = "lablMaxSpeed";
            this.lablMaxSpeed.Size = new System.Drawing.Size(136, 17);
            this.lablMaxSpeed.TabIndex = 10;
            this.lablMaxSpeed.Text = "Max Speed (float)";
            // 
            // txtbMaxSpeed
            // 
            this.txtbMaxSpeed.Location = new System.Drawing.Point(45, 354);
            this.txtbMaxSpeed.Name = "txtbMaxSpeed";
            this.txtbMaxSpeed.Size = new System.Drawing.Size(210, 20);
            this.txtbMaxSpeed.TabIndex = 9;
            this.txtbMaxSpeed.TextChanged += new System.EventHandler(this.validateMaxSpeed);
            // 
            // txtbAccelerate
            // 
            this.txtbAccelerate.Location = new System.Drawing.Point(45, 259);
            this.txtbAccelerate.Name = "txtbAccelerate";
            this.txtbAccelerate.Size = new System.Drawing.Size(210, 20);
            this.txtbAccelerate.TabIndex = 7;
            this.txtbAccelerate.TextChanged += new System.EventHandler(this.validateAcceleration);
            // 
            // txtbTurnSpeed
            // 
            this.txtbTurnSpeed.Location = new System.Drawing.Point(45, 165);
            this.txtbTurnSpeed.Name = "txtbTurnSpeed";
            this.txtbTurnSpeed.Size = new System.Drawing.Size(210, 20);
            this.txtbTurnSpeed.TabIndex = 5;
            this.txtbTurnSpeed.TextChanged += new System.EventHandler(this.validateTurnSpeed);
            // 
            // txtbNewShipName
            // 
            this.txtbNewShipName.Location = new System.Drawing.Point(45, 74);
            this.txtbNewShipName.Name = "txtbNewShipName";
            this.txtbNewShipName.Size = new System.Drawing.Size(210, 20);
            this.txtbNewShipName.TabIndex = 3;
            this.txtbNewShipName.TextChanged += new System.EventHandler(this.validateShipName);
            // 
            // lablAccelerate
            // 
            this.lablAccelerate.AutoSize = true;
            this.lablAccelerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablAccelerate.ForeColor = System.Drawing.Color.White;
            this.lablAccelerate.Location = new System.Drawing.Point(48, 242);
            this.lablAccelerate.Name = "lablAccelerate";
            this.lablAccelerate.Size = new System.Drawing.Size(147, 17);
            this.lablAccelerate.TabIndex = 8;
            this.lablAccelerate.Text = "Acceleration (float)";
            // 
            // lablTurnSpeed
            // 
            this.lablTurnSpeed.AutoSize = true;
            this.lablTurnSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablTurnSpeed.ForeColor = System.Drawing.Color.White;
            this.lablTurnSpeed.Location = new System.Drawing.Point(48, 148);
            this.lablTurnSpeed.Name = "lablTurnSpeed";
            this.lablTurnSpeed.Size = new System.Drawing.Size(142, 17);
            this.lablTurnSpeed.TabIndex = 6;
            this.lablTurnSpeed.Text = "Turn Speed (float)";
            // 
            // lablNewShipNameErrMsg
            // 
            this.lablNewShipNameErrMsg.AutoSize = true;
            this.lablNewShipNameErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablNewShipNameErrMsg.Location = new System.Drawing.Point(140, 59);
            this.lablNewShipNameErrMsg.Name = "lablNewShipNameErrMsg";
            this.lablNewShipNameErrMsg.Size = new System.Drawing.Size(92, 13);
            this.lablNewShipNameErrMsg.TabIndex = 4;
            this.lablNewShipNameErrMsg.Text = "ship name err msg";
            this.lablNewShipNameErrMsg.Visible = false;
            // 
            // lablNewShipName
            // 
            this.lablNewShipName.AutoSize = true;
            this.lablNewShipName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablNewShipName.ForeColor = System.Drawing.Color.White;
            this.lablNewShipName.Location = new System.Drawing.Point(48, 57);
            this.lablNewShipName.Name = "lablNewShipName";
            this.lablNewShipName.Size = new System.Drawing.Size(86, 17);
            this.lablNewShipName.TabIndex = 2;
            this.lablNewShipName.Text = "Ship Name";
            // 
            // lablAddNewShip
            // 
            this.lablAddNewShip.AutoSize = true;
            this.lablAddNewShip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablAddNewShip.ForeColor = System.Drawing.Color.White;
            this.lablAddNewShip.Location = new System.Drawing.Point(22, 23);
            this.lablAddNewShip.Name = "lablAddNewShip";
            this.lablAddNewShip.Size = new System.Drawing.Size(136, 20);
            this.lablAddNewShip.TabIndex = 1;
            this.lablAddNewShip.Text = "Add a New Ship";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // editUserTab
            // 
            this.editUserTab.BackColor = System.Drawing.Color.Transparent;
            this.editUserTab.BackgroundImage = global::AdminControlForm.Properties.Resources.dark_space;
            this.editUserTab.Controls.Add(this.lablCurrentBlockStatus);
            this.editUserTab.Controls.Add(this.txtbCurrentBlockStatus);
            this.editUserTab.Controls.Add(this.txtbUserEditing);
            this.editUserTab.Controls.Add(this.txtbUserToEdit);
            this.editUserTab.Controls.Add(this.chkbBlockUnblockUser);
            this.editUserTab.Controls.Add(this.bttnBlockUnblock);
            this.editUserTab.Controls.Add(this.lablUserEditErrMsg);
            this.editUserTab.Controls.Add(this.lablUsernameToEdit);
            this.editUserTab.Controls.Add(this.bttnGetUserInfo);
            this.editUserTab.Controls.Add(this.lablUserToEdit);
            this.editUserTab.Controls.Add(this.label1);
            this.editUserTab.Location = new System.Drawing.Point(4, 22);
            this.editUserTab.Name = "editUserTab";
            this.editUserTab.Padding = new System.Windows.Forms.Padding(3);
            this.editUserTab.Size = new System.Drawing.Size(488, 486);
            this.editUserTab.TabIndex = 1;
            this.editUserTab.Text = "User block/unblock";
            // 
            // lablCurrentBlockStatus
            // 
            this.lablCurrentBlockStatus.AutoSize = true;
            this.lablCurrentBlockStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablCurrentBlockStatus.ForeColor = System.Drawing.Color.White;
            this.lablCurrentBlockStatus.Location = new System.Drawing.Point(20, 271);
            this.lablCurrentBlockStatus.Name = "lablCurrentBlockStatus";
            this.lablCurrentBlockStatus.Size = new System.Drawing.Size(137, 15);
            this.lablCurrentBlockStatus.TabIndex = 14;
            this.lablCurrentBlockStatus.Text = "Current Block Status";
            // 
            // txtbCurrentBlockStatus
            // 
            this.txtbCurrentBlockStatus.Location = new System.Drawing.Point(20, 286);
            this.txtbCurrentBlockStatus.Name = "txtbCurrentBlockStatus";
            this.txtbCurrentBlockStatus.ReadOnly = true;
            this.txtbCurrentBlockStatus.Size = new System.Drawing.Size(161, 20);
            this.txtbCurrentBlockStatus.TabIndex = 13;
            // 
            // txtbUserEditing
            // 
            this.txtbUserEditing.Location = new System.Drawing.Point(20, 225);
            this.txtbUserEditing.Name = "txtbUserEditing";
            this.txtbUserEditing.ReadOnly = true;
            this.txtbUserEditing.Size = new System.Drawing.Size(161, 20);
            this.txtbUserEditing.TabIndex = 5;
            // 
            // txtbUserToEdit
            // 
            this.txtbUserToEdit.Location = new System.Drawing.Point(13, 46);
            this.txtbUserToEdit.Name = "txtbUserToEdit";
            this.txtbUserToEdit.Size = new System.Drawing.Size(232, 20);
            this.txtbUserToEdit.TabIndex = 1;
            // 
            // chkbBlockUnblockUser
            // 
            this.chkbBlockUnblockUser.AutoSize = true;
            this.chkbBlockUnblockUser.ForeColor = System.Drawing.Color.White;
            this.chkbBlockUnblockUser.Location = new System.Drawing.Point(23, 342);
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
            this.lablUserEditErrMsg.Location = new System.Drawing.Point(254, 52);
            this.lablUserEditErrMsg.Name = "lablUserEditErrMsg";
            this.lablUserEditErrMsg.Size = new System.Drawing.Size(132, 13);
            this.lablUserEditErrMsg.TabIndex = 9;
            this.lablUserEditErrMsg.Text = "Error entering in Username";
            this.lablUserEditErrMsg.Visible = false;
            // 
            // lablUsernameToEdit
            // 
            this.lablUsernameToEdit.AutoSize = true;
            this.lablUsernameToEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablUsernameToEdit.ForeColor = System.Drawing.Color.White;
            this.lablUsernameToEdit.Location = new System.Drawing.Point(20, 210);
            this.lablUsernameToEdit.Name = "lablUsernameToEdit";
            this.lablUsernameToEdit.Size = new System.Drawing.Size(73, 15);
            this.lablUsernameToEdit.TabIndex = 4;
            this.lablUsernameToEdit.Text = "Username";
            // 
            // bttnGetUserInfo
            // 
            this.bttnGetUserInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttnGetUserInfo.Location = new System.Drawing.Point(125, 72);
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
            this.lablUserToEdit.Location = new System.Drawing.Point(19, 181);
            this.lablUserToEdit.Name = "lablUserToEdit";
            this.lablUserToEdit.Size = new System.Drawing.Size(356, 20);
            this.lablUserToEdit.TabIndex = 2;
            this.lablUserToEdit.Text = "Info of the User you Want to Block/Unblock";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(447, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter the username of the user you want to block or unblock";
            // 
            // createUserTab
            // 
            this.createUserTab.BackColor = System.Drawing.Color.Transparent;
            this.createUserTab.BackgroundImage = global::AdminControlForm.Properties.Resources.dark_space;
            this.createUserTab.Controls.Add(this.lablCreateNewUser);
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
            this.createUserTab.Controls.Add(this.txtbConfirmPassword);
            this.createUserTab.Controls.Add(this.txtbPassword);
            this.createUserTab.Controls.Add(this.txtbUsername);
            this.createUserTab.Controls.Add(this.lablEmail);
            this.createUserTab.Controls.Add(this.lablConfirmPassword);
            this.createUserTab.Controls.Add(this.lablPassword);
            this.createUserTab.Controls.Add(this.lablUsername);
            this.createUserTab.Location = new System.Drawing.Point(4, 22);
            this.createUserTab.Name = "createUserTab";
            this.createUserTab.Padding = new System.Windows.Forms.Padding(3);
            this.createUserTab.Size = new System.Drawing.Size(488, 486);
            this.createUserTab.TabIndex = 0;
            this.createUserTab.Text = "Create New User";
            // 
            // lablCreateNewUser
            // 
            this.lablCreateNewUser.AutoSize = true;
            this.lablCreateNewUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablCreateNewUser.ForeColor = System.Drawing.Color.White;
            this.lablCreateNewUser.Location = new System.Drawing.Point(24, 16);
            this.lablCreateNewUser.Name = "lablCreateNewUser";
            this.lablCreateNewUser.Size = new System.Drawing.Size(145, 20);
            this.lablCreateNewUser.TabIndex = 18;
            this.lablCreateNewUser.Text = "Create New User";
            // 
            // lablConfPasswordErrMsg
            // 
            this.lablConfPasswordErrMsg.AutoSize = true;
            this.lablConfPasswordErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablConfPasswordErrMsg.Location = new System.Drawing.Point(188, 245);
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
            this.lablUsernameSpec2.Location = new System.Drawing.Point(45, 106);
            this.lablUsernameSpec2.Name = "lablUsernameSpec2";
            this.lablUsernameSpec2.Size = new System.Drawing.Size(181, 13);
            this.lablUsernameSpec2.TabIndex = 15;
            this.lablUsernameSpec2.Text = "Can only contain letters and numbers";
            // 
            // lablPasswordSpec2
            // 
            this.lablPasswordSpec2.AutoSize = true;
            this.lablPasswordSpec2.ForeColor = System.Drawing.Color.White;
            this.lablPasswordSpec2.Location = new System.Drawing.Point(45, 200);
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
            this.lablPasswordSpec.Location = new System.Drawing.Point(45, 187);
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
            this.lablUsernameSpec.Location = new System.Drawing.Point(45, 93);
            this.lablUsernameSpec.Name = "lablUsernameSpec";
            this.lablUsernameSpec.Size = new System.Drawing.Size(81, 13);
            this.lablUsernameSpec.TabIndex = 12;
            this.lablUsernameSpec.Text = "4-16 characters";
            // 
            // lablEmailErrMsg
            // 
            this.lablEmailErrMsg.AutoSize = true;
            this.lablEmailErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablEmailErrMsg.Location = new System.Drawing.Point(98, 314);
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
            this.lablPasswordErrMsg.Location = new System.Drawing.Point(127, 146);
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
            this.lablUsernameErrMsg.Location = new System.Drawing.Point(125, 52);
            this.lablUsernameErrMsg.Name = "lablUsernameErrMsg";
            this.lablUsernameErrMsg.Size = new System.Drawing.Size(101, 13);
            this.lablUsernameErrMsg.TabIndex = 9;
            this.lablUsernameErrMsg.Text = "Username error msg";
            this.lablUsernameErrMsg.Visible = false;
            // 
            // txtbEmail
            // 
            this.txtbEmail.Location = new System.Drawing.Point(45, 332);
            this.txtbEmail.Name = "txtbEmail";
            this.txtbEmail.Size = new System.Drawing.Size(223, 20);
            this.txtbEmail.TabIndex = 7;
            // 
            // txtbConfirmPassword
            // 
            this.txtbConfirmPassword.Location = new System.Drawing.Point(45, 263);
            this.txtbConfirmPassword.Name = "txtbConfirmPassword";
            this.txtbConfirmPassword.Size = new System.Drawing.Size(223, 20);
            this.txtbConfirmPassword.TabIndex = 5;
            this.txtbConfirmPassword.TextChanged += new System.EventHandler(this.validateConfPassword);
            // 
            // txtbPassword
            // 
            this.txtbPassword.Location = new System.Drawing.Point(45, 164);
            this.txtbPassword.Name = "txtbPassword";
            this.txtbPassword.Size = new System.Drawing.Size(223, 20);
            this.txtbPassword.TabIndex = 3;
            this.txtbPassword.TextChanged += new System.EventHandler(this.validatePassword);
            // 
            // txtbUsername
            // 
            this.txtbUsername.Location = new System.Drawing.Point(45, 70);
            this.txtbUsername.Name = "txtbUsername";
            this.txtbUsername.Size = new System.Drawing.Size(223, 20);
            this.txtbUsername.TabIndex = 1;
            this.txtbUsername.TextChanged += new System.EventHandler(this.validateUsername);
            // 
            // lablEmail
            // 
            this.lablEmail.AutoSize = true;
            this.lablEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablEmail.ForeColor = System.Drawing.Color.White;
            this.lablEmail.Location = new System.Drawing.Point(45, 312);
            this.lablEmail.Name = "lablEmail";
            this.lablEmail.Size = new System.Drawing.Size(47, 17);
            this.lablEmail.TabIndex = 6;
            this.lablEmail.Text = "Email";
            // 
            // lablConfirmPassword
            // 
            this.lablConfirmPassword.AutoSize = true;
            this.lablConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablConfirmPassword.ForeColor = System.Drawing.Color.White;
            this.lablConfirmPassword.Location = new System.Drawing.Point(45, 243);
            this.lablConfirmPassword.Name = "lablConfirmPassword";
            this.lablConfirmPassword.Size = new System.Drawing.Size(137, 17);
            this.lablConfirmPassword.TabIndex = 4;
            this.lablConfirmPassword.Text = "Confirm Password";
            // 
            // lablPassword
            // 
            this.lablPassword.AutoSize = true;
            this.lablPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablPassword.ForeColor = System.Drawing.Color.White;
            this.lablPassword.Location = new System.Drawing.Point(45, 144);
            this.lablPassword.Name = "lablPassword";
            this.lablPassword.Size = new System.Drawing.Size(77, 17);
            this.lablPassword.TabIndex = 2;
            this.lablPassword.Text = "Password";
            // 
            // lablUsername
            // 
            this.lablUsername.AutoSize = true;
            this.lablUsername.BackColor = System.Drawing.Color.Transparent;
            this.lablUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablUsername.ForeColor = System.Drawing.Color.White;
            this.lablUsername.Location = new System.Drawing.Point(45, 50);
            this.lablUsername.Name = "lablUsername";
            this.lablUsername.Size = new System.Drawing.Size(81, 17);
            this.lablUsername.TabIndex = 0;
            this.lablUsername.Text = "Username";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.createUserTab);
            this.tabControl.Controls.Add(this.editUserTab);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(496, 512);
            this.tabControl.TabIndex = 0;
            // 
<<<<<<< HEAD
            // btnGetUserStats
            // 
            this.btnGetUserStats.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGetUserStats.Location = new System.Drawing.Point(338, 142);
            this.btnGetUserStats.Name = "btnGetUserStats";
            this.btnGetUserStats.Size = new System.Drawing.Size(101, 23);
            this.btnGetUserStats.TabIndex = 26;
            this.btnGetUserStats.Text = "Get User Stats";
            this.btnGetUserStats.UseVisualStyleBackColor = true;
            this.btnGetUserStats.Click += new System.EventHandler(this.btnGetUserStats_Click);
            // 
            // powerupNameDataGridViewTextBoxColumn
            // 
            this.powerupNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.powerupNameDataGridViewTextBoxColumn.DataPropertyName = "PowerupName";
            this.powerupNameDataGridViewTextBoxColumn.HeaderText = "PowerupName";
            this.powerupNameDataGridViewTextBoxColumn.Name = "powerupNameDataGridViewTextBoxColumn";
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
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
=======
            // lblUserStatError
            // 
            this.lblUserStatError.AutoSize = true;
            this.lblUserStatError.ForeColor = System.Drawing.Color.Red;
            this.lblUserStatError.Location = new System.Drawing.Point(108, 172);
            this.lblUserStatError.Name = "lblUserStatError";
            this.lblUserStatError.Size = new System.Drawing.Size(85, 13);
            this.lblUserStatError.TabIndex = 27;
            this.lblUserStatError.Text = "- not a valid user";
            this.lblUserStatError.Visible = false;
            // 
            // btnDelPower
            // 
            this.btnDelPower.Location = new System.Drawing.Point(387, 69);
            this.btnDelPower.Name = "btnDelPower";
            this.btnDelPower.Size = new System.Drawing.Size(95, 23);
            this.btnDelPower.TabIndex = 27;
            this.btnDelPower.Text = "Delete Powerup";
            this.btnDelPower.UseVisualStyleBackColor = true;
            this.btnDelPower.Click += new System.EventHandler(this.btnDelPower_Click);
            // 
            // lblPwrError
            // 
            this.lblPwrError.AutoSize = true;
            this.lblPwrError.ForeColor = System.Drawing.Color.Red;
            this.lblPwrError.Location = new System.Drawing.Point(345, 95);
            this.lblPwrError.Name = "lblPwrError";
            this.lblPwrError.Size = new System.Drawing.Size(135, 13);
            this.lblPwrError.TabIndex = 28;
            this.lblPwrError.Text = "- not a valid powerup name";
            this.lblPwrError.Visible = false;
>>>>>>> fb46d967fe33a8cf5f815b9106581044f6fde28b
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
<<<<<<< HEAD
            this.tabControl.ResumeLayout(false);
            this.createUserTab.ResumeLayout(false);
            this.createUserTab.PerformLayout();
            this.editUserTab.ResumeLayout(false);
            this.editUserTab.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
=======
            this.Load += new System.EventHandler(this.AdminForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spaceUnionDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerupBindingSource)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPwrValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPwrup)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlagsCaptured)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShip3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShip2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShip1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDied)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudKills)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShotsFired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLoses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWins)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.editUserTab.ResumeLayout(false);
            this.editUserTab.PerformLayout();
            this.createUserTab.ResumeLayout(false);
            this.createUserTab.PerformLayout();
            this.tabControl.ResumeLayout(false);
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
            this.ResumeLayout(false);

        }

        #endregion

<<<<<<< HEAD
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage createUserTab;
        private System.Windows.Forms.TabPage editUserTab;
        private System.Windows.Forms.TextBox txtbUsername;
        private System.Windows.Forms.Label lablUsername;
        private System.Windows.Forms.TextBox txtbConfirmPassword;
        private System.Windows.Forms.Label lablConfirmPassword;
        private System.Windows.Forms.TextBox txtbPassword;
        private System.Windows.Forms.Label lablPassword;
=======
        private SpaceUnionDataSet spaceUnionDataSet;
        private System.Windows.Forms.BindingSource powerupBindingSource;
        private SpaceUnionDataSetTableAdapters.PowerupTableAdapter powerupTableAdapter;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnUpdatePwr;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown nudPwrValue;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbPowerupName;
        private System.Windows.Forms.DataGridView dgvPwrup;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label lablShipEditMaxSpeed;
        private System.Windows.Forms.Label lablShipEditAccelerateErrMsg;
        private System.Windows.Forms.Label lablShipEditTurnSpdErrMsg;
        private System.Windows.Forms.Button bttnShipUpdate;
        private System.Windows.Forms.Button bttnShipToEdit;
        private System.Windows.Forms.TextBox txtbNewMaxSpeed;
        private System.Windows.Forms.TextBox txtbNewAccelerate;
        private System.Windows.Forms.TextBox txtbNewTurnSpeed;
        private System.Windows.Forms.RichTextBox rtxtCurrentShipStats;
        private System.Windows.Forms.TextBox txtbShipEditing;
        private System.Windows.Forms.Label lablNewShipMaxSpeed;
        private System.Windows.Forms.Label lablNewShipAccelerate;
        private System.Windows.Forms.Label lablNewShipTurnSpeed;
        private System.Windows.Forms.Label lablCurrentShipStats;
        private System.Windows.Forms.Label lablShipEditing;
        private System.Windows.Forms.Label lablEditShip;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown nudFlagsCaptured;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudShip3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudShip2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudShip1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudDied;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudKills;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudHits;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudShotsFired;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudLoses;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudWins;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbUserStatName;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lablMaxSpeedErrMsg;
        private System.Windows.Forms.Label lablAccelErrMsg;
        private System.Windows.Forms.Label lablTurnSpeedErrMsg;
        private System.Windows.Forms.Button bttnAddShip;
        private System.Windows.Forms.Label lablMaxSpeed;
        private System.Windows.Forms.TextBox txtbMaxSpeed;
        private System.Windows.Forms.TextBox txtbAccelerate;
        private System.Windows.Forms.TextBox txtbTurnSpeed;
        private System.Windows.Forms.TextBox txtbNewShipName;
        private System.Windows.Forms.Label lablAccelerate;
        private System.Windows.Forms.Label lablTurnSpeed;
        private System.Windows.Forms.Label lablNewShipNameErrMsg;
        private System.Windows.Forms.Label lablNewShipName;
        private System.Windows.Forms.Label lablAddNewShip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage editUserTab;
        private System.Windows.Forms.Label lablCurrentBlockStatus;
        private System.Windows.Forms.TextBox txtbCurrentBlockStatus;
        private System.Windows.Forms.TextBox txtbUserEditing;
        private System.Windows.Forms.TextBox txtbUserToEdit;
        private System.Windows.Forms.CheckBox chkbBlockUnblockUser;
        private System.Windows.Forms.Button bttnBlockUnblock;
        private System.Windows.Forms.Label lablUserEditErrMsg;
        private System.Windows.Forms.Label lablUsernameToEdit;
        private System.Windows.Forms.Button bttnGetUserInfo;
        private System.Windows.Forms.Label lablUserToEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage createUserTab;
        private System.Windows.Forms.Label lablCreateNewUser;
        private System.Windows.Forms.Label lablConfPasswordErrMsg;
        private System.Windows.Forms.Button bttnCreateUser;
        private System.Windows.Forms.Label lablUsernameSpec2;
>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
        private System.Windows.Forms.Label lablPasswordSpec2;
        private System.Windows.Forms.Label lablPasswordSpec;
        private System.Windows.Forms.Label lablUsernameSpec;
        private System.Windows.Forms.Label lablEmailErrMsg;
        private System.Windows.Forms.Label lablPasswordErrMsg;
        private System.Windows.Forms.Label lablUsernameErrMsg;
        private System.Windows.Forms.TextBox txtbEmail;
<<<<<<< HEAD
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
=======
        private System.Windows.Forms.TextBox txtbConfirmPassword;
        private System.Windows.Forms.TextBox txtbPassword;
        private System.Windows.Forms.TextBox txtbUsername;
        private System.Windows.Forms.Label lablEmail;
        private System.Windows.Forms.Label lablConfirmPassword;
        private System.Windows.Forms.Label lablPassword;
        private System.Windows.Forms.Label lablUsername;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Button btnGetUserStats;
        private System.Windows.Forms.DataGridViewTextBoxColumn powerupNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn powerupValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label lblUserStatError;
        private System.Windows.Forms.Button btnDelPower;
        private System.Windows.Forms.Label lblPwrError;

>>>>>>> 0187f50c6dcf5dd78a3fcf9bfa1804582faa739e
    }
}

