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

    /// <summary>
    /// Extension of clone functionality for List, only for this assembly.
    /// </summary>
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


    public class User
    {
        private string name;
        private string lname;
        private List<Favorite> favorites;
        private List<string> history;

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
        public List<Favorite> Favorites
        {
            get
            {
                return favorites.Clone<Favorite>();
            }
            set
            {
                this.favorites = value;
            }
        }
        public List<string> History
        {
            get
            {
                return history;
            }
            set
            {
                this.history = value;
            }
        }

        public User() 
        {
            this.name = "";
            this.lname = "";
            this.favorites = new List<Favorite>();
        }
        public User(string name, string lname)
        {
            this.name = name;
            this.lname = lname;
            this.favorites = new List<Favorite>();
        }
        public User(string name, string lname, List<Favorite> favorites)
        {
            this.name = name;
            this.lname = lname;
            this.favorites = favorites;
        }
        public User(string name, string lname, List<Favorite> favorites, List<string> history)
        {
            this.name = name;
            this.lname = lname;
            this.favorites = favorites;
            this.history = history;
        }

    }
}
