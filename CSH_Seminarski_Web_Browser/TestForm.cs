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
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TESTbtn_Click(object sender, EventArgs e)
        {
            Persistence.WriteHistory("common_history.xml", "https://www.google.com");
            List<string> histories = Persistence.ReadHistory("common_history.xml");

            foreach (string hist in histories)
            {
                outputBox.Text += hist;
            }

        }
    }
}
