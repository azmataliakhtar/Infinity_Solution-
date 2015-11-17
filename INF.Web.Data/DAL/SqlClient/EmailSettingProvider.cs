using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class EmailSettingProvider : DataAccess
    {
        private static EmailSettingProvider _instance;

        EmailSettingProvider()
        {
        }

        static EmailSettingProvider()
        {
            _instance = new EmailSettingProvider();
        }

        public static EmailSettingProvider Instance
        {
            get { return _instance ?? (_instance = new EmailSettingProvider()); }
        }

        public CsEmailSetting GetFirstEmailSetting()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.FindAll<CsEmailSetting>().FirstOrDefault();
            }
        }
    }
}
