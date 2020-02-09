using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSH_Seminarski_Web_Browser
{
    public class Favorite
    {
        private string url;
        private string name;

        public Favorite() { }
        public Favorite(string url, string description)
        {
            this.url = url;
            this.name = description;
        }

        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
            }
        }
        public string Description
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

    }
}
