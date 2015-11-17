using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.DAL.SqlClient;
using INF.Web.Data.Entities;

namespace INF.Web.Data.BLL
{
    public class ShoppingBusinessLogic : BaseBusinessLogic
    {
        private readonly string _connectionString;

        public ShoppingBusinessLogic()
        {
        }

        public ShoppingBusinessLogic(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Order SaveCustomerOrder(Order order)
        {
            return OrderProvider.GetInstance(_connectionString).SaveOrder(order);
        }

        public Order GetOrderByID(decimal id)
        {
            return OrderProvider.GetInstance(_connectionString).GetOrderByID(id);
        }

        public List<Order> GetOrdersInWeek(int customerId)
        {
            return OrderProvider.GetInstance(_connectionString).GetOrdersInWeek(customerId);
        }

        public List<Order> GetOrdersByCustomer(int customerId)
        {
            return OrderProvider.GetInstance(_connectionString).GetOrdersByCustomer(customerId);
        }

        public List<Order> GetOrdersByCustomer(int customerId, int days)
        {
            return OrderProvider.GetInstance(_connectionString).GetOrdersByCustomer(customerId, days);
        }

        public Order GetLastOrderByCustomer(int customerId)
        {
            return OrderProvider.GetInstance(_connectionString).GetLastOrderByCustomer(customerId);
        }
        public List<Order> GetOrdersRecentlyDays(int days)
        {
            //return OrderProvider.Instance.GetOrdersRecentlyDays(days);
            return OrderProvider.GetInstance(_connectionString).GetOrdersRecentlyDays(days);
        }

        public List<Order> GetOrdersRecentlyDays(int days, bool inclCustomerInfo)
        {
         
            return OrderProvider.GetInstance(_connectionString).GetOrdersRecentlyDays(days,inclCustomerInfo);
        }

        public int GetNumberOfNewOrders()
        {
            return OrderProvider.GetInstance(_connectionString).GetNumberOfNewOrders();
        }
    }
}
