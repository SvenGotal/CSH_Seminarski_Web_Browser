using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace CSH_Seminarski_Web_Browser
{
    public partial class Form1 : Form
    {
        private string placeholder; 
        private HashSet<string> history;

        public Form1()
        {
            InitializeComponent();

            placeholder = "http://";
            history = new HashSet<string>();

            textBoxURL.Text = placeholder;
            textBoxURL.GotFocus += TextBoxURL_GotFocus;
            textBoxURL.LostFocus += TextBoxURL_LostFocus;

        }

        /*********************************************************************************************/
        /* Event handlers */

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
        private void textBoxURL_KeyDown(object sender, KeyEventArgs e)
        {
            if(validateKey(e, Keys.Enter) && !string.IsNullOrWhiteSpace(textBoxURL.Text))
            {
                Navigate();
            }
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            webBrowser.GoBack();
        }


        private void buttonFavoritesAdd_Click(object sender, EventArgs e)
        {

            

        }









        /*********************************************************************************************/
        /* Helper methods */

        /// <summary>
        /// Checks if the key entered equals the desired key.
        /// </summary>
        /// <param name="key">Input key</param>
        /// <param name="chosenKey">Desired key</param>
        /// <returns>True if key equals desired key.</returns>
        private bool validateKey(KeyEventArgs key, Keys chosenKey)
        {
            return key.KeyCode == chosenKey;
        }

        /// <summary>
        /// Navigates the webBrowser control to the desired URL
        /// </summary>
        private void Navigate()
        {
            try
            {
                historyToolStripMenuItem.DropDownItems.Clear();

                webBrowser.Navigate(textBoxURL.Text);


                textBoxURL.Text = webBrowser.Url.ToString();
                history.Add(webBrowser.Url.ToString());
                foreach (string item in history)
                {
                    historyToolStripMenuItem.DropDownItems.Add(item);

                }
            }
            catch(Exception e)
            {

            }

        }

        private async Task delayTask(int miliseconds)
        {
            await Task.Delay(miliseconds);
        }

        private void newProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUser form = new AddUser();
            form.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
