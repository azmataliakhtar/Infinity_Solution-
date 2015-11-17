using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using INF.Web.Data.BLL;
using INF.Web.Data.Entities;
using INF.Web.UI.UserRights;

namespace INF.Web.UI
{
    public class AdminPage : BasePage
    {
        protected int CustomerId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["CustomerId"]))
                {
                    return -1;
                }
                return Convert.ToInt32(Request.QueryString["CustomerId"]);
            }
        }

        protected int AddressId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["AddressId"]))
                {
                    return -1;
                }
                return Convert.ToInt32(Request.QueryString["AddressId"]);
            }
        }

        protected int OrderID
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["OrderID"]))
                {
                    return -1;
                }
                return Convert.ToInt32(Request.QueryString["OrderID"]);
            }
        }

        protected int UserId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["UserId"]))
                {
                    return -1;
                }
                return Convert.ToInt32(Request.QueryString["UserId"]);
            }
        }

        protected string PageID { get; set; }
        protected string FunctionID { get; set; }

        protected override void OnPreLoad(EventArgs e)
        {
            var vFormIdentity = HttpContext.Current.User.Identity as FormsIdentity;
            if (vFormIdentity != null)
            {
                string vRole = vFormIdentity.Ticket.UserData;
                if (!(HttpContext.Current.User.Identity.IsAuthenticated && ADMIN_ROLE.Equals(vRole)))
                {
                    Response.RedirectTo("Login.aspx");
                }

                List<PageUserRight> userRights = AdminPanelMenus.DefinePageUserRights();
                CsUser loggedInUser = GetLoggedInUser();
                if ((loggedInUser == null))
                    return;

                string requestPage = Path.GetFileName(Request.PhysicalPath);
                if (requestPage != null && !requestPage.Equals("LogOut.aspx", StringComparison.CurrentCultureIgnoreCase))
                {
                    var availablePages = userRights.Where(
                            p => String.Equals(p.PageID, requestPage, StringComparison.CurrentCultureIgnoreCase) &&
                                p.UserRole == loggedInUser.UserRole && p.Rights.AllowView);
                    if (!availablePages.Any())
                    {
                        Response.RedirectTo("Default.aspx");
                    }
                }
            }
        }

        protected CsUser GetLoggedInUser()
        {
            var identity = HttpContext.Current.User.Identity as FormsIdentity;
            if (identity != null)
            {
                string userName = identity.Ticket.Name;
                if (!(HttpContext.Current.User.Identity.IsAuthenticated))
                {
                    Response.RedirectTo("~/Admin/Login.aspx");
                }

                var userBusiness = new UserBusinessLogic();
                return userBusiness.GetUser(userName);
            }
            return null;
        }

        protected string CurrentUserName()
        {
            CsUser loggedInUser = GetLoggedInUser();
            if ((loggedInUser == null))
            {
                return "UnKnownUser";
            }
            else
            {
                return loggedInUser.UserName;
            }
        }

        protected UserRoles RoleOfLoggedInUser()
        {
            CsUser loggedInUser = GetLoggedInUser();
            return loggedInUser.UserRole;
        }
    }
}