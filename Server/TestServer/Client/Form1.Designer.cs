namespace Client
{
    partial class Form1
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
            this.picturebox_user = new System.Windows.Forms.PictureBox();
            this.picturebox_player = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_user)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_player)).BeginInit();
            this.SuspendLayout();
            // 
            // picturebox_user
            // 
            this.picturebox_user.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picturebox_user.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picturebox_user.Location = new System.Drawing.Point(243, 161);
            this.picturebox_user.Name = "picturebox_user";
            this.picturebox_user.Size = new System.Drawing.Size(45, 36);
            this.picturebox_user.TabIndex = 0;
            this.picturebox_user.TabStop = false;
            // 
            // picturebox_player
            // 
            this.picturebox_player.BackColor = System.Drawing.SystemColors.HotTrack;
            this.picturebox_player.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picturebox_player.Location = new System.Drawing.Point(294, 161);
            this.picturebox_player.Name = "picturebox_player";
            this.picturebox_player.Size = new System.Drawing.Size(45, 36);
            this.picturebox_player.TabIndex = 1;
            this.picturebox_player.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 374);
            this.Controls.Add(this.picturebox_player);
            this.Controls.Add(this.picturebox_user);
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_user)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_player)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picturebox_user;
        private System.Windows.Forms.PictureBox picturebox_player;
    }
}

