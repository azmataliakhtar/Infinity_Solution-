using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class StaticPageProvider : DataAccess
    {
        private static StaticPageProvider _instance;

        StaticPageProvider()
        {
        }

        static StaticPageProvider()
        {
            _instance = new StaticPageProvider();
        }

        public static StaticPageProvider Instance
        {
            get { return _instance ?? (_instance = new StaticPageProvider()); }
        }

        public CsStaticPage GetStaticPage(string pageName)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsStaticPage>(" WHERE [StaticPage] = '" + pageName + "'");
                var page = query.GetSingleResult<CsStaticPage>();
                return page;
            }
        }

        public CsStaticPage SaveStaticPage(CsStaticPage page)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                var savedPage = page.ID == 0 ? session.Insert(page) : session.Update(page);
                tranx.Commit();
                return savedPage;
            }
        }
    }
}
