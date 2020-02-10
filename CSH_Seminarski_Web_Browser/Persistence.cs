using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CSH_Seminarski_Web_Browser
{
    public static class Persistence
    {



        /* Write to xml functionalities */
        /// <summary>
        /// Writes the user in specified format to desired filename, if user already exists it adds user to the specified .xml format.
        /// </summary>
        /// <param name="filename">Path to filename, .xml.</param>
        /// <param name="usr">User to write</param>
        /// <returns>True if the method succeeds or user already exists, false if method fails to write user to .xml file.</returns>
        public static bool WriteUser(string filename, User usr)
        {
            XDocument doc;
            appendDotXmlIfNotPresent(ref filename);

            try
            {
                doc = XDocument.Load(filename);

                if (!formatUsersIfNotExist(filename, usr))
                {
                    XElement user = doc.Element("Users");
                    user.Add(new XElement("User", new XElement("Name", usr.Name), new XElement("Last", usr.LastName)));

                    doc.Save(filename);
                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Writes the history entry in specified format to desired filename.
        /// </summary>
        /// <param name="filename">Path to filename, .xml.</param>
        /// <param name="history">Entry to write</param>
        /// <returns>True if the method succeeds writing the entry, false if method fails to write entry to .xml file.</returns>
        public static bool WriteHistory(string filename, string history)
        {
            XDocument doc;
            appendDotXmlIfNotPresent(ref filename);

            try
            {
                formatHistoryIfNotExist(filename);

                doc = XDocument.Load(filename);

                XElement hist = doc.Element("History");

                hist.Add(new XElement("Url", history));

                doc.Save(filename);

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Writes the favorite entry into .xml file.
        /// </summary>
        /// <param name="filename">Path to file.</param>
        /// <param name="favorite">Favorite entry to be written.</param>
        /// <returns>True if entry is successfully entered.</returns>
        public static bool WriteFavorite(string filename, Favorite favorite)
        {
            XDocument doc;
            appendDotXmlIfNotPresent(ref filename);

            try
            {
                formatFavoritesIfNotExist(filename);
                doc = XDocument.Load(filename);

                if (!validateFavoriteInFavorites(doc, favorite))
                {
                    

                    XElement fav = doc.Element("Favorites");
                    fav.Add(new XElement("Favorite", new XElement("Url", favorite.Url), new XElement("Name", favorite.Description)));

                    doc.Save(filename);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }



        /* Read from xml functionalities */
        /// <summary>
        /// Reads from specified .xml file all entries. 
        /// </summary>
        /// <param name="filename">Path to filename to read from.</param>
        /// <returns>If reading is successful the the List\<Favorite\> is returned.</Favorite></returns>
        public static List<string> ReadHistory(string filename)
        {
            List<string> histories = new List<string>();
            XDocument doc;

            try
            {
                appendDotXmlIfNotPresent(ref filename);
                doc = XDocument.Load(filename);

                var query = from favs in doc.Descendants("History")
                            select new string(favs.Element("Url").Value.ToCharArray());

                foreach (string history in query)
                {
                    histories.Add(history);
                }

            }
            catch (Exception)
            {
                return null;
            }


            return histories;

        }
        /// <summary>
        /// Reads the specified file that contains users.
        /// </summary>
        /// <param name="filename">Path to file</param>
        /// <param name="usrname">Search by name</param>
        /// <param name="usrlast">Search by last name</param>
        /// <returns>Returns the User if found.</returns>
        public static User ReadUser(string filename, string usrname, string usrlast)
        {
            appendDotXmlIfNotPresent(ref filename);

            XDocument doc = XDocument.Load(filename);

            User usr = new User();

            try
            {
                var query = from user in doc.Descendants("User")
                            where user.Element("Name").Value == usrname
                            && user.Element("Last").Value == usrlast
                            select new User(user.Element("Name").Value, user.Element("Last").Value);

                foreach (var users in query)
                {
                    usr = users;
                }

            }
            catch (Exception)
            {
                return null;
            }
            return usr;
        }
        /// <summary>
        /// Reads all User entries from specified file.
        /// </summary>
        /// <param name="filename">Path to file to be read.</param>
        /// <returns>List of users found within the file.</returns>
        public static List<User> ReadUsersAll(string filename)
        {
            //TODO reserved for v.1.2.
            throw new NotImplementedException();
        }
        /// <summary>
        /// Reads all Favorite entries from specified file.
        /// </summary>
        /// <param name="filename">Path to file</param>
        /// <returns>List of Favorites read from file.</returns>
        public static List<Favorite> ReadFavorites(string filename)
        {
            List<Favorite> favorites = new List<Favorite>();
            XDocument doc;

            try
            {
                appendDotXmlIfNotPresent(ref filename);
                doc = XDocument.Load(filename);

                var query = from favs in doc.Descendants("Favorite")
                            select new Favorite
                            {
                                Url = (string)favs.Element("Url"), 
                                Description = (string)favs.Element("Name")
                            };

                foreach (Favorite favorite in query)
                {
                    favorites.Add(favorite);
                }

            }
            catch(Exception)
            {
                return null;
            }


            return favorites;
        }



        /* Validation methods */
        private static bool validateUserInUsers(XDocument doc, User usr)
        {
            try
            {
                var query = from user in doc.Descendants("User")
                            where user.Element("Name").Value == usr.Name
                            && user.Element("Last").Value == usr.LastName
                            select new User(user.Element("Name").Value, user.Element("Last").Value);

                if (query.Any())
                    return true;

            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        private static bool validateHistoryInHistory(XDocument doc, string history)
        {
            try
            {
                var query = from fav in doc.Descendants("Url")
                            where fav.Value == history
                            select new string(fav.Value.ToCharArray());

                if (query.Any())
                    return true;

            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        private static bool validateFavoriteInFavorites(XDocument doc, Favorite favorite)
        {
            try
            {
                var query = from fav in doc.Descendants("Favorite")
                            where fav.Element("Url").Value == favorite.Url
                            select new string(fav.Element("Url").Value.ToCharArray());

                if (query.Any())
                    return true;

            }
            catch (Exception)
            {
                return false;
            }
            return false;

        }



        /* Private helper methods */
        private static void appendDotXmlIfNotPresent(ref string filename)
        {
            string extension = filename.Substring(filename.Length - 4);

            if (extension != ".xml")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(filename);
                sb.Append(".xml");

                filename = sb.ToString();
            }

        }
        private static bool formatUsersIfNotExist(string filename, User usr)
        {
            if (!File.Exists(filename))
            {

                XmlWriter writer = XmlWriter.Create(filename);

                writer.WriteStartDocument();
                writer.WriteStartElement("Users");

                writer.WriteStartElement("User");

                writer.WriteStartElement("Name");
                writer.WriteString(usr.Name);
                writer.WriteEndElement();

                writer.WriteStartElement("Last");
                writer.WriteString(usr.LastName);
                writer.WriteEndElement();


                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();

                return false;
            }
            return true;
        }
        private static void formatHistoryIfNotExist(string filename)
        {
            if (!File.Exists(filename))
            {

                XmlWriter writer = XmlWriter.Create(filename);

                writer.WriteStartDocument();
                writer.WriteStartElement("History");

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();

            }

        }
        private static void formatFavoritesIfNotExist(string filename)
        {
            if (!File.Exists(filename))
            {

                XmlWriter writer = XmlWriter.Create(filename);

                writer.WriteStartDocument();
                writer.WriteStartElement("Favorites");


                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();

            }
        }
    }
}
