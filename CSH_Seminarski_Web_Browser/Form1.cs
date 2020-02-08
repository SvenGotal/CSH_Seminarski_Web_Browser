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
        private string placeholder; 
        private List<string> history;
        private int historyIndex;

        public Form1()
        {
            InitializeComponent();

            placeholder = "http://";
            history = new List<string>();
            historyIndex = -1;

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
            throw new NotImplementedException();
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
            webBrowser.Navigate(textBoxURL.Text);
        }

    }
}
