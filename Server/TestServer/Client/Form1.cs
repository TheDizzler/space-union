using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    picturebox_user.Left -= 5;
                    break;
                case Keys.W:
                    picturebox_user.Top -= 5;
                    break;
                case Keys.D:
                    picturebox_user.Left += 5;
                    break;
                case Keys.S:
                    picturebox_user.Top += 5;
                    break;
            }
        }
    }
}
