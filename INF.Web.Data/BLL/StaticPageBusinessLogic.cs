using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.DAL.SqlClient;
using INF.Web.Data.Entities;

namespace INF.Web.Data.BLL
{
    public class StaticPageBusinessLogic : BaseBusinessLogic
    {
        public CsStaticPage GetStaticPage(string pageName)
        {
            string key = string.Format("StaticPages_{0}", pageName);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
                return (CsStaticPage) Cache[key];

            var page = StaticPageProvider.Instance.GetStaticPage(pageName);
            CacheData(key, page);
            return page;
        }

        public CsStaticPage SaveStaticPage(CsStaticPage page)
        {
            string key = string.Format("StaticPages_{0}", page.PageName);
            var savedPage = StaticPageProvider.Instance.SaveStaticPage(page);
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
            {
                Cache[key] = savedPage;
            }
            return savedPage;
        }
    }
}
