using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
  public  class MenuDressingProvider:DataAccess
    {
      private static MenuDressingProvider _Instance;

        MenuDressingProvider()
        {
        }

        static MenuDressingProvider()
        {
            _Instance = new MenuDressingProvider();
        }

        [Obsolete("This funtion is obsolete. Please use the replacement one, GetInstance(string connectionString).")]
        public static MenuDressingProvider Instance
        {
            get { return _Instance ?? (_Instance = new MenuDressingProvider()); }
        }

        public static MenuDressingProvider GetInstance(string connectionString)
        {
            ConnectionString = connectionString;
            return _Instance ?? (_Instance = new MenuDressingProvider());
        }

        public IEnumerable<CsMenuDressing> GetAllDressing()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var dressing = session.FindAll<CsMenuDressing>().OrderBy(t => t.ItemPosition).ThenBy(t => t.Name);
                return dressing;
            }
        }

        public CsMenuDressing GetDressingByID(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsMenuDressing>(" WHERE [ID] = @ID");
                query.AddParameter("@ID", id, DbType.Int32);
                return query.GetSingleResult<CsMenuDressing>();
            }
        }
        
        public CsMenuDressing SaveDressing(CsMenuDressing dressing)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                try
                {
                    var cat = dressing.ID == 0 ? session.Insert(dressing) : session.Update(dressing);
                    tranx.Commit();
                    return cat;
                }
                catch (Exception)
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }

        public bool DeleteDressing(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var catToDel = session.Get<CsMenuDressing>(id);
                if (catToDel == null)
                    return false;

                var tranx = session.GetTransaction();
                try
                {
                    session.Delete(catToDel);
                    tranx.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tranx.Rollback();
                    throw;
                }
            }
        }
    }
}
