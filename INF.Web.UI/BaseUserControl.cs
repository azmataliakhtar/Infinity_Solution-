using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using INF.Web.Data.BLL;
using INF.Web.Data.Entities;
using INF.Web.UI.UserRights;

namespace INF.Web.UI
{
    public class BaseUserControl : System.Web.UI.UserControl
    {
        public List<PageUserRight> PageUserRights
        {
            get
            {
                if ((ViewState["Admin_BaseUserControl_PageUserRights"] != null))
                {
                    return ViewState["Admin_BaseUserControl_PageUserRights"] as List<PageUserRight>;
                }
                return null;
            }
            set { ViewState["Admin_BaseUserControl_PageUserRights"] = value; }
        }

        protected CsUser GetLoggedInUser()
        {
            var identity = HttpContext.Current.User.Identity as FormsIdentity;
            if (identity != null)
            {
                string userName = identity.Ticket.Name;
                if (!(HttpContext.Current.User.Identity.IsAuthenticated))
                {
                    Response.RedirectTo("Login.aspx");
                }

                var userBusiness = new UserBusinessLogic();
                return userBusiness.GetUser(userName);
            }
            return null;
        }
    }

    public class BaseHeaderUserControl : BaseUserControl
    {
        public event EventHandler LogOutClick;

        protected virtual void OnLogOutClick(object sender, EventArgs args)
        {
            var handler = LogOutClick;
            if (handler != null)
            {
                handler(sender, args);
            }
        }
    }
}