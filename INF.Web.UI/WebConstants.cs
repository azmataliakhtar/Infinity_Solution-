using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INF.Web.UI
{
    public class WebConstants
    {
        #region "Session Keys"
        public const string PLACING_ORDER_TEMP_BASKET_ID = "INF.Web.PLACING_ORDER_TEMP_BASKET_ID";

        #endregion

        public const string DEFAULT_THEME = "default";
        //public const string SSN_SELECTED_THEME = "INF.Web.UI.BasePage.SSN_SELECTED_THEME";
        public const string THEMES_SETTING = "INF.Web.UI.Settings.CURRENT_THEME";

        public const string ERROR_PAGE = "~/Error.aspx";
    }
}