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

    static class Extensions
    {
        public static List<T> Clone<T>(this List<T> source) where T : ICloneable
        {
            List<T> clone = new List<T>();
            foreach (T fav in source)
            {
                clone.Add(fav);
            }
            return clone;
        }

    }


    class User
    {
        private string name;
        private string lname;
        private List<string> favorites;

        public string Name 
        { 
            get
            {
                return name;
            }
        }
        public string LastName 
        {
            get
            {
                return lname;
            }
        }
        public List<string> Favorites

        {
            get
            {
                return favorites.Clone<string>();
            }
        }

        public User(string name, string lname)
        {
            this.name = name;
            this.lname = lname;

            createUsers();

            createHistory();
            createFavorites();


        }

        public bool AddUserHistory()
        {
            return true;
        }

        public bool ReadUserHistory()
        {
            return true;
        }


        public bool AddUserFavorites()
        {
            return true;
        }

        public bool ReadUserFavorites()
        {
            return true;
        }

        /// <summary>
        /// Creates the .xml file for Favorites for this user.
        /// </summary>
        /// <returns>True if file did not exist previously</returns>
        private bool createFavorites()
        {
            string filename_favs = name + ".xml";

            if (!File.Exists(filename_favs))
            {
                XmlWriter xwriter = XmlWriter.Create(filename_favs);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Creates the .xml file for History for this user.
        /// </summary>
        /// <returns>True if file did not exist previously</returns>
        private bool createHistory()
        {
            string filename_hist = name + "_history.xml";

            if (!File.Exists(filename_hist))
            {
                XmlWriter xwriter = XmlWriter.Create(filename_hist);
                return true;
            }
            return false;
        }

        private bool createUsers()
        {
            if(!File.Exists("Users.xml"))
            {
                XmlWriter writer = XmlWriter.Create("Users.xml");
                writer.WriteStartDocument();
                writer.WriteStartElement("Users");

                writer.WriteStartElement("User");

                writer.WriteStartElement("Name");
                writer.WriteString(name);
                writer.WriteEndElement();

                writer.WriteStartElement("Last");
                writer.WriteString(lname);
                writer.WriteEndElement();


                writer.WriteEndDocument();
                writer.Close();

                return true;
            }
            else
            {
                addUser();
            }
            return false;
        }
        private bool addUser()
        {
            if (!validateUser())
            {
                XDocument xdoc = XDocument.Load("Users.xml");
                XElement user = xdoc.Element("Users");
                user.Add(new XElement("User", new XElement("Name", name),
                                              new XElement("Last", lname)));
                xdoc.Save("Users.xml");
                

            }
            return true;
        }

        /// <summary>
        /// Checks if the user already exists within the Users.xml.
        /// </summary>
        /// <returns>Returns true if user already exists, false otherwise.</returns>
        private bool validateUser()
        {
            XDocument doc = XDocument.Load("Users.xml");

            var findUser = from users in doc.Descendants("User")
                           where users.Element("Name").Value == name && users.Element("Last").Value == lname
                           select new
                           {
                               name = users.Element("Name").Value,
                               lname = users.Element("Last").Value
                           };

            if (findUser.Any())
                return true;

            return false;
        }

    }
}
