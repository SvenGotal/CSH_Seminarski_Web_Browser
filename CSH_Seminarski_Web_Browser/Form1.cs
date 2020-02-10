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

    //TODO NoteToSelf: For users logins use DB, not .xml
    //TODO NoteToSelf: Expand User class with password.

    public partial class Form1 : Form
    {
        private readonly string placeholder; 
        public User CurrentUser;

        public Form1()
        {
            InitializeComponent();

            placeholder = "http://";

            CurrentUser = new User("common", "user");
            UpdateCurrentUserFavorites();

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
            navigate();

            refresh();
        }
        private void textBoxURL_KeyDown(object sender, KeyEventArgs e)
        {
            if(validateKey(e, Keys.Enter) && !string.IsNullOrWhiteSpace(textBoxURL.Text))
            {
                navigate();
                refresh();
            }
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            webBrowser.GoBack();
        }
        private void buttonForward_Click(object sender, EventArgs e)
        {
            webBrowser.GoForward();
        }
        private void buttonFavoritesAdd_Click(object sender, EventArgs e)
        {
            AddFavorite add = new AddFavorite(this);
            add.Show();
        }
        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string url = webBrowser.Url.ToString();
            textBoxURL.Text = url;

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
        private void selectProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersSelect form = new UsersSelect();
            form.Show();
        }
        private void favoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            favoritesToolStripMenuItem.DropDownItems.Clear();
            foreach (Favorite favorite in CurrentUser.Favorites)
            {

                ToolStripMenuItem item = new ToolStripMenuItem(favoritesToolStripMenuItem.ToString());
                item.Text = favorite.Url;
                item.Click += new EventHandler(menu_Click);
                favoritesToolStripMenuItem.DropDownItems.Add(item);

            }

        }
        private void menu_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            var menuText = menuItem.Text;

            webBrowser.Navigate(menuText);

            refresh();

        }
        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO reserved for v.1.2.
            ComingSoon();
        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Perhaps in the future help will be provided.");
        }
        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.Show();
        }



        /*********************************************************************************************/
        /* Helper methods */

        /// <summary>
        /// Updates the loged in user's favorites.
        /// </summary>
        public void UpdateCurrentUserFavorites()
        {
            try
            {
                CurrentUser.Favorites = loadCurrentUserFavorites();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Displays the "Coming soon" message.
        /// </summary>
        public void ComingSoon()
        {
            MessageBox.Show("Coming soon!!!");
        }



        /* Private internals */
        private List<Favorite> loadCurrentUserFavorites()
        {

            string filename = CurrentUser.Name + "_favorites.xml";
            return Persistence.ReadFavorites(filename);

        }
        private bool validateKey(KeyEventArgs key, Keys chosenKey)
        {
            return key.KeyCode == chosenKey;
        }
        private void navigate()
        {
            try
            {
                historyToolStripMenuItem.DropDownItems.Clear();

                webBrowser.Navigate(textBoxURL.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void refresh()
        {

            webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;

        }

    }
}
