using System;
using System.ComponentModel;
using System.Configuration;

namespace INF.Web.UI.Settings
{
    public class WebsiteConfig
    {
        private static WebsiteConfig _Instance;

        public bool EnaleCachingMasterData { get; set; }
        public bool EnableCaching { get; set; }
        public int CacheDuration { get; set; }

        public string OrderReviewReferrerUrl { get; set; }
        public string OrderConfirmationReferrerUrl { get; set; }
        public double PayByCardFee { get; set; }
        public double MinOrderValue { get; set; }

        public string ConnectionString { get; set; }

        private string _infTheme = "default";
        public string INFTheme
        {
            get { return _infTheme; }
        }

        private string _infMasterPageFile = "~/SiteMaster.master";
        public string INFMasterPageFile
        {
            get { return _infMasterPageFile; }
        }

        static WebsiteConfig()
        {
            _Instance = new WebsiteConfig();
        }

        WebsiteConfig()
        {
            GetWebSettings();
        }

        public static WebsiteConfig Instance
        {
            get { return _Instance ?? (_Instance = new WebsiteConfig()); }
        }

        private void GetWebSettings()
        {

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EnableCaching"]))
                {
                    EnableCaching = ("true").Equals(ConfigurationManager.AppSettings["EnableCaching"], StringComparison.CurrentCultureIgnoreCase);
                }

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EnaleCachingMasterData"]))
                {
                    EnaleCachingMasterData = ("true").Equals(ConfigurationManager.AppSettings["EnaleCachingMasterData"], StringComparison.CurrentCultureIgnoreCase);
                }

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheDuration"]))
                {
                    int cacheDuration = 0;
                    CacheDuration = int.TryParse(ConfigurationManager.AppSettings["CacheDuration"], out cacheDuration) ? cacheDuration : 0;
                }

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["OrderReviewReferrerUrl"]))
                {
                    OrderReviewReferrerUrl = ConfigurationManager.AppSettings["OrderReviewReferrerUrl"];
                }

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["OrderConfirmationReferrerUrl"]))
                {
                    OrderConfirmationReferrerUrl = ConfigurationManager.AppSettings["OrderConfirmationReferrerUrl"];
                }

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PayByCardFee"]))
                {
                    double payByCardFee = 0;
                    PayByCardFee = double.TryParse(ConfigurationManager.AppSettings["PayByCardFee"], out payByCardFee) ? payByCardFee : 0.5;
                }

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MinOrderValue"]))
                {
                    double minOrderValue = 0;
                    MinOrderValue = double.TryParse(ConfigurationManager.AppSettings["MinOrderValue"], out minOrderValue) ? minOrderValue : 8;
                }

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["INF_Theme"]))
                {
                    _infTheme = ConfigurationManager.AppSettings["INF_Theme"];
                }
                else
                {
                    _infTheme = WebConstants.DEFAULT_THEME;
                }

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["INF_MasterPageFile"]))
                {
                    _infMasterPageFile = ConfigurationManager.AppSettings["INF_MasterPageFile"];
                }

            if (ConfigurationManager.ConnectionStrings["PizzaWebConnectionString"] == null ||
                string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["PizzaWebConnectionString"].ConnectionString))
            {
                throw new Exception("PizzaWebConnectionString is not initialized yet. Please check the Web.config file.");
            }
            else
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["PizzaWebConnectionString"].ConnectionString;
            }
        }
    }
}