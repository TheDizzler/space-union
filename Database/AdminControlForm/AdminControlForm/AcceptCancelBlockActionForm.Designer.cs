namespace AdminControlForm
{
    partial class AcceptCancelBlockActionForm
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
            this.bttnNo = new System.Windows.Forms.Button();
            this.bttnYes = new System.Windows.Forms.Button();
            this.textMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bttnNo
            // 
            this.bttnNo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bttnNo.Location = new System.Drawing.Point(184, 89);
            this.bttnNo.Name = "bttnNo";
            this.bttnNo.Size = new System.Drawing.Size(75, 23);
            this.bttnNo.TabIndex = 0;
            this.bttnNo.Text = "No";
            this.bttnNo.UseVisualStyleBackColor = true;
            // 
            // bttnYes
            // 
            this.bttnYes.Location = new System.Drawing.Point(103, 89);
            this.bttnYes.Name = "bttnYes";
            this.bttnYes.Size = new System.Drawing.Size(75, 23);
            this.bttnYes.TabIndex = 1;
            this.bttnYes.Text = "Yes";
            this.bttnYes.UseVisualStyleBackColor = true;
            // 
            // textMsg
            // 
            this.textMsg.AutoSize = true;
            this.textMsg.Location = new System.Drawing.Point(12, 32);
            this.textMsg.Name = "textMsg";
            this.textMsg.Size = new System.Drawing.Size(35, 13);
            this.textMsg.TabIndex = 2;
            this.textMsg.Text = "label1";
            // 
            // AcceptCancelBlockActionForm
            // 
            this.AcceptButton = this.bttnYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bttnNo;
            this.ClientSize = new System.Drawing.Size(284, 127);
            this.Controls.Add(this.textMsg);
            this.Controls.Add(this.bttnYes);
            this.Controls.Add(this.bttnNo);
            this.Name = "AcceptCancelBlockActionForm";
            this.Text = "Are you sure?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bttnNo;
        private System.Windows.Forms.Button bttnYes;
        private System.Windows.Forms.Label textMsg;
    }
}