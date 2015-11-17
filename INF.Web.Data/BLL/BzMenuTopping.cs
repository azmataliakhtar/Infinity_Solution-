using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using INF.Web.Data.DAL.SqlClient;
using INF.Web.Data.Entities;

namespace INF.Web.Data.BLL
{
    public class BzMenuTopping : BaseBusinessLogic
    {
        private readonly string _connectionString;

        public BzMenuTopping(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString) || connectionString.Trim().Length==0)
                throw new ArgumentNullException("connectionString","Please check the ConnectrionString value, it seem to be empty or not set up yet.");

            _connectionString = connectionString;
        }

        public MenuToppingProvider DalProvider
        {
            get { return MenuToppingProvider.GetInstance(this._connectionString); }
        }

        public IEnumerable<CsToppingCategory> GetToppingCategories()
        {
            return this.DalProvider.GetToppingCategories();
        }

        public CsToppingCategory GetToppingCategory(int id)
        {
            return this.DalProvider.GetToppingCategory(id);
        }

        public IEnumerable<CsMenuTopping> GetMenuToppings(int categoryId)
        {
            return this.DalProvider.GetMenuToppings(categoryId);
        }

        public IEnumerable<CsMenuTopping> GetAllMenuToppings()
        {
            return this.DalProvider.GetAllMenuToppings();
        }

        public CsMenuTopping GetMenuTopping(int id)
        {
            return this.DalProvider.GetMenuTopping(id);
        }

        public CsToppingCategory SaveToppingCategory(CsToppingCategory toppingCategory)
        {
            return this.DalProvider.SaveToppingCategory(toppingCategory);
        }

        public bool DeleteToppingCategory(int id)
        {
            return this.DalProvider.DeleteToppingCategory(id);
        }

        public CsMenuTopping SaveMenuTopping(CsMenuTopping menuTopping)
        {
            return this.DalProvider.SaveMenuTopping(menuTopping);
        }

        public bool DeleteMenuTopping(int id)
        {
            return this.DalProvider.DeleteMenuTopping(id);
        }
    }
}
