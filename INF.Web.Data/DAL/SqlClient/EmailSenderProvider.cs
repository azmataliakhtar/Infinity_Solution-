using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class EmailSenderProvider : DataAccess
    {
        private static EmailSenderProvider _instance;

        EmailSenderProvider()
        {
        }

        static EmailSenderProvider()
        {
            _instance = new EmailSenderProvider();
        }

        public static EmailSenderProvider Instance
        {
            get { return _instance ?? (_instance = new EmailSenderProvider()); }
        }

        public IEnumerable<CsEmailSender> GetAll()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.FindAll<CsEmailSender>();
            }
        }
    }
}
