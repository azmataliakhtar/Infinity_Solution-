using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class PostcodeProvider : DataAccess
    {
        private static PostcodeProvider _instance;

        static PostcodeProvider()
        {
            _instance = new PostcodeProvider();
        }

        PostcodeProvider()
        {
        }

        //public static PostcodeProvider Instance
        //{
        //    get { return _instance ?? (_instance = new PostcodeProvider()); }
        //}

        public static PostcodeProvider GetInstance(string connectionString)
        {
            ConnectionString = connectionString;
            return _instance ?? (_instance = new PostcodeProvider());
        }

        public IEnumerable<CsPostCodePrice> GetAllPostCode()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.FindAll<CsPostCodePrice>();
            }
        }

        public CsPostCodePrice GetPostCodeById(decimal id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.Get<CsPostCodePrice>(id);
            }
        }

        public CsPostCodePrice SavedPostCodePrice(CsPostCodePrice postCodePrice)
        {
            CsPostCodePrice savedPostCodePrice = null;
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();
                savedPostCodePrice = postCodePrice.ID == 0 ? session.Insert(postCodePrice) : session.Update(postCodePrice);
                tranx.Commit();
            }
            return savedPostCodePrice;
        }

        public bool DeletePostcode(int id)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var itemToDelete = session.Get<CsPostCodePrice>(id);
                if (itemToDelete == null)
                    return false;

                var tranx = session.GetTransaction();
                try
                {
                    session.Delete<CsPostCodePrice>(itemToDelete);
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

        public CsPostCodePrice FindPostcode(string postcode)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsPostCodePrice>(" WHERE [POST_CODE] = @Postcode ");
                query.AddParameter("@Postcode",postcode,DbType.String);
                return query.GetSingleResult<CsPostCodePrice>();
            }
        }
    }
}
