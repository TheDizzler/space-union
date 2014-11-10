namespace AdminControlForm
{
    partial class AdminLogin
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
            this.txtbUsernameInput = new System.Windows.Forms.TextBox();
            this.txtbPasswordInput = new System.Windows.Forms.TextBox();
            this.lablUsernameInput = new System.Windows.Forms.Label();
            this.lablPasswordInput = new System.Windows.Forms.Label();
            this.bttnAdminLogin = new System.Windows.Forms.Button();
            this.lablAdminLoginErrMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtbUsernameInput
            // 
            this.txtbUsernameInput.Location = new System.Drawing.Point(130, 187);
            this.txtbUsernameInput.Name = "txtbUsernameInput";
            this.txtbUsernameInput.Size = new System.Drawing.Size(158, 20);
            this.txtbUsernameInput.TabIndex = 0;
            // 
            // txtbPasswordInput
            // 
            this.txtbPasswordInput.Location = new System.Drawing.Point(130, 247);
            this.txtbPasswordInput.Name = "txtbPasswordInput";
            this.txtbPasswordInput.Size = new System.Drawing.Size(158, 20);
            this.txtbPasswordInput.TabIndex = 1;
            // 
            // lablUsernameInput
            // 
            this.lablUsernameInput.AutoSize = true;
            this.lablUsernameInput.Location = new System.Drawing.Point(127, 171);
            this.lablUsernameInput.Name = "lablUsernameInput";
            this.lablUsernameInput.Size = new System.Drawing.Size(55, 13);
            this.lablUsernameInput.TabIndex = 2;
            this.lablUsernameInput.Text = "Username";
            // 
            // lablPasswordInput
            // 
            this.lablPasswordInput.AutoSize = true;
            this.lablPasswordInput.Location = new System.Drawing.Point(127, 231);
            this.lablPasswordInput.Name = "lablPasswordInput";
            this.lablPasswordInput.Size = new System.Drawing.Size(53, 13);
            this.lablPasswordInput.TabIndex = 3;
            this.lablPasswordInput.Text = "Password";
            // 
            // bttnAdminLogin
            // 
            this.bttnAdminLogin.Location = new System.Drawing.Point(213, 289);
            this.bttnAdminLogin.Name = "bttnAdminLogin";
            this.bttnAdminLogin.Size = new System.Drawing.Size(75, 23);
            this.bttnAdminLogin.TabIndex = 4;
            this.bttnAdminLogin.Text = "Login";
            this.bttnAdminLogin.UseVisualStyleBackColor = true;
            this.bttnAdminLogin.Click += new System.EventHandler(this.bttnAdminLogin_Click);
            // 
            // lablAdminLoginErrMsg
            // 
            this.lablAdminLoginErrMsg.AutoSize = true;
            this.lablAdminLoginErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lablAdminLoginErrMsg.Location = new System.Drawing.Point(127, 148);
            this.lablAdminLoginErrMsg.Name = "lablAdminLoginErrMsg";
            this.lablAdminLoginErrMsg.Size = new System.Drawing.Size(120, 13);
            this.lablAdminLoginErrMsg.TabIndex = 5;
            this.lablAdminLoginErrMsg.Text = "admin login err message";
            this.lablAdminLoginErrMsg.Visible = false;
            // 
            // AdminLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 368);
            this.Controls.Add(this.lablAdminLoginErrMsg);
            this.Controls.Add(this.bttnAdminLogin);
            this.Controls.Add(this.lablPasswordInput);
            this.Controls.Add(this.lablUsernameInput);
            this.Controls.Add(this.txtbPasswordInput);
            this.Controls.Add(this.txtbUsernameInput);
            this.Name = "AdminLogin";
            this.Text = "AdminLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbUsernameInput;
        private System.Windows.Forms.TextBox txtbPasswordInput;
        private System.Windows.Forms.Label lablUsernameInput;
        private System.Windows.Forms.Label lablPasswordInput;
        private System.Windows.Forms.Button bttnAdminLogin;
        private System.Windows.Forms.Label lablAdminLoginErrMsg;
    }
}