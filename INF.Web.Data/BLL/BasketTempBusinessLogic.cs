using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.DAL.SqlClient;
using INF.Web.Data.Entities;

namespace INF.Web.Data.BLL
{
    public class BasketTempBusinessLogic : BaseBusinessLogic
    {
        public IEnumerable<CsBasketTemp> FindCsBasketTemps(bool isIncludeDetails)
        {
            return BasketTempProvider.Instance.FindCsBasketTemps(isIncludeDetails);
        }

        public CsBasketTemp SaveBasketTemp(CsBasketTemp basket)
        {
            return BasketTempProvider.Instance.SaveBasketTemp(basket);
        }

        public CsBasketTemp GetBasketTempByID(int id)
        {
            return BasketTempProvider.Instance.GetBasketTempByID(id);
        }

        public CsBasketTemp GetBasketTempByLoginUser(string loginUser)
        {
            return BasketTempProvider.Instance.GetBasketTempByLoginUser(loginUser);
        }

        public bool DeleteBasketTemp(int id)
        {
            return BasketTempProvider.Instance.DeleteBasketTemp(id);
        }

        public void UpdateSagePayStatus(string loginUser,string status)
        {
            BasketTempProvider.Instance.UpdateSagePayStatus(loginUser, status);
        }

        public void UpdateBasketTemp(CsBasketTemp temp, string status)
        {
            BasketTempProvider.Instance.UpdateBasketTemp(temp, status);
        }

        public void UpdateBasketTemp(CsBasketTemp temp)
        {
            BasketTempProvider.Instance.UpdateBasketTemp(temp);
        }
    }
}
