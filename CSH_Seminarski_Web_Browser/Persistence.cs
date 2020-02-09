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

        public static bool ReadUsersAll(string filename)
        {
            //TODO reserved for v.1.2.
            throw new NotImplementedException();
        }


        public static List<string> ReadFavorites(string filename)
        {
            throw new NotImplementedException();
        }
        public static bool WriteFavorite(string filename, string favorite)
        {
            throw new NotImplementedException();
        }
        public static List<string> ReadHistory(string filename)
        {
            //TODO reserved for varsion 1.2.
            throw new NotImplementedException();
        }
        public static bool WriteHistory(string filename, string history)
        {

            appendDotXmlIfNotPresent(ref filename);

            XDocument doc;

            try
            {
                formatHistoryIfNotExist(filename, history);

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
        /// <summary>
        /// Writers the user in specified format to desired filename, if user already exists it adds user to the specified .xml format.
        /// </summary>
        /// <param name="filename">Path to filename, .xml.</param>
        /// <param name="usr">User to write</param>
        /// <returns>True if the method succeeds or user already exists, false if method fails to write user to .xml file.</returns>
        public static bool WriteUser(string filename, User usr)
        {
            appendDotXmlIfNotPresent(ref filename);

            XDocument doc;
            try
            {
                formatUsersIfNotExist(filename, usr);

                doc = XDocument.Load(filename);

                if (!validateUserInUsers(doc, usr))
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
        private static bool validateHistory(XDocument doc, string history)
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
        private static void formatUsersIfNotExist(string filename, User usr)
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

            }

        }
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
        private static void formatHistoryIfNotExist(string filename, string favorite)
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
    }
}
