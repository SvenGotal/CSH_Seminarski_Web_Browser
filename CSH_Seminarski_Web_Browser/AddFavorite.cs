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
    public partial class AddFavorite : Form
    {
        public AddFavorite()
        {
            InitializeComponent();
        }

        private readonly Form1 mainForm;
        private readonly string placeholder;
        public AddFavorite(Form mainForm)
        {
            this.mainForm = mainForm as Form1;
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;


            try
            {


                placeholder = this.mainForm.webBrowser.Url.ToString();
                textBoxUrl.Text = placeholder;
                textBoxUrl.GotFocus += TextBoxUrl_GotFocus;
                textBoxUrl.LostFocus += TextBoxUrl_LostFocus;



            }
            catch (Exception)
            {
                placeholder = "";
            }


        }

        /* Private internals and handlers */
        private void TextBoxUrl_LostFocus(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBoxUrl.Text))
                textBoxUrl.Text = placeholder;
        }
        private void TextBoxUrl_GotFocus(object sender, EventArgs e)
        {
            textBoxUrl.Text = "";
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {

                string filename = this.mainForm.CurrentUser.Name + "_favorites.xml";
                string url = textBoxUrl.Text;
                string name = textBoxDescription.Text;

                if (!string.IsNullOrWhiteSpace(url) && !string.IsNullOrWhiteSpace(name))
                {
                    Favorite fav = new Favorite(url, name);
                    Persistence.WriteFavorite(filename, fav);

                    this.mainForm.UpdateCurrentUserFavorites();
                    this.mainForm.checkIfListAvailable<Favorite>(mainForm.CurrentUser.Favorites, mainForm.getFavoritesToolstripItem());

                    this.Close();

                }
                else
                {
                    MessageBox.Show("No white spaces allowed!", "Error!", MessageBoxButtons.OK);
                    return;
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK);
            }
        }
    }
}
