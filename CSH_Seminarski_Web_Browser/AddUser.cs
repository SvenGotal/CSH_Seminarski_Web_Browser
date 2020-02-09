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
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            //TODO
            throw new NotImplementedException();

            //string name = textBoxName.Text;
            //string last = textBoxLast.Text;

            //if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(last))
            //{
            //    MessageBox.Show("Name or last name invalid.", "Blank spaces Error!", MessageBoxButtons.OK);
            //}
            //else
            //{
            //    User usr = new User(name, last);
            //}

        }
    }
}
