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
    public partial class Form1 : Form
    {
        private string placeholder = "http://";
        private string placeholderSecure = "https://";

        public Form1()
        {
            InitializeComponent();

            textBoxURL.Text = placeholder;
            textBoxURL.GotFocus += TextBoxURL_GotFocus;
            textBoxURL.LostFocus += TextBoxURL_LostFocus;

        }

        private void TextBoxURL_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxURL.Text))
            {
                textBoxURL.Text = placeholder;
            }
        }

        private void TextBoxURL_GotFocus(object sender, EventArgs e)
        {
            if( textBoxURL.Text == placeholder)
            {
                textBoxURL.Text = "";
            }
        }

        private void buttonGO_Click(object sender, EventArgs e)
        {
            Navigate();
        }

        private void Navigate()
        {
            webBrowser.Navigate(textBoxURL.Text);
        }

        private void textBoxURL_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(textBoxURL.Text))
            {
                Navigate();
            }
        }

        private void textBoxURL_Enter(object sender, EventArgs e)
        {

        }
    }
}
