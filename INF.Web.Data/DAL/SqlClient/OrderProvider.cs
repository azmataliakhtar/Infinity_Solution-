using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using INF.Web.Data.Entities;

namespace INF.Web.Data.DAL.SqlClient
{
    public class OrderProvider : DataAccess
    {
        private static OrderProvider _instance;

        OrderProvider()
        {
        }

        static OrderProvider()
        {
            _instance = new OrderProvider();
        }

        public static OrderProvider Instance
        {
            get { return _instance ?? (_instance = new OrderProvider()); }
        }

        public static OrderProvider GetInstance(string connectionString)
        {
            ConnectionString = connectionString;
            return _instance ?? (_instance = new OrderProvider());
        }

        public Order SaveOrder(Order order)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var tranx = session.GetTransaction();

                try
                {
                    var savedOrder = order.ID == 0 ? session.Insert<CsOrder>(order) : session.Update<CsOrder>(order);
                    if (savedOrder == null)
                    {
                        tranx.Rollback();
                        return null;
                    }
                    order.ID = savedOrder.ID;
                    for (int index = 0; index < order.OrderDetails.Count; index++)
                    {
                        var detail = order.OrderDetails[index];
                        detail.OrderID = savedOrder.ID;
                        CsOrderDetail savedOrderDetail = detail.ID == 0
                                                             ? session.Insert(detail)
                                                             : session.Update(detail);
                        if (savedOrderDetail == null)
                        {
                            tranx.Rollback();
                            return null;
                        }

                        order.OrderDetails[index] = savedOrderDetail;
                    }
                    tranx.Commit();
                }
                catch
                {
                    tranx.Rollback();
                    throw;
                }
            }
            return order;
        }

        public List<Order> GetOrdersByCustomer(int customerId)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                //var queryOrders = session.CreateQuery<CsOrder>(" WHERE [OrderType]<>'DELIVERED' " +
                //                                                    "AND [OrderType]<>'WAITING_PAYMENT' " +
                //                                                    "AND DATEDIFF(DAY,[OrderDate],GETDATE())<5 " +
                //                                                    "AND [CustomerID]= " + customerId + " " +
                //                                                " ORDER BY [OrderDate] DESC" + " "
                //                                               );

                var queryOrders = session.CreateQuery<CsOrder>(" WHERE [CustomerID]= " + customerId + " ORDER BY [OrderDate] DESC");

                var ordersInWeek = queryOrders.GetResults<CsOrder>();
                var fullOrderList = new List<Order>();
                foreach (var order in ordersInWeek)
                {
                    var fullOrder = new Order
                    {
                        ID = order.ID,
                        AddressId = order.AddressId,
                        AmountDue = order.AmountDue,
                        AmountReceived = order.AmountReceived,
                        AnyReason = order.AnyReason,
                        CustomerID = order.CustomerID,
                        DeliveryCharges = order.DeliveryCharges,
                        Discount = order.Discount,
                        DiscountType = order.DiscountType,
                        IsEdited = order.IsEdited,
                        ShopPostCode = order.ShopPostCode,
                        SpecialInstructions = order.SpecialInstructions,
                        PayStatus = order.PayStatus,
                        PaymentCharges = order.PaymentCharges,
                        PaymentType = order.PaymentType,
                        VoucherCode = order.VoucherCode,
                        TotalAmount = order.TotalAmount,
                        ProcessingTime = order.ProcessingTime,
                        OrderType = order.OrderType,
                        OrderStatus = order.OrderStatus,
                        OrderDate = order.OrderDate,
                        ExpectedTime = order.ExpectedTime
                    };
                    var query = session.CreateQuery<CsOrderDetail>(" WHERE [OrderID] = " + order.ID + " ORDER BY [MenuItemName]");
                    var results = query.GetResults<CsOrderDetail>();
                    if (results != null)
                        fullOrder.OrderDetails = results.ToList();

                    fullOrderList.Add(fullOrder);
                }

                return fullOrderList;
            }
        }

        public Order GetLastOrderByCustomer(int customerId)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                //var queryOrders = session.CreateQuery<CsOrder>(" WHERE [OrderType]<>'DELIVERED' " +
                //                                                    "AND [OrderType]<>'WAITING_PAYMENT' " +
                //                                                    "AND DATEDIFF(DAY,[OrderDate],GETDATE())<5 " +
                //                                                    "AND [CustomerID]= " + customerId + " " +
                //                                                " ORDER BY [OrderDate] DESC" + " "
                //                                               );

                var queryOrders = session.CreateQuery<CsOrder>(" WHERE [CustomerID]= " + customerId + " ORDER BY [OrderDate] DESC LIMIT 1");

                var ordersInWeek = queryOrders.GetResults<CsOrder>();
                var fullOrderListNew = new Order();
                foreach (var order in ordersInWeek)
                {
                    var fullOrder = new Order
                    {
                        ID = order.ID,
                        AddressId = order.AddressId,
                        AmountDue = order.AmountDue,
                        AmountReceived = order.AmountReceived,
                        AnyReason = order.AnyReason,
                        CustomerID = order.CustomerID,
                        DeliveryCharges = order.DeliveryCharges,
                        Discount = order.Discount,
                        DiscountType = order.DiscountType,
                        IsEdited = order.IsEdited,
                        ShopPostCode = order.ShopPostCode,
                        SpecialInstructions = order.SpecialInstructions,
                        PayStatus = order.PayStatus,
                        PaymentCharges = order.PaymentCharges,
                        PaymentType = order.PaymentType,
                        VoucherCode = order.VoucherCode,
                        TotalAmount = order.TotalAmount,
                        ProcessingTime = order.ProcessingTime,
                        OrderType = order.OrderType,
                        OrderStatus = order.OrderStatus,
                        OrderDate = order.OrderDate,
                        ExpectedTime = order.ExpectedTime
                    };
                    var query = session.CreateQuery<CsOrderDetail>(" WHERE [OrderID] = " + order.ID + " ORDER BY [MenuItemName]");
                    var results = query.GetResults<CsOrderDetail>();
                    if (results != null)
                        fullOrder.OrderDetails = results.ToList();

                    fullOrderListNew = fullOrder;
                }

                return fullOrderListNew;
            }
        }

        public List<Order> GetOrdersByCustomer(int customerId,int days)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var queryOrders = session.CreateQuery<CsOrder>(" WHERE DATEDIFF(DAY,[OrderDate],GETDATE())< " + days + " " +
                                                                    "AND [CustomerID]= " + customerId + " " +
                                                                " ORDER BY [OrderDate] DESC" + " "
                                                               );

                var ordersInWeek = queryOrders.GetResults<CsOrder>();
                var fullOrderList = new List<Order>();
                foreach (var order in ordersInWeek)
                {
                    var fullOrder = new Order
                    {
                        ID = order.ID,
                        AddressId = order.AddressId,
                        AmountDue = order.AmountDue,
                        AmountReceived = order.AmountReceived,
                        AnyReason = order.AnyReason,
                        CustomerID = order.CustomerID,
                        DeliveryCharges = order.DeliveryCharges,
                        Discount = order.Discount,
                        DiscountType = order.DiscountType,
                        IsEdited = order.IsEdited,
                        ShopPostCode = order.ShopPostCode,
                        SpecialInstructions = order.SpecialInstructions,
                        PayStatus = order.PayStatus,
                        PaymentCharges = order.PaymentCharges,
                        PaymentType = order.PaymentType,
                        VoucherCode = order.VoucherCode,
                        TotalAmount = order.TotalAmount,
                        ProcessingTime = order.ProcessingTime,
                        OrderType = order.OrderType,
                        OrderStatus = order.OrderStatus,
                        OrderDate = order.OrderDate,
                        ExpectedTime = order.ExpectedTime
                    };
                    var query = session.CreateQuery<CsOrderDetail>(" WHERE [OrderID] = " + order.ID + " ORDER BY [MenuItemName]");
                    var results = query.GetResults<CsOrderDetail>();
                    if (results != null)
                        fullOrder.OrderDetails = results.ToList();

                    fullOrderList.Add(fullOrder);
                }

                return fullOrderList;
            }
        }

        public List<Order> GetOrdersInWeek(int customerId)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var queryOrders = session.CreateQuery<CsOrder>(" WHERE [OrderType]<>'DELIVERED' " +
                                                                    "AND [OrderType]<>'WAITING_PAYMENT' " +
                                                                    "AND DATEDIFF(DAY,[OrderDate],GETDATE())<5 " +
                                                                    "AND [CustomerID]= " + customerId + " " +
                                                                " ORDER BY [OrderDate] DESC" + " "
                                                               );

                var ordersInWeek = queryOrders.GetResults<CsOrder>();
                var fullOrderList = new List<Order>();
                foreach (var order in ordersInWeek)
                {
                    var fullOrder = new Order
                    {
                        ID = order.ID,
                        AddressId = order.AddressId,
                        AmountDue = order.AmountDue,
                        AmountReceived = order.AmountReceived,
                        AnyReason = order.AnyReason,
                        CustomerID = order.CustomerID,
                        DeliveryCharges = order.DeliveryCharges,
                        Discount = order.Discount,
                        DiscountType = order.DiscountType,
                        IsEdited = order.IsEdited,
                        ShopPostCode = order.ShopPostCode,
                        SpecialInstructions = order.SpecialInstructions,
                        PayStatus = order.PayStatus,
                        PaymentCharges = order.PaymentCharges,
                        PaymentType = order.PaymentType,
                        VoucherCode = order.VoucherCode,
                        TotalAmount = order.TotalAmount,
                        ProcessingTime = order.ProcessingTime,
                        OrderType = order.OrderType,
                        OrderStatus = order.OrderStatus,
                        OrderDate = order.OrderDate,
                        ExpectedTime = order.ExpectedTime
                    };
                    var query = session.CreateQuery<CsOrderDetail>(" WHERE [OrderID] = " + order.ID + " ORDER BY [MenuItemName]");
                    var results = query.GetResults<CsOrderDetail>();
                    if (results != null)
                        fullOrder.OrderDetails = results.ToList();

                    fullOrderList.Add(fullOrder);
                }

                return fullOrderList;
            }
        }

        public List<Order> GetOrdersRecentlyDays(int days)
        {
            return GetOrdersRecentlyDays(days, false);
        }

        public List<Order> GetOrdersRecentlyDays(int days, bool inclCustomerInfo)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var queryOrders = session.CreateQuery<CsOrder>(" WHERE DATEDIFF(DAY,[OrderDate],GETDATE())<" + days + " " +
                                                                " ORDER BY [OrderDate] DESC" + " "
                                                               );

                var ordersInWeek = queryOrders.GetResults<CsOrder>();
                var fullOrderList = new List<Order>();
                foreach (var order in ordersInWeek)
                {
                    var fullOrder = new Order
                    {
                        ID = order.ID,
                        AddressId = order.AddressId,
                        AmountDue = order.AmountDue,
                        AmountReceived = order.AmountReceived,
                        AnyReason = order.AnyReason,
                        CustomerID = order.CustomerID,
                        DeliveryCharges = order.DeliveryCharges,
                        Discount = order.Discount,
                        DiscountType = order.DiscountType,
                        IsEdited = order.IsEdited,
                        ShopPostCode = order.ShopPostCode,
                        SpecialInstructions = order.SpecialInstructions,
                        PayStatus = order.PayStatus,
                        PaymentCharges = order.PaymentCharges,
                        PaymentType = order.PaymentType,
                        VoucherCode = order.VoucherCode,
                        TotalAmount = order.TotalAmount,
                        ProcessingTime = order.ProcessingTime,
                        OrderType = order.OrderType,
                        OrderStatus = order.OrderStatus,
                        OrderDate = order.OrderDate,
                        ExpectedTime = order.ExpectedTime
                    };
                    var query = session.CreateQuery<CsOrderDetail>(" WHERE [OrderID] = " + order.ID + " ORDER BY [MenuItemName]");
                    var results = query.GetResults<CsOrderDetail>();
                    if (results != null)
                        fullOrder.OrderDetails = results.ToList();

                    if (inclCustomerInfo)
                    {
                        query = session.CreateQuery<CsCustomer>(" WHERE [Customer_Id] = @CustomerId");
                        query.AddParameter("@CustomerId",order.CustomerID,DbType.Int32);
                        var customer = query.GetSingleResult<CsCustomer>();
                        fullOrder.Customer = customer;


                        query = session.CreateQuery<CsCustomerAddress>(" WHERE [Address_Id] = @AddressId");
                        query.AddParameter("@AddressId", order.AddressId, DbType.Int32);
                        var customerAddress = query.GetSingleResult<CsCustomerAddress>();
                        fullOrder.CustomerAddress = customerAddress;
                    }

                    fullOrderList.Add(fullOrder);
                }

                return fullOrderList;
            }
        }

        public List<Order> GetOrdersToDays(DateTime startDate, DateTime endDate)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                //var queryOrders = session.CreateQuery<CsOrder>(" WHERE [OrderDate] > '" + startDate + "' AND [OrderDate] < '" + endDate.AddDays(1) +
                //                                                "' ORDER BY [OrderDate] DESC" + " "
                //                                               );
                string startDateStr = startDate.ToString("yyyy-MM-dd HH:mm:ss");
                string endDateStr= endDate.ToString("yyyy-MM-dd HH:mm:ss");
                var queryOrders = session.CreateQuery<CsOrder>(" WHERE [OrderDate] BETWEEN CONVERT(DateTime,'" + startDateStr + "',120) AND CONVERT(DateTime,'" + endDateStr + "',120) ORDER BY [OrderDate] DESC");

                var orders = queryOrders.GetResults<CsOrder>();
                var fullOrderList = new List<Order>();
                foreach (var order in orders)
                {
                    var fullOrder = new Order
                    {
                        ID = order.ID,
                        AddressId = order.AddressId,
                        AmountDue = order.AmountDue,
                        AmountReceived = order.AmountReceived,
                        AnyReason = order.AnyReason,
                        CustomerID = order.CustomerID,
                        DeliveryCharges = order.DeliveryCharges,
                        Discount = order.Discount,
                        DiscountType = order.DiscountType,
                        IsEdited = order.IsEdited,
                        ShopPostCode = order.ShopPostCode,
                        SpecialInstructions = order.SpecialInstructions,
                        PayStatus = order.PayStatus,
                        PaymentCharges = order.PaymentCharges,
                        PaymentType = order.PaymentType,
                        VoucherCode = order.VoucherCode,
                        TotalAmount = order.TotalAmount,
                        ProcessingTime = order.ProcessingTime,
                        OrderType = order.OrderType,
                        OrderStatus = order.OrderStatus,
                        OrderDate = order.OrderDate,
                        ExpectedTime = order.ExpectedTime
                    };

                    var query = session.CreateQuery<CsOrderDetail>(" WHERE [OrderID] = " + order.ID + " ORDER BY [MenuItemName]");
                    var results = query.GetResults<CsOrderDetail>();
                    if (results != null)
                        fullOrder.OrderDetails = results.ToList();

                    query = session.CreateQuery<CsCustomerAddress>("WHERE [Address_Id] = @AddressID");
                    query.AddParameter("@AddressID", order.AddressId, DbType.Int32);
                    var address = query.GetSingleResult<CsCustomerAddress>();
                    if (address != null)
                    {
                        fullOrder.CustomerAddress = address;
                    }

                    fullOrderList.Add(fullOrder);
                }

                return fullOrderList;
            }
        }

        public Order GetOrderByID(decimal orderId)
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var fullOrder = new Order();
                var order = session.Get<CsOrder>(orderId);
                if (order != null)
                {
                    fullOrder = new Order
                                    {
                                        ID = order.ID,
                                        AddressId = order.AddressId,
                                        AmountDue = order.AmountDue,
                                        AmountReceived = order.AmountReceived,
                                        AnyReason = order.AnyReason,
                                        CustomerID = order.CustomerID,
                                        DeliveryCharges = order.DeliveryCharges,
                                        Discount = order.Discount,
                                        DiscountType = order.DiscountType,
                                        IsEdited = order.IsEdited,
                                        ShopPostCode = order.ShopPostCode,
                                        SpecialInstructions = order.SpecialInstructions,
                                        PayStatus = order.PayStatus,
                                        PaymentCharges = order.PaymentCharges,
                                        PaymentType = order.PaymentType,
                                        VoucherCode = order.VoucherCode,
                                        TotalAmount = order.TotalAmount,
                                        ProcessingTime = order.ProcessingTime,
                                        OrderType = order.OrderType,
                                        OrderStatus = order.OrderStatus,
                                        OrderDate = order.OrderDate,
                                        ExpectedTime = order.ExpectedTime
                                    };
                    var query = session.CreateQuery<CsOrderDetail>(" WHERE [OrderID] = " + order.ID + " ORDER BY [MenuItemName]");
                    var results = query.GetResults<CsOrderDetail>();
                    if (results != null)
                        fullOrder.OrderDetails = results.ToList();

                }
                return fullOrder;
            }
        }

        public IEnumerable<CsOrder> GetAllOrders()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                return session.FindAll<CsOrder>();
            }
        }

        public IEnumerable<CsOrder> GetConfirmedOrders()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsOrder>(" WHERE [OrderStatus] = 'CONFIRMED'");
                return query.GetResults<CsOrder>();
                //return session.FindAll<CsOrder>();
            }
        }

        public IEnumerable<CsOrderDetail> GetConfirmedOrderDetails()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsOrderDetail>(" INNER JOIN [OrderInfo] AS Ord " +
                    " ON [Order_Detail].[OrderId] = Ord.[OrderID] " +
                    " WHERE Ord.[OrderStatus] = 'CONFIRMED'");
                return query.GetResults<CsOrderDetail>();
            }
        }

        public int GetNumberOfNewOrders()
        {
            using (var session = Provider.CreateSessionFactory().CreateSession())
            {
                var query = session.CreateQuery<CsOrder>(" WHERE [OrderStatus] = 'NEW'");
                var newOrders= query.GetResults<CsOrder>();
                return newOrders.Count();
            }
        }
    }

    public class Order : CsOrder
    {
        private List<CsOrderDetail> _orderDetails;

        public List<CsOrderDetail> OrderDetails
        {
            get { return _orderDetails ?? (_orderDetails = new List<CsOrderDetail>()); }
            set { _orderDetails = value; }
        }

        public virtual CsCustomer Customer { get; set; }
        public virtual CsCustomerAddress CustomerAddress { get; set; }
    }
}
