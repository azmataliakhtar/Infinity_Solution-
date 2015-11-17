using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INF.Web.Data.Entities;

namespace INF.Web.UI.UserRights
{
    public class PageUserRight
    {
        public string Title { get; set; }

        public string ParentPageID { get; set; }

        public string PageID { get; set; }

        public string FunctionID { get; set; }

        public UserRoles UserRole { get; set; }

        public UserRight Rights { get; set; }

        public bool RenderAsNavigationItem { get; set; }
    }
}