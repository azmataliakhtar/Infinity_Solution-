using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using INF.Web.Data.DAL.SqlClient;
using INF.Web.Data.Entities;

namespace INF.Web.Data.BLL
{
    public class ReportingBusinessLogic : BaseBusinessLogic
    {
        public ReportModel[] GetRestaurantInfo()
        {
            var restaurantInfo = new ReportModel();

            var res = RestaurantInfoProvider.CurrentInstance.GetRestaurantInfo();
            if (res != null)
            {
                restaurantInfo.RestaurantName = res.ShopName;
                restaurantInfo.RestaurantAddress = string.Format("{0} - {1} - {2} - {3}",
                                                                 res.BuildingName, res.ShopName, res.City, res.County);
                restaurantInfo.RestaurantAddress = restaurantInfo.RestaurantAddress.Trim().Trim(new char[] { '-' }).Trim();
                restaurantInfo.RestaurantPostCode = res.PostCode;
                restaurantInfo.RestaurantTelephone = string.IsNullOrEmpty(res.Telephone1) ? res.Telephone1 : res.Telephone2;
            }

            return new[] { restaurantInfo };
        }

        public PeriodReportModel[] GetReportPeriod(DateTime startDate, DateTime endDate)
        {
            var period = new PeriodReportModel {FromDate = startDate, ToDate = endDate};
            period.FromDate = period.FromDate.Date.AddHours(-12);
            period.ToDate = period.ToDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            return new[] { period };
        }

        public OrdersMonthlyModel[] GetOrdersMonthly(string reportName,DateTime startDate, DateTime endDate)
        {
            var ordersMonthly = new OrdersMonthlyModel();
            //ordersMonthly.Title = string.Format("Basic {0} Report", reportName);
            ordersMonthly.Title = string.Format("Basic Report");
            ordersMonthly.BillNo = DateTime.Today.ToString("yyMMdd");
            ordersMonthly.ReferenceNo = "INF-" + DateTime.Today.ToString("yyMMdd");
            ordersMonthly.BillDate = DateTime.Today.ToString("MM-dd-yyyy");

            var res = RestaurantInfoProvider.CurrentInstance.GetRestaurantInfo();
            if (res != null)
            {
                ordersMonthly.RestaurantName = res.ShopName;
                ordersMonthly.RestaurantAddress = string.Format("{0} - {1} - {2} - {3}",
                                                                 res.BuildingName, res.ShopName, res.City, res.County);
                ordersMonthly.RestaurantAddress = ordersMonthly.RestaurantAddress.Trim().Trim(new char[] { '-' }).Trim();
                ordersMonthly.RestaurantPostCode = res.PostCode;
                ordersMonthly.RestaurantTelephone = string.IsNullOrEmpty(res.Telephone1) ? res.Telephone1 : res.Telephone2;
            }

            var period = GetReportPeriod(startDate, endDate);
            var allOrdersInMonth = GetOrdersInCurrentMonth(period[0]);

            double amtOrdersPaidByCard = 0;
            int qtyOrdersPaidByCard = 0;

            double amtOrdersPaidByCash = 0;
            int qtyOrdersPaidByCash = 0;

            foreach (var order in allOrdersInMonth)
            {
                if (order.PaymentType.Equals("CASH", StringComparison.CurrentCultureIgnoreCase))
                {
                    qtyOrdersPaidByCash = qtyOrdersPaidByCash + 1;
                    amtOrdersPaidByCash = amtOrdersPaidByCash + order.TotalAmount;
                }
                else if (order.PaymentType.Equals("CARD", StringComparison.CurrentCultureIgnoreCase))
                {
                    qtyOrdersPaidByCard = qtyOrdersPaidByCard + 1;
                    amtOrdersPaidByCard = amtOrdersPaidByCard + order.TotalAmount;
                }
                else
                    continue;
            }

            ordersMonthly.OrdersPaidByCardAmount = amtOrdersPaidByCard;
            ordersMonthly.OrdersPaidByCardQuantity = qtyOrdersPaidByCard;

            ordersMonthly.OrdersPaidByCashQuantity = qtyOrdersPaidByCash;
            ordersMonthly.OrdersPaidByCashAmount = amtOrdersPaidByCash;

            return new OrdersMonthlyModel[] { ordersMonthly };
        }

        public ChargeModel[] GetAvailableCharges()
        {
            var servicesCharges = ServicesChargeProvider.Instance.GetEnabledServicesCharge();
            var chargesModel = servicesCharges.Select(sc => new ChargeModel { ChargeName = sc.Name, ChargeAmount = sc.Charge, ChargeOnOrder = sc.ChargeOnOrder}).ToList();
            return chargesModel.ToArray();
        }

        public IEnumerable<CsServicesCharge> GetAllServicesCharge()
        {
            return ServicesChargeProvider.Instance.GetAllServicesCharge();
        }

        public CsServicesCharge GetServiceCharge(int id)
        {
            return ServicesChargeProvider.Instance.GetServicesCharge(id);
        }

        public CsServicesCharge SaveServiceCharge(CsServicesCharge sc)
        {
            return ServicesChargeProvider.Instance.SaveServicesCharge(sc);
        }

        public GenericItemReportModel[] GetBestItemSold()
        {
            var confirmedOrderDetails = OrderProvider.Instance.GetConfirmedOrderDetails();
            var csOrderDetails = confirmedOrderDetails as List<CsOrderDetail> ?? confirmedOrderDetails.ToList();

            var orderDetailsDic = csOrderDetails.ToDictionary(d => d.ID);

            var groupOrderDetail = csOrderDetails.GroupBy(p => p.ID).Select(
                    g => new { OrderDetailID = g.Key, Quantity = g.Sum(k => k.Quantity), Amount = g.Sum(k => k.Price) });

            groupOrderDetail = groupOrderDetail.OrderBy(g => g.Quantity).Reverse();

            var items = new List<GenericItemReportModel>();
            var enumerator = groupOrderDetail.GetEnumerator();
            var counter = 0;
            while (counter < 5 && enumerator.MoveNext())
            {
                var item = new GenericItemReportModel
                {
                    Name = orderDetailsDic[enumerator.Current.OrderDetailID].MenuItemName,
                    Quantity = enumerator.Current.Quantity,
                    Amount = (double)enumerator.Current.Amount
                };
                items.Add(item);

                counter = counter + 1;
            }

            return items.ToArray();
        }

        public GenericItemReportModel[] GetCustomersReport(DateTime startDate, DateTime endDate)
        {
            var period = GetReportPeriod(startDate, endDate);
            return GetCustomersReportInMonth(period[0]);
        }

        public GenericItemReportModel[] GetOrdersInStatuses(DateTime startDate, DateTime endDate)
        {
            var period = GetReportPeriod(startDate, endDate);
            var allOrders = OrderProvider.Instance.GetAllOrders();
            var allOrdersInMonth = allOrders
                .Where(o => o.OrderDate >= period[0].FromDate && o.OrderDate <= period[0].ToDate)
                .ToList();

            var deniedOrders = new GenericItemReportModel()
            {
                Name = "Denied Orders",
                Quantity = allOrdersInMonth
                     .Count(o => o.OrderStatus.Equals("Denied", StringComparison.CurrentCultureIgnoreCase)),
                Amount = allOrdersInMonth
                     .Where(o => o.OrderStatus.Equals("Denied", StringComparison.CurrentCultureIgnoreCase))
                     .Sum(o => o.TotalAmount)
            };

            var confirmedOrders = new GenericItemReportModel()
            {
                Name = "Confirmed Orders",
                Quantity = allOrdersInMonth
                    .Count(o => o.OrderStatus.Equals("Confirmed", StringComparison.CurrentCultureIgnoreCase)),
                Amount = allOrdersInMonth
                    .Where(o => o.OrderStatus.Equals("Confirmed", StringComparison.CurrentCultureIgnoreCase))
                    .Sum(o => o.TotalAmount)
            };

            return new GenericItemReportModel[] { deniedOrders, confirmedOrders };
        }


        public SalesComparisonModel[] GetSalesComparison(DateTime startDate, DateTime endDate)
        {
            var currentPeriod = GetReportPeriod(startDate, endDate)[0];
            var lastPeriod = new PeriodReportModel()
            {
                FromDate = currentPeriod.FromDate.AddMonths(-1),
                ToDate = currentPeriod.ToDate.AddMonths(-1)
            };

            var lastTwoPeriod = new PeriodReportModel()
            {
                FromDate = currentPeriod.FromDate.AddMonths(-2),
                ToDate = currentPeriod.ToDate.AddMonths(-2)
            };


            var currentCustomersReport = GetCustomersReportInMonth(currentPeriod);
            var lastMonthCustomersReport = GetCustomersReportInMonth(lastPeriod);
            var lastTwoMonthCustomersReport = GetCustomersReportInMonth(lastTwoPeriod);

            var newCustomers = new SalesComparisonModel()
            {
                Name = "New Customers Ordered - Quantity",
                ThisMonth = currentCustomersReport[0].Quantity.ToString(),
                LastMonth = lastMonthCustomersReport[0].Quantity.ToString(),
                LastTwoMonths = lastTwoMonthCustomersReport[0].Quantity.ToString()
            };

            var newCustomersAmount = new SalesComparisonModel()
            {
                Name = "New Customers Ordered - Amount",
                ThisMonth = currentCustomersReport[0].Amount.ToString(),
                LastMonth = lastMonthCustomersReport[0].Amount.ToString(),
                LastTwoMonths = lastTwoMonthCustomersReport[0].Amount.ToString()
            };

            var existingCustomers = new SalesComparisonModel()
            {
                Name = "Existing Customers Ordered - Quantity",
                ThisMonth = currentCustomersReport[1].Quantity.ToString(),
                LastMonth = lastMonthCustomersReport[1].Quantity.ToString(),
                LastTwoMonths = lastTwoMonthCustomersReport[1].Quantity.ToString()
            };

            var existingCustomersAmount = new SalesComparisonModel()
            {
                Name = "Existing Customers Ordered - Amount",
                ThisMonth = currentCustomersReport[1].Amount.ToString(),
                LastMonth = lastMonthCustomersReport[1].Amount.ToString(),
                LastTwoMonths = lastTwoMonthCustomersReport[1].Amount.ToString()
            };

            //var deniedOrders = new SalesComparisonModel()
            //{
            //    Name = "Denied Orders",
            //    ThisMonth = "13",
            //    LastMonth = "12",
            //    LastTwoMonths = "11"
            //};

            //var confirmedOrders = new SalesComparisonModel()
            //{
            //    Name = "Confirmed Orders",
            //    ThisMonth = "33",
            //    LastMonth = "22",
            //    LastTwoMonths = "11"
            //};

            return new SalesComparisonModel[]
                       {
                           newCustomers, newCustomersAmount, existingCustomers, existingCustomersAmount/*, deniedOrders,
                           confirmedOrders*/
                       };
        }

        public void SaveMonthlyReportStartDate(string dateStr)
        {
            FlatFieldsProvider.Instance.SaveFlatField("monthly_report_start_date", dateStr);
        }

        public void SaveMonthlyReportMonth(string dateStr)
        {
            FlatFieldsProvider.Instance.SaveFlatField("monthly_report_month", dateStr);
        }

        public void SaveMonthlyReportYear(string dateStr)
        {
            FlatFieldsProvider.Instance.SaveFlatField("monthly_report_year", dateStr);
        }

        public string GetMonthlyReportStartDate()
        {
            return FlatFieldsProvider.Instance.GetFlatFieldValue("monthly_report_start_date");
        }

        public string GetMonthlyReportMonth()
        {
            return FlatFieldsProvider.Instance.GetFlatFieldValue("monthly_report_month");
        }

        public string GetMonthlyReportYear()
        {
            return FlatFieldsProvider.Instance.GetFlatFieldValue("monthly_report_year");
        }

        private IEnumerable<CsOrder> GetOrdersInCurrentMonth(PeriodReportModel period)
        {
            string key = string.Format("AllConfirmedOrdersInMonth_{0}_{1}", period.FromDate, period.ToDate);
            //if (WebSettings.Settings.EnableCaching && Cache[key] != null)
            //{
            //    return (IEnumerable<CsOrder>)Cache[key];
            //}
            var allConfirmedOrdersInMonth = OrderProvider.Instance.GetConfirmedOrders()
                .Where(o => o.OrderDate >= period.FromDate && o.OrderDate <= period.ToDate);
            CacheData(key, allConfirmedOrdersInMonth);
            return allConfirmedOrdersInMonth;
        }

        private GenericItemReportModel[] GetCustomersReportInMonth(PeriodReportModel period)
        {
            double newCustomerAmt = 0;
            double existingCustomerAmt = 0;

            var customerBll = new CustomerBusinessLogic();
            var allCustomers = customerBll.GetAllCustomers();
            var allCustomers1 = customerBll.GetAllCustomers();

            var newCustomers = allCustomers
                .Where(c => (c.MemberSince >= period.FromDate) && (c.MemberSince <= period.ToDate))
                .ToDictionary(nc => nc.ID);

            var existingCustomers = allCustomers1
                .Where(c => (c.MemberSince < period.FromDate))
                .ToDictionary(nc => nc.ID);

            var allOrdersInMonth = GetOrdersInCurrentMonth(period);
            var newCusQantity = 0;
            var existCusQuantity = 0;

            foreach (var order in allOrdersInMonth)
            {
                if (newCustomers.ContainsKey(order.CustomerID))
                {
                    newCusQantity = newCusQantity + 1;
                    newCustomerAmt = newCustomerAmt + order.TotalAmount;
                    continue;
                }

                if (existingCustomers.ContainsKey(order.CustomerID))
                {
                    existCusQuantity = existCusQuantity + 1;
                    existingCustomerAmt = existingCustomerAmt + order.TotalAmount;
                }
            }

            var item1 = new GenericItemReportModel
            {
                Name = "New Customers Ordered",
                Quantity = newCusQantity, //newCustomers.Count(),
                Amount = newCustomerAmt
            };
            var item2 = new GenericItemReportModel
            {
                Name = "Existing Customers Ordered",
                Quantity = existCusQuantity,//existingCustomers.Count(),
                Amount = existingCustomerAmt
            };

            return new[] { item1, item2 };
        }
    }

    public class OrdersMonthlyModel : ReportModel
    {
        public string BillNo { get; set; }
        public string ReferenceNo { get; set; }
        public string BillDate { get; set; }

        public int OrdersPaidByCashQuantity { get; set; }
        public double OrdersPaidByCashAmount { get; set; }
        public int OrdersPaidByCardQuantity { get; set; }
        public double OrdersPaidByCardAmount { get; set; }

        public int TotalOrders { get; set; }
        public double TotalAmount { get; set; }
    }

    public class ChargeModel
    {
        public string ChargeName { get; set; }
        public double ChargeAmount { get; set; }
        public bool ChargeOnOrder { get; set; }
    }

    public class ReportModel
    {
        public string Title { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantPostCode { get; set; }
        public string RestaurantTelephone { get; set; }
    }

    public class GenericItemReportModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }

    public class PeriodReportModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class SalesComparisonModel
    {
        public string Name { get; set; }
        public string ThisMonth { get; set; }
        public string LastMonth { get; set; }
        public string LastTwoMonths { get; set; }
    }
}
