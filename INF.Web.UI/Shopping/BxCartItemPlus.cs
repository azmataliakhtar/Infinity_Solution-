//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Web;
//using INF.Web.Data.BLL;

//namespace INF.Web.UI.Shopping
//{
//    public class BxCartItemPlus
//    {
//        public string ID { get; private set; }

//        public bool IsDeal { get; set; }

//        public int DealID { get; set; }

//        public string Name { get; set; }

//        public decimal UnitPrice { get; set; }

//        public int Quantity { get; set; }

//        public bool IsDeliveryOrder { get; set; }

//        public BxCartItemPlus()
//        {
//        }

//        private List<SubCartItem> _items;

//        public List<SubCartItem> Items
//        {
//            get { return _items ?? (_items = new List<SubCartItem>()); }
//            private set { _items = value; }
//        }

//        public void ReadDetails()
//        {
//            ReadDeailsOfTheDeal();
//        }

//        public void AddItemToDeal(int menuItemId,int subMenuItemId)
//        {
//            // Load sub menuitem given by the two IDs
//            if (menuItemId > 0 && subMenuItemId > 0)
//            {
//                var bzdMenu = new MenuBusinessLogic(GetDbConnectionString());
//                var menuItem = bzdMenu.GetMenuItem(menuItemId);
//                if (menuItem != null)
//                {
//                    var subItem = new SubCartItem()
//                    {
//                        ID = (int)menuItem.ID,
//                        Name = menuItem.Name,
//                        UnitPrice = IsDeliveryOrder?menuItem.DeliveryPrice:menuItem.CollectionPrice
//                    };
//                }
//            }
//        }

//        private void ReadDeailsOfTheDeal()
//        {
//            if (!IsDeal)
//            {
//                ID = "";
//                DealID = 0;
//                Name = "";
//                UnitPrice = 0;
//                return;
//            }

//            var bzdMenu = new MenuBusinessLogic(GetDbConnectionString());
//            var dealDetail = bzdMenu.GetDealDetailById(DealID,false);
//            if (dealDetail != null)
//            {
//                DealID = dealDetail.ID;
//                Name = dealDetail.Name;
//                UnitPrice = IsDeliveryOrder ? dealDetail.DeliveryUnitPrice : dealDetail.CollectionUnitPrice;
//            }
//        }

//        private string GetDbConnectionString()
//        {
//            string connectionStr = ConfigurationManager.ConnectionStrings["PizzaWebConnectionString"].ConnectionString;
//            return connectionStr;
//        }
            
//    }
//}