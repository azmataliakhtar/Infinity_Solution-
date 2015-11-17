using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.DAL.SqlClient;
using INF.Web.Data.Entities;

namespace INF.Web.Data.BLL
{
    public class EmailSenderBusinessLogic : BaseBusinessLogic
    {
        public IEnumerable<CsEmailSender> GetAll()
        {
            string key = string.Format("EmailSender_{0}", 0);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
                return (IEnumerable<CsEmailSender>)Cache[key];

            var page = EmailSenderProvider.Instance.GetAll();
            CacheData(key, page);
            return page;
        }
    }
}
