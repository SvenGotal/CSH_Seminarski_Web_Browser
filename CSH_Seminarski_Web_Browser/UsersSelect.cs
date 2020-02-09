using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSH_Seminarski_Web_Browser
{
    public partial class UsersSelect : Form
    {
        public UsersSelect()
        {
            InitializeComponent();

            User usr = new User();

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            // TODO v.1.1
            MessageBox.Show("Not yet implemented, reserved for version 1.2", "Error", MessageBoxButtons.OK);
        }
    }
}
