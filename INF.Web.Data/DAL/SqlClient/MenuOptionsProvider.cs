using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class MenuOptionsProvider : DataAccess
    {
        private static MenuOptionsProvider _Instance;

        MenuOptionsProvider()
        {
            
        }

        static MenuOptionsProvider()
        {
            _Instance = new MenuOptionsProvider();
        }

        //public static MenuOptionsProvider Instance
        //{
        //    get { return _instance ?? (_instance = new MenuOptionsProvider()); }
        //}

        public static MenuOptionsProvider GetInstance(string connectionString)
        {
            ConnectionString = connectionString;
            return _Instance ?? (_Instance = new MenuOptionsProvider());
        }

        public IEnumerable<CsMenuOption> GetAllMenuOptions()
        {
            using (var session=Provider.CreateSessionFactory().CreateSession())
            {
                var allMenuOptions= session.FindAll<CsMenuOption>();
                return allMenuOptions.Where(o => o.IsEnabled);
            }
        }

        public IEnumerable<CsOptionDetail> GetOptionDetails(int optionId)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsOptionDetail>(" WHERE [OptionID] = " + optionId + "");
                return query.GetResults<CsOptionDetail>();
            }
        }

        public CsOptionDetail GetOptionDetailByID(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.Get<CsOptionDetail>(id);
            }
        }
    }
}
