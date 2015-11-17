using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class UserProvider : DataAccess
    {
        private static UserProvider _instance;

        UserProvider()
        {
        }

        static UserProvider()
        {
            _instance = new UserProvider();
        }

        public static UserProvider Instance
        {
            get { return _instance ?? (_instance = new UserProvider()); }
        }

        public CsUser SaveUser(CsUser user)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    var savedUser = user.ID == 0 ? session.Insert<CsUser>(user) : session.Update<CsUser>(user);
                    tranx.Commit();
                    return savedUser;
                }
                catch
                {
                    tranx.Rollback();
                    return null;
                }
            }
        }

        public CsUser GetUser(string username)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsUser>(" WHERE [UserName] = @username");
                query.AddParameter("@username", username, DbType.String);
                return query.GetSingleResult<CsUser>();
            }
        }

        public CsUser GetUserByEmail(string email)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsUser>(" WHERE [Email] = @email");
                query.AddParameter("@email", email, DbType.String);
                return query.GetSingleResult<CsUser>();
            }
        }

        public CsUser GetUser(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsUser>(" WHERE [ID] = @id");
                query.AddParameter("@id", id, DbType.Int32);
                return query.GetSingleResult<CsUser>();
            }
        }

        public IEnumerable<CsUser> GetAllUsers(bool excludeInActivedOnes)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var allUsers = session.FindAll<CsUser>();
                if (excludeInActivedOnes)
                {
                    return allUsers.Where(u => u.IsActived);
                }
                return allUsers;
            }
        }
    }
}
