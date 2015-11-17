using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INF.Web.UI
{
    public class Theme
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }

        public Theme(string name)
        {
            this.Name = name;
        }
    }
}