using System;
using System.Web;
using INF.Web.Data.BLL;
using log4net;

namespace INF.Web.UI.Settings
{
    public class EPATheme
    {
        private log4net.ILog _log = log4net.LogManager.GetLogger(typeof(EPATheme));

        
        private static EPATheme _instance;

        static EPATheme()
        {
            _instance = new EPATheme();
        }

        EPATheme()
        {
        }

        public static EPATheme Current
        {
            get { return _instance ?? (_instance = new EPATheme()); }
        }

        public ThemeSettings Themes
        {
            get
            {
                var cachedTheme = HttpContext.Current.Cache.Get(WebConstants.THEMES_SETTING) as ThemeSettings;
                if (cachedTheme != null && WebsiteConfig.Instance.EnableCaching)
                {
                    return cachedTheme;
                }

                var ts = LoadThemeSettings();
                if (WebsiteConfig.Instance.EnableCaching)
                {
                    HttpContext.Current.Cache.Insert(WebConstants.THEMES_SETTING, ts, null,
                        DateTime.Now.AddSeconds(WebsiteConfig.Instance.CacheDuration),
                        TimeSpan.Zero);
                }
                
                return ts;
            }
        }

        public ThemeSettings LoadThemeSettings()
        {
            ThemeSettings ts = null;
            try
            {
                ts = new ThemeSettings();
                var bll = new ThemesSettingsBusinessLogic();
                var logoAndSlogan = bll.LoadLogoAndSloganSettings();
                var header = bll.LoadHeaderSettings();
                var footer = bll.LoadFooterSettings();
                var homepage = bll.LoadHomePageSettings();
                var navs = bll.LoadNavigationImagesSettings();
                var menuCats = bll.LoadMenuCategorySettings();
                var websiteInfo = bll.LoadWebsiteInformation();
                var colors = bll.LoadColorSettings();

                if (logoAndSlogan != null)
                {
                    ts.Logo = logoAndSlogan.LogoUrl.Replace("~","");
                    ts.Slogan = logoAndSlogan.Slogan;
                }

                ts.BackgroundHeader = header != null && header.ImageUrl.Length > 0
                                          ? header.ImageUrl.Replace("~", "")
                                          : "/App_Themes/" + WebsiteConfig.Instance.INFTheme + "/images/heading.png";

                ts.BackgroundFooter = footer != null && footer.ImageUrl.Length > 0
                                          ? footer.ImageUrl.Replace("~", "")
                                          : "/App_Themes/" + WebsiteConfig.Instance.INFTheme + "/images/footer.png";

                ts.BackgroundHomePage = homepage != null && homepage.ImageUrl.Length > 0
                                            ? homepage.ImageUrl.Replace("~", "")
                                            : "/App_Themes/" + WebsiteConfig.Instance.INFTheme + "/images/web-background.png";

                if (navs != null && navs.ImageUrl.Length > 0)
                {
                    ts.BackgroundNavigation = navs.ImageUrl.Replace("~", "");
                    ts.BackgroundNavigationHover = navs.ImageHoverUrl.Replace("~", "");
                }
                else
                {
                    ts.BackgroundNavigation = "/App_Themes/" + WebsiteConfig.Instance.INFTheme + "/images/button.png";
                    ts.BackgroundNavigationHover = "/App_Themes/" + WebsiteConfig.Instance.INFTheme + "/images/button_hover.png";
                }

                if(menuCats != null)
                {
                    ts.MenuCategoryWidth = string.IsNullOrEmpty(menuCats.Width) ? 180 : Convert.ToInt32(menuCats.Width);
                    ts.MenuCategoryHeight = string.IsNullOrEmpty(menuCats.Height) ? 28 : Convert.ToInt32(menuCats.Height);
                }
                else
                {
                    ts.MenuCategoryWidth =  180 ;
                    ts.MenuCategoryHeight = 28;
                }

                if(websiteInfo != null)
                {
                    ts.WebsiteMeta = websiteInfo.Meta;
                    ts.WebsiteName = websiteInfo.Name;
                }

                if(colors != null)
                {
                    ts.BaseColor = string.IsNullOrEmpty(colors.BaseColor) ? "#FF0000" : "#" + colors.BaseColor;
                    ts.BackColor = string.IsNullOrEmpty(colors.BackColor) ? "#FF0000" : "#" + colors.BackColor;
                    ts.AddToCartImageUrl = string.IsNullOrEmpty(colors.AddToCartImageUrl) ? "/App_Themes/" + WebsiteConfig.Instance.INFTheme + "/images/AddToCart.png" : colors.AddToCartImageUrl.Replace("~", "");
                    ts.CheckOutImageUrl = string.IsNullOrEmpty(colors.CheckOutImageUrl) ? "/App_Themes/" + WebsiteConfig.Instance.INFTheme + "/images/CheckOut.png" : colors.CheckOutImageUrl.Replace("~", "");
                    ts.EditOrderImageUrl = string.IsNullOrEmpty(colors.EditOrderImageUrl) ? "/App_Themes/" + WebsiteConfig.Instance.INFTheme + "/images/EditOrder.png" : colors.EditOrderImageUrl.Replace("~", "");
                    ts.ConfirmOrderImageUrl = string.IsNullOrEmpty(colors.ConfirmOrderImageUrl) ? "/App_Themes/" + WebsiteConfig.Instance.INFTheme + "/images/ConfirmOrder.png" : colors.ConfirmOrderImageUrl.Replace("~", "");
                }
            }
            catch(Exception ex)
            {
                _log.Error("Could not load the setting themes.", ex);
                throw;
            }
           
            return ts;
        }
    }

    public class ThemeSettings
    {
        public string WebsiteName { get; set; }
        public string WebsiteMeta { get; set; }

        public bool ShouldBeUpdated { get; set; }

        public string Logo { get; set; }

        public string Slogan { get; set; }

        public string BackgroundHeader { get; set; }

        public string BackgroundFooter { get; set; }

        public string BackgroundNavigation { get; set; }

        public string BackgroundNavigationHover { get; set; }

        public string BackgroundHomePage { get; set; }

        public int MenuCategoryWidth { get; set; }
        public int MenuCategoryHeight { get; set; }

        public string BaseColor { get; set; }
        public string BackColor { get; set; }
        public string EditOrderImageUrl { get; set; }
        public string ConfirmOrderImageUrl { get; set; }
        public string CheckOutImageUrl { get; set; }
        public string AddToCartImageUrl { get; set; }
    }
}