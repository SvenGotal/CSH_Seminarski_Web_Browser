using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSH_Seminarski_Web_Browser
{
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
            set 
            {

            } 
        }
        public string LastName { get; set; }
        //public List<string> Favorites 
        
        //{ 
        //    get
        //    {
        //        return favorites.cl
        //    }
        //}

        //public static IList<T> Clone(this IList<T> source) 
        //{
        //    List<T> clone = new List<T>();
        //    foreach (var fav in source)
        //    {
        //        clone.Add(fav);
        //    }
        //    return clone;
        //}

    }
}
