using System;
using System.Text.RegularExpressions;
using INF.Web.Data.DAL.SqlClient;
using INF.Web.Data.Entities;

namespace INF.Web.Data.BLL
{
    public class RestaurantBusinessLogic : BaseBusinessLogic
    {
        public CsRestaurant GetRestaurantInfo()
        {
            return RestaurantInfoProvider.CurrentInstance.GetRestaurantInfo();
        }

        public CsRestaurant GetRestaurantByID(int restaurantID)
        {
            //string key = string.Format("RestaurantInfo_{0}", restaurantID);
            //if (WebSettings.Settings.EnaleCachingMasterData && BaseBusinessLogic.Cache[key] != null)
            //{
            //    return (CsRestaurant)BaseBusinessLogic.Cache[key];
            //}

            var resInfo = RestaurantInfoProvider.CurrentInstance.GetRestaurantInfo(restaurantID);
            //CacheData(key, resInfo);
            return resInfo;
        }

        public CsRestaurant SaveRestaurantInfo(CsRestaurant restaurant)
        {
            var savedRes = RestaurantInfoProvider.CurrentInstance.SaveRestaurantInfo(restaurant);
            string key = string.Format("RestaurantInfo_{0}", restaurant.ID);
            CacheData(key, savedRes);
            return savedRes;
        }

        public CsRestaurantTiming GetRestaurantTiming(int dayInWeek)
        {
            //string key = string.Format("RestaurantTiming_{0}", dayInWeek);
            //if (WebSettings.Settings.EnaleCachingMasterData && Cache[key] != null)
            //{
            //    return (CsRestaurantTiming)Cache[key];
            //}

            var timing = RestaurantTimingProvider.Instance.GetRestaurantTiming(dayInWeek);
            //CacheData(key, timing);
            return timing;
        }

        public double CheckRestaurentTiming(int dayInWeek)
        {
            var restauranInfo = GetRestaurantInfo();
            if (restauranInfo == null)
                return 0;

            // This means the restaurant is closed at the moment!
            if (restauranInfo.WebSiteStatus)
                return 0;

            var timing = GetRestaurantTiming(dayInWeek);
            if (timing == null)
                return 0;

            var dtOpenning = DateTime.MinValue;
            var dtClosing = DateTime.MinValue;

            string openTime = timing.OpeningTime;
            if (!string.IsNullOrEmpty(openTime) && openTime.Length > 5)
            {
                openTime = openTime.Substring(0, 5);
                try { dtOpenning = DateTime.Parse(openTime); }
                catch { return 0; }
            }
            else
                return 0;

            string closingTime = timing.ClosingTime;
            if (!string.IsNullOrEmpty(closingTime) && closingTime.Length > 5)
            {
                closingTime = closingTime.Substring(0, 5);
                try { dtClosing = DateTime.Parse(closingTime); }
                catch { return 0; }
            }
            else
                return 0;

            // The time-range takes over night
            if (dtClosing.CompareTo(dtOpenning) < 0)
            {
                dtClosing = dtClosing.AddDays(1);
            }

            if (dtOpenning.CompareTo(DateTime.Now) > 0)
            {
                // Check with the timings of the previous day
                if (dayInWeek == 0)
                    dayInWeek = 6;
                else
                    dayInWeek = dayInWeek - 1;

                timing = GetRestaurantTiming(dayInWeek);
                if (timing == null)
                    return 0;

                dtOpenning = DateTime.MinValue;
                dtClosing = DateTime.MinValue;

                openTime = timing.OpeningTime;
                if (!string.IsNullOrEmpty(openTime) && openTime.Length > 5)
                {
                    openTime = openTime.Substring(0, 5);
                    try { dtOpenning = DateTime.Parse(openTime).AddDays(-1); }
                    catch { return 0; }
                }
                else
                    return 0;

                closingTime = timing.ClosingTime;
                if (!string.IsNullOrEmpty(closingTime) && closingTime.Length > 5)
                {
                    closingTime = closingTime.Substring(0, 5);
                    try { dtClosing = DateTime.Parse(closingTime).AddDays(-1); }
                    catch { return 0; }
                }
                else
                    return 0;

                // The time-range takes over night
                if (dtClosing.CompareTo(dtOpenning) < 0)
                {
                    dtClosing = dtClosing.AddDays(1);
                }

                if (dtClosing.CompareTo(DateTime.Now) < 0)
                {
                    return 0;
                }
                else
                {
                    return dtClosing.Subtract(DateTime.Now).TotalSeconds;
                }
            }
            else if (dtClosing.CompareTo(DateTime.Now) < 0)
            {
                return 0;
            }
            else
            {
                return dtClosing.Subtract(DateTime.Now).TotalSeconds;
            }
        }

