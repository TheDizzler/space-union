using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminControlForm
{
    /// <summary>
    /// Custom yes, no acceptance form
    /// </summary>
    public partial class AcceptCancelBlockActionForm : Form
    {
        public string TxtMsg {
            set { textMsg.Text = value; }
            get { return this.textMsg.Text; }
        }
        public AcceptCancelBlockActionForm()
        {
            InitializeComponent();
        }
    }
}
