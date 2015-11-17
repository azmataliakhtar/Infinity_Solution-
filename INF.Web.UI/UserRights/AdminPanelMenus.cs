using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INF.Web.Data.Entities;

namespace INF.Web.UI.UserRights
{
    public class AdminPanelMenus
    {
        public static List<PageUserRight> DefinePageUserRights()
        {
            var pageUserRights = new List<PageUserRight>();

            //******************************************************************************
            // Dashboard
            //******************************************************************************
            //Dashboard.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Dashboard.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "",
                Title = "Dashboard",
                Rights = new UserRight{AllowView = true,AllowEdit = true,AllowDelete = true,AllowExport = true}
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Dashboard.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "",
                Title = "Dashboard",
                Rights = new UserRight{AllowView = true,AllowEdit = true,AllowDelete = true,AllowExport = true}
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Dashboard.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "",
                Title = "Dashboard",
                Rights = new UserRight{AllowView = true,AllowEdit = true,AllowDelete = true,AllowExport = true}
            });

            //Dashboad -> TodayOrders.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "TodayOrders.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "Dashboard.aspx",
                Title = "Today Orders",
                Rights = new UserRight { AllowView = true, AllowEdit = true, AllowDelete = true, AllowExport = true }
                ,RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "TodayOrders.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "Dashboard.aspx",
                Title = "Today Orders",
                Rights = new UserRight { AllowView = true, AllowEdit = true, AllowDelete = true, AllowExport = true }
                ,RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "TodayOrders.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "Dashboard.aspx",
                Title = "Today Orders",
                Rights = new UserRight { AllowView = true, AllowEdit = true, AllowDelete = true, AllowExport = true }
                ,RenderAsNavigationItem = true
            });

            //TodayOrders -> OrderView.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "OrderView.aspx",UserRole = UserRoles.Administrator,ParentPageID = "Dashboard.aspx",Title = "OrderView",
                Rights = new UserRight{AllowView = true,AllowEdit = true,AllowDelete = true,AllowExport = true}
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "OrderView.aspx",UserRole = UserRoles.Manager,ParentPageID = "Dashboard.aspx",Title = "OrderView",
                Rights = new UserRight{AllowView = true,AllowEdit = true,AllowDelete = true,AllowExport = true}
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "OrderView.aspx",UserRole = UserRoles.RestrictedUser,ParentPageID = "Dashboard.aspx",Title = "OrderView",
                Rights = new UserRight{AllowView = true,AllowEdit = true,AllowDelete = true,AllowExport = true}
            });

            //******************************************************************************
            // Dashboard -> Pending Orders
            //******************************************************************************
            // PendingOrders.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "PendingOrders.aspx",UserRole = UserRoles.Administrator,ParentPageID = "Dashboard.aspx",Title = "Pending Orders",
                Rights = new UserRight{AllowView = true,AllowEdit = true,AllowDelete = true,AllowExport = true},
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "PendingOrders.aspx",UserRole = UserRoles.Manager,ParentPageID = "Dashboard.aspx",Title = "Pending Orders",
                Rights = new UserRight{AllowView = true,AllowEdit = false,AllowDelete = false,AllowExport = false},
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "PendingOrders.aspx",UserRole = UserRoles.RestrictedUser,ParentPageID = "Dashboard.aspx",Title = "Pending Orders",
                Rights = new UserRight{AllowView = false,AllowEdit = false,AllowDelete = false,AllowExport = false},
                RenderAsNavigationItem =true
            });

            // Dashboard -> PendingOrders -> OrderDetails.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "OrderDetails.aspx",UserRole = UserRoles.Administrator,ParentPageID = "Dashboard.aspx",Title = "Order Details",
                Rights = new UserRight{AllowView = true,AllowEdit = true,AllowDelete = true,AllowExport = true}
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "OrderDetails.aspx",UserRole = UserRoles.Manager,ParentPageID = "Dashboard.aspx",Title = "Order Details",
                Rights = new UserRight{AllowView = true,AllowEdit = false,AllowDelete = false,AllowExport = false}
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "OrderDetails.aspx",UserRole = UserRoles.RestrictedUser,ParentPageID = "Dashboard.aspx",Title = "Order Details",
                Rights = new UserRight{AllowView = false,AllowEdit = false,AllowDelete = false,AllowExport = false}
            });

            //******************************************************************************
            // Settings Pages
            //******************************************************************************
            //GeneralSettings.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "GeneralSettings.aspx",UserRole = UserRoles.Administrator,ParentPageID = "",Title = "Settings",
                Rights = new UserRight{AllowView = true,AllowEdit = true,AllowDelete = true,AllowExport = true}
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "GeneralSettings.aspx",UserRole = UserRoles.Manager,ParentPageID = "",Title = "Settings",
                Rights = new UserRight{AllowView = true,AllowEdit = false,AllowDelete = false,AllowExport = false}
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "GeneralSettings.aspx",UserRole = UserRoles.RestrictedUser,ParentPageID = "",Title = "Settings",
                Rights = new UserRight{AllowView = false,AllowEdit = false,AllowDelete = false,AllowExport = false}
            });

            //Settings.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Settings.aspx",UserRole = UserRoles.Administrator,ParentPageID = "GeneralSettings.aspx",Title = "Restaurant Setting",
                Rights = new UserRight{AllowView = true,AllowEdit = true,AllowDelete = true,AllowExport = true},
                RenderAsNavigationItem=true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Settings.aspx",UserRole = UserRoles.Manager,ParentPageID = "GeneralSettings.aspx",Title = "Restaurant Setting",
                Rights = new UserRight{AllowView = false,AllowEdit = false,AllowDelete = false,AllowExport = false},
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Settings.aspx",UserRole = UserRoles.RestrictedUser,ParentPageID = "GeneralSettings.aspx",Title = "Restaurant Setting",
                Rights = new UserRight{AllowView = false,AllowEdit = false,AllowDelete = false,AllowExport = false},
                RenderAsNavigationItem = true
            });

            //Info.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Info.aspx",UserRole = UserRoles.Administrator,ParentPageID = "GeneralSettings.aspx",Title = "Website Information",
                Rights = new UserRight{AllowView = true,AllowEdit = true,AllowDelete = true,AllowExport = true},
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Info.aspx",UserRole = UserRoles.Manager,ParentPageID = "GeneralSettings.aspx",Title = "Website Information",
                Rights = new UserRight{AllowView = false,AllowEdit = false,AllowDelete = false,AllowExport = false},
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Info.aspx",UserRole = UserRoles.RestrictedUser,ParentPageID = "GeneralSettings.aspx",Title = "Website Information",
                Rights = new UserRight{AllowView = false,AllowEdit = false,AllowDelete = false,AllowExport = false},
                RenderAsNavigationItem = true
            });

            //Themes.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Themes.aspx",UserRole = UserRoles.Administrator,ParentPageID = "GeneralSettings.aspx",Title = "Theme Setting",
                Rights = new UserRight{AllowView = true,AllowEdit = true,AllowDelete = true,AllowExport = true},
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Themes.aspx",UserRole = UserRoles.Manager,ParentPageID = "GeneralSettings.aspx",Title = "Theme Setting",
                Rights = new UserRight{AllowView = true,AllowEdit = false,AllowDelete = false,AllowExport = false},
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Themes.aspx",UserRole = UserRoles.RestrictedUser,ParentPageID = "GeneralSettings.aspx",Title = "Theme Setting",
                Rights = new UserRight{AllowView = false,AllowEdit = false,AllowDelete = false,AllowExport = false},
                RenderAsNavigationItem = true
            });

            //StaticPages.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "StaticPages.aspx",UserRole = UserRoles.Administrator,ParentPageID = "GeneralSettings.aspx",Title = "Static Pages",
                Rights = new UserRight{AllowView = true,AllowEdit = true,AllowDelete = true,AllowExport = true},
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "StaticPages.aspx",UserRole = UserRoles.Manager,ParentPageID = "GeneralSettings.aspx",Title = "Static Pages",
                Rights = new UserRight{AllowView = true,AllowEdit = false,AllowDelete = false,AllowExport = false},
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "StaticPages.aspx",UserRole = UserRoles.RestrictedUser,ParentPageID = "GeneralSettings.aspx",Title = "Static Pages",
                Rights = new UserRight{AllowView = false,AllowEdit = false,AllowDelete = false,AllowExport = false},
                RenderAsNavigationItem = true
            });

            //Users.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Users.aspx",UserRole = UserRoles.Administrator,ParentPageID = "GeneralSettings.aspx",Title = "System Users",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Users.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "GeneralSettings.aspx",
                Title = "System Users",
                Rights = new UserRight
                {
                    AllowView = false,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Users.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "GeneralSettings.aspx",
                Title = "System Users",
                Rights = new UserRight
                {
                    AllowView = false,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                },
                RenderAsNavigationItem = true
            });

            //******************************************************************************
            // Settings -> Menu Category
            //******************************************************************************
            //Category.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Category.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "",
                Title = "Manage Category",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Category.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "",
                Title = "Manage Category",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                }
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Category.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "",
                Title = "Manage Category",
                Rights = new UserRight
                {
                    AllowView = false,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                }
            });
            // Navigation Item
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Category.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "Category.aspx",
                Title = "Category",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Category.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "Category.aspx",
                Title = "Category",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Category.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "Category.aspx",
                Title = "Category",
                Rights = new UserRight
                {
                    AllowView = false,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                },
                RenderAsNavigationItem = true
            });

            //MenuItem.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "MenuItem.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "Category.aspx",
                Title = "Menu Item",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "MenuItem.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "Category.aspx",
                Title = "Menu Item",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "MenuItem.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "Category.aspx",
                Title = "Menu Item",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });

            //SubMenuItem.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "SubMenuItem.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "Category.aspx",
                Title = "Sub Menu Item",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = false
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "SubMenuItem.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "Category.aspx",
                Title = "Sub Menu Item",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = false
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "SubMenuItem.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "Category.aspx",
                Title = "Sub Menu Item",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = false
            });

            //******************************************************************************
            // Postcode Pages
            //******************************************************************************
            //Postcode.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Postcode.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "Category.aspx",
                Title = "Postcode",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Postcode.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "Category.aspx",
                Title = "Postcode",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Postcode.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "Category.aspx",
                Title = "Postcode",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });

            //******************************************************************************
            // Manage Customer Pages
            //******************************************************************************
            //Customers.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Customers.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "",
                Title = "Manage Customer",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                }
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Customers.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "",
                Title = "Manage Customer",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                }
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Customers.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "",
                Title = "Manage Customer",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                }
            });

            //CustomerDetails.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "CustomerDetails.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "Customers.aspx",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "CustomerDetails.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "Customers.aspx",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "CustomerDetails.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "Customers.aspx",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                },
                RenderAsNavigationItem = true
            });

            //CustomerOrders.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "CustomerOrders.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "Customers.aspx",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "CustomerOrders.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "Customers.aspx",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "CustomerOrders.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "Customers.aspx",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                },
                RenderAsNavigationItem = true
            });

            //CustomerAddressEdit.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "CustomerAddressEdit.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "Customers.aspx",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "CustomerAddressEdit.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "Customers.aspx",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "CustomerAddressEdit.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "Customers.aspx",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                },
                RenderAsNavigationItem = true
            });

            
            //******************************************************************************
            // Reporting Pages
            //******************************************************************************
            //Reporting.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Reporting.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "",
                Title = "Reporting",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                }
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Reporting.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "",
                Title = "Reporting",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                }
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "Reporting.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "",
                Title = "Reporting",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                }
            });

            //ReportSettings.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "ReportSettings.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "Reporting.aspx",
                Title = "Report Setting",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "ReportSettings.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "Reporting.aspx",
                Title = "Report Setting",
                Rights = new UserRight
                {
                    AllowView = false,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "ReportSettings.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "Reporting.aspx",
                Title = "Report Setting",
                Rights = new UserRight
                {
                    AllowView = false,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                },
                RenderAsNavigationItem = true
            });

            //BasicReport.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "BasicReport.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "Reporting.aspx",
                Title = "Basic Report",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "BasicReport.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "Reporting.aspx",
                Title = "Basic Report",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "BasicReport.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "Reporting.aspx",
                Title = "Basic Report",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                },
                RenderAsNavigationItem = true
            });


            //SalesReport.aspx
            pageUserRights.Add(new PageUserRight
            {
                PageID = "SalesReport.aspx",
                UserRole = UserRoles.Administrator,
                ParentPageID = "Reporting.aspx",
                Title = "Sales Report",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = true,
                    AllowDelete = true,
                    AllowExport = true
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "SalesReport.aspx",
                UserRole = UserRoles.Manager,
                ParentPageID = "Reporting.aspx",
                Title = "Sales Report",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                },
                RenderAsNavigationItem = true
            });
            pageUserRights.Add(new PageUserRight
            {
                PageID = "SalesReport.aspx",
                UserRole = UserRoles.RestrictedUser,
                ParentPageID = "Reporting.aspx",
                Title = "Sales Report",
                Rights = new UserRight
                {
                    AllowView = true,
                    AllowEdit = false,
                    AllowDelete = false,
                    AllowExport = false
                },
                RenderAsNavigationItem = true
            });

            return pageUserRights;
        }
    }
}