using System;
using System.Configuration;

namespace INF.Web.Data
{
    public class WebSettings
    {
        private static WebSettings _Instance;

        public bool EnableCaching { get; set; }
        public int CacheDuration { get; set; }
        public bool EnaleCachingMasterData { get; set; }

        public string ConnectionString { get; set; }

        static WebSettings()
        {
            _Instance = new WebSettings();
        }

        private WebSettings()
        {
            ReadSettings();
        }

        public static WebSettings Instance
        {
            get { return _Instance ?? (_Instance = new WebSettings()); }
        }

        private void ReadSettings()
        {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EnableCaching"]))
                {
                EnableCaching = ("true").Equals(ConfigurationManager.AppSettings["EnableCaching"], StringComparison.CurrentCultureIgnoreCase);
                }

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CacheDuration"]))
                {
                    int cacheDuration = 0;
                CacheDuration = int.TryParse(ConfigurationManager.AppSettings["CacheDuration"], out cacheDuration) ? cacheDuration : 0;
                }

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EnaleCachingMasterData"]))
                {
                EnaleCachingMasterData = ("true").Equals(ConfigurationManager.AppSettings["EnaleCachingMasterData"], StringComparison.CurrentCultureIgnoreCase);
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