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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            string project = " Web Browser v 1.1.";
            string creator = " Sven Gotal.";
            string licence = " GPL - GNU Public Licence.";
            string note = "You are free to redistrubite, change and use this program freely as covered in GPL.";

            labelProject.Text += project;
            labelCreator.Text += creator;
            labelLicence.Text += licence;
            noteBox.Text = note;    
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
