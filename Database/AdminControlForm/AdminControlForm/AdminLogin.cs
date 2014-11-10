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
    public partial class AdminLogin : Form
    {
        private UserTableAccess userTable = new UserTableAccess();

        public AdminLogin()
        {
            InitializeComponent();
        }

        private void bttnAdminLogin_Click(object sender, EventArgs e)
        {
            int errCode = 0;

            if (userTable.AdminLogin(txtbUsernameInput.Text,
                                     txtbPasswordInput.Text,
                                     ref errCode) ) {
                lablAdminLoginErrMsg.Visible = false;
                this.Hide();

                AdminForm af = new AdminForm();
                af.ShowDialog();
            }
            else {
                getErrorMessage(errCode);
                lablAdminLoginErrMsg.Text    = getErrorMessage(errCode);
                lablAdminLoginErrMsg.Visible = true;
            }

        }

        private string getErrorMessage(int errCode)
        {
            string errMsg = null;

            switch (errCode) {
                case 1:
                    errMsg = "Sorry this account is blocked from access";
                    break;
                case 2:
                    errMsg = "Sorry, this account does not have admin privileges";
                    break;
                case 3:
                    errMsg = "You may only have one user logged in per account";
                    break;
                case 4:
                    errMsg = "The username or password is incorrect";
                    break;
            }

            return errMsg;
        }
    }
}