        public CsDeliveryTiming GetDeliveryTiming(int dayInWeek)
        {
            return DeliveryTimingProvider.Instance.GetDeliveryTiming(dayInWeek);
        }

        public double GetDeliveryDiscount(int dayInWeek)
        {
            return GetDeliveryTiming(dayInWeek).DiscountPercent;
        }

        public double GetCollectionDiscount(int dayInWeek)
        {
            return GetRestaurantTiming(dayInWeek).DiscountPercent;
        }

        public double GetSpecialDiscount()
        {
            string enabledStr = FlatFieldsProvider.Instance.GetFlatFieldValue(SpeicalDiscountEnabled);
            bool isEnabled = ("true").Equals(enabledStr, StringComparison.CurrentCultureIgnoreCase);
            if (isEnabled)
            {
                string discountStr = FlatFieldsProvider.Instance.GetFlatFieldValue(SpeicalDiscountPercent);
                //if (!string.IsNullOrEmpty(discountStr) && Regex.IsMatch(discountStr, @"\d"))
                //{
                //    return Convert.ToDouble(discountStr);
                //}

                double discountVal = 0;
                if (double.TryParse(discountStr, out discountVal))
                {
                    return discountVal;
                }
            }
            return 0;
        }

        public double GetSpecialDiscountOrderValue()
        {
            string enabledStr = FlatFieldsProvider.Instance.GetFlatFieldValue(SpeicalDiscountEnabled);
            bool isEnabled = ("true").Equals(enabledStr, StringComparison.CurrentCultureIgnoreCase);
            if (isEnabled)
            {
                string orderValueStr = FlatFieldsProvider.Instance.GetFlatFieldValue(SpeicalDiscountOrderValue);
                //if (!string.IsNullOrEmpty(orderValueStr) && Regex.IsMatch(orderValueStr,@"\d"))
                //{
                //    return Convert.ToDouble(orderValueStr);
                //}

                double discountVal = 0;
                if (double.TryParse(orderValueStr, out discountVal))
                {
                    return discountVal;
                }
            }
            return 0;
        }

        private const string SpeicalDiscountEnabled = "f_special_discount_enabled";
        private const string SpeicalDiscountOrderValue = "f_special_discount_order_value";
        private const string SpeicalDiscountPercent = "f_special_discount_percent";

        private const string DeliverySpeicalDiscountEnabled = "f_delivery_special_discount_enabled";
        private const string DeliverySpeicalDiscountOrderValue = "f_delivery_special_discount_order_value";
        private const string DeliverySpeicalDiscountPercent = "f_delivery_special_discount_percent";

        public double GetDeliverySpecialDiscount()
        {
            string enabledStr = FlatFieldsProvider.Instance.GetFlatFieldValue(DeliverySpeicalDiscountEnabled);
            bool isEnabled = ("true").Equals(enabledStr, StringComparison.CurrentCultureIgnoreCase);
            if (isEnabled)
            {
                string discountStr = FlatFieldsProvider.Instance.GetFlatFieldValue(DeliverySpeicalDiscountPercent);
                //if (!string.IsNullOrEmpty(discountStr) && Regex.IsMatch(discountStr, @"\d"))
                //{
                //    return Convert.ToDouble(discountStr);
                //}

                double discountVal = 0;
                if (double.TryParse(discountStr, out discountVal))
                {
                    return discountVal;
                }
            }
            return 0;
        }

        public double GetDeliverySpecialDiscountOrderValue()
        {
            string enabledStr = FlatFieldsProvider.Instance.GetFlatFieldValue(DeliverySpeicalDiscountEnabled);
            bool isEnabled = ("true").Equals(enabledStr, StringComparison.CurrentCultureIgnoreCase);
            if (isEnabled)
            {
                string orderValueStr = FlatFieldsProvider.Instance.GetFlatFieldValue(DeliverySpeicalDiscountOrderValue);
                //if (!string.IsNullOrEmpty(orderValueStr) && Regex.IsMatch(orderValueStr, @"\d"))
                //{
                //    return Convert.ToDouble(orderValueStr);
                //}
                double discountVal = 0;
                if (double.TryParse(orderValueStr, out discountVal))
                {
                    return discountVal;
                }
            }
            return 0;
        }

        public double GetOnlineDiscount(bool isDeliveryOrder, int dayInWeek)
        {
            double onlineDiscount = 0;

            if (isDeliveryOrder)
            {
                onlineDiscount = GetDeliveryDiscount(dayInWeek);
            }
            else
            {
                onlineDiscount = GetCollectionDiscount(dayInWeek);
            }

            if ((double)GetRestaurantInfo().OnlineDiscount > onlineDiscount)
                onlineDiscount = (double)GetRestaurantInfo().OnlineDiscount;

            return onlineDiscount;
        }
    }
}