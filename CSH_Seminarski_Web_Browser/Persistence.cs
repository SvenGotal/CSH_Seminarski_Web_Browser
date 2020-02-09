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



        /* Write functionalities */
        /// <summary>
        /// Writers the user in specified format to desired filename, if user already exists it adds user to the specified .xml format.
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
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
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
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
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
                    fav.Add(new XElement("Favorite", new XElement("Url", favorite.Url), new XElement("Last", favorite.Description)));

                    doc.Save(filename);

                }

            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }



        /* Read functionalities */
        public static List<string> ReadHistory(string filename)
        {
            //TODO reserved for varsion 1.2.
            throw new NotImplementedException();
        }
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
            catch (Exception ex)
            {
                return null;
            }
            return usr;
        }
        public static bool ReadUsersAll(string filename)
        {
            //TODO reserved for v.1.2.
            throw new NotImplementedException();
        }
        public static List<Favorite> ReadFavorites(string filename)
        {
            List<Favorite> favorites = new List<Favorite>();
            XDocument doc;

            try
            {
                appendDotXmlIfNotPresent(ref filename);
                doc = XDocument.Load(filename);

                var query = from favs in doc.Descendants("Favorite")
                            select new Favorite(favs.Element("Url").Value, favs.Element("Name").Value);

                foreach (Favorite favorite in query)
                {
                    favorites.Add(favorite);
                }

            }
            catch(Exception ex)
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
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        private static bool validateHistoryInHistory(XDocument doc, string history)
        {
            bool flag = false;
            try
            {
                var query = from fav in doc.Descendants("Url")
                            where fav.Value == history
                            select new string(fav.Value.ToCharArray());

                if (query.Any())
                    return true;

            }
            catch (Exception ex)
            {
                return flag;
            }
            return flag;
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
            catch (Exception ex)
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
