using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CSH_Seminarski_Web_Browser
{

    //TODO NoteToSelf: For users logins use DB, not .xml
    //TODO NoteToSelf: Expand User class with password.
    //TODO DeBug: History writes itself multiple times during navigation.

    public partial class Form1 : Form
    {
        private readonly string placeholder; 
        public User CurrentUser;

        public Form1()
        {
            InitializeComponent();

            placeholder = "http://";


            CurrentUser = new User("nologin", "user");
            CurrentUser.Favorites = new List<Favorite>();
            CurrentUser.History = new List<string>();

            UpdateCurrentUserFavorites();
            UpdateCurrentUserHistory();


            textBoxURL.Text = placeholder;
            textBoxURL.GotFocus += TextBoxURL_GotFocus;
            textBoxURL.LostFocus += TextBoxURL_LostFocus;

            TestForm tf = new TestForm();
            tf.Show();

            checkIfListAvailable<Favorite>(CurrentUser.Favorites, favoritesToolStripMenuItem);

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
            add.ShowDialog(this);
        }
        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string url = webBrowser.Url.ToString();
            textBoxURL.Text = url;
            persistUserHistory(url);
            addUserHistory(url);

        }
        private void newProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUser form = new AddUser();
            form.ShowDialog(this);
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

            try
            {
                foreach (Favorite favorite in CurrentUser.Favorites)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(favoritesToolStripMenuItem.ToString());
                    item.Text = favorite.Url;
                    item.Click += new EventHandler(menu_Click);
                    favoritesToolStripMenuItem.DropDownItems.Add(item);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Uuups, unable to load your favorites. Is the file missing? Error msg: " + ex.Message);
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
            historyToolStripMenuItem.DropDownItems.Clear();

            try
            {
                foreach (string history in CurrentUser.History)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(historyToolStripMenuItem.ToString());
                    item.Text = history;
                    item.Click += new EventHandler(menu_Click);
                    historyToolStripMenuItem.DropDownItems.Add(item);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Uuups, unable to load your history. Is the file missing? Error msg: " + ex.Message);
            }

        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Perhaps in the future help will be provided.");
        }
        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.ShowDialog(this);
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

        //TODO Document: Pending
        public void UpdateCurrentUserHistory()
        {
            try
            {
                CurrentUser.History = loadCurrentUserHistory();
            }
            catch(Exception ex)
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
        private List<string> loadCurrentUserHistory()
        {
            string filename = CurrentUser.Name + "_history.xml";
            return Persistence.ReadHistory(filename);
        }
        private void persistUserHistory(string history)
        {
            try
            {
                string filename = CurrentUser.Name + "_history.xml";
                Persistence.WriteHistory(filename, history);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void addUserHistory(string history)
        {
            if (CurrentUser.History == null)
                CurrentUser.History = new List<string>();
            CurrentUser.History.Add(history);
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
        public void checkIfListAvailable<T>(List<T> list, ToolStripMenuItem ctrl)
        {
            if (list != null && list.Count != 0)
                ctrl.Enabled = true;
            else
                ctrl.Enabled = false;
        }

        /// <summary>
        /// Getter for FavoritesToolstripItem in main Form
        /// </summary>
        /// <returns></returns>
        public ToolStripMenuItem getFavoritesToolstripItem()
        {
            return favoritesToolStripMenuItem;
        }
    }
}
