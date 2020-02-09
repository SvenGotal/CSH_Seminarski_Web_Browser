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

    //TODO reserved for v.1.2.

    public partial class UsersSelect : Form
    {
        public UsersSelect()
        {
            InitializeComponent();



        }

        /* Private internals and handlers */
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {

            //TODO reserved for v.1.2.
            ComingSoon();
        }
        public void ComingSoon()
        {
            MessageBox.Show("Coming soon!!!");
        }

    }
}
