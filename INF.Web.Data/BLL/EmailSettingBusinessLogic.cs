using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.DAL.SqlClient;
using INF.Web.Data.Entities;

namespace INF.Web.Data.BLL
{
    public class EmailSettingBusinessLogic : BaseBusinessLogic
    {
        public CsEmailSetting GetFirstEmailSetting()
        {
            //string key = string.Format("EmailSetting_{0}", 0);
            //if (WebSettings.Settings.EnaleCachingMasterData && Cache[key] != null)
            //    return (CsEmailSetting)Cache[key];

            var page = EmailSettingProvider.Instance.GetFirstEmailSetting();
            //CacheData(key, page);
            return page;
        }
    }
}
