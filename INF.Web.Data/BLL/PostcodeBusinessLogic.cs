using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.DAL.SqlClient;
using INF.Web.Data.Entities;

namespace INF.Web.Data.BLL
{
    public class PostcodeBusinessLogic : BaseBusinessLogic
    {
        private readonly string _connectionString;

         public PostcodeBusinessLogic()
        { }

         public PostcodeBusinessLogic(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CsPostCodePrice GetPostcodeById(int id)
        {
            return PostcodeProvider.GetInstance(_connectionString).GetPostCodeById(id);
        }

        public IEnumerable<CsPostCodePrice> GetAllPostcodes()
        {
            return PostcodeProvider.GetInstance(_connectionString).GetAllPostCode();
        }

        public bool SavePostCodePrice(CsPostCodePrice postCodePrice)
        {
            if (postCodePrice.ID == 0)
            {
                // Do not encrypt the password... because of the old data do not...
                //customer.Password = CryptoUtility.EncryptText(customer.Password);
            }

            var savedPostCode = PostcodeProvider.GetInstance(_connectionString).SavedPostCodePrice(postCodePrice);

            return savedPostCode != null;
        }

        public bool DeletePostcode(int id)
        {
            return PostcodeProvider.GetInstance(_connectionString).DeletePostcode(id);
        }

        public CsPostCodePrice FindPostcode(string postcode)
        {
            return PostcodeProvider.GetInstance(_connectionString).FindPostcode(postcode);
        }
    }
}
