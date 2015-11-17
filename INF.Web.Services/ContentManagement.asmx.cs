using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using INF.Web.Data.BLL;
using INF.Web.Data.Entities;

namespace INF.Web.Services
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://services.infinitysol.co.uk/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ContentManagement : WebService
    {
        public const string DEFAULT_CONNECTION_STRING = "ConnectionStringDefault";

        public AuthenticateHeader AuthHeader { get; set; }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [SoapHeader("AuthHeader")]
        public bool Authenticate()
        {
            try
            {
                if (AuthHeader.SiteId == "INFINZABOOK" && AuthHeader.SitePwd == "patookey69")
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        [WebMethod]
        public DataSet GetAllCategories()
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Menu_Category] order by ItemPosition"); // change the query here
            }
            return null;
        }

        [WebMethod]
        public bool UpdateCategory(long categoryId, string name, int position, bool active, string normalImage,
                                   string mouseOverImage)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery("update [Menu_Category] set Name=\'" + name + "\',ItemPosition=" +
                                       position.ToString(CultureInfo.InvariantCulture) + ",[Active]=\'" + active.ToString() + "\',NormalImage=\'" +
                                       normalImage + "\',MouseOverImage=\'" + mouseOverImage + "\'  where Category_Id=" +
                                       categoryId + ""); // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool UpdateWebCategory(long categoryId, string name, int position, bool active, string normalImage, string mouseOverImage, bool exclOnlineDiscount)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery("update [Menu_Category] set Name=\'" + name + "\',ItemPosition=" +
                                       position.ToString(CultureInfo.InvariantCulture) + ",[Active]=\'" + active.ToString() + "\',NormalImage=\'" +
                                       normalImage + "\',MouseOverImage=\'" + mouseOverImage + "\' " + ",ExclOnlineDiscount= \'" + exclOnlineDiscount + "\' " +
                                       " where Category_Id=" + categoryId + "");
            }
            return false;
        }

        [WebMethod]
        public bool UpdateWebCategoryInclDeal(long categoryId, string name, int position, bool active, string normalImage, string mouseOverImage, bool exclOnlineDiscount, bool isDeal, bool isAvailableForDeal)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery("update [Menu_Category] set Name=\'" + name + "\',ItemPosition=" +
                                       position.ToString(CultureInfo.InvariantCulture) + ",[Active]=\'" + active.ToString() + "\',NormalImage=\'" +
                                       normalImage + "\',MouseOverImage=\'" + mouseOverImage + "\' " + ",ExclOnlineDiscount= \'" + exclOnlineDiscount + "\' " +
                                       ",IsDeal= \'" + isDeal + "\' " + ",IsAvailableForDeal= \'" + isAvailableForDeal + "\' " +
                                       " where Category_Id=" + categoryId + "");
            }
            return false;
        }

        [WebMethod]
        public bool UpdateWebCategoryInclDealAndMaxDressing(long categoryId, string name, int position
            , bool active ,string normalImage, string mouseOverImage, bool exclOnlineDiscount, bool isDeal
            , bool isAvailableForDeal, int maxOfDressing)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery("UPDATE [Menu_Category] SET "
                        + " Name=\'" + name + "\'"
                        + ",ItemPosition=" + position.ToString(CultureInfo.InvariantCulture) + ","
                        + "[Active]=\'" + active.ToString() + "\'"
                        + ",NormalImage=\'" + normalImage + "\'"
                        + ",MouseOverImage=\'" + mouseOverImage + "\' "
                        + ",ExclOnlineDiscount= \'" + exclOnlineDiscount + "\' "
                        + ",IsDeal= \'" + isDeal + "\' "
                        + ",IsAvailableForDeal= \'" + isAvailableForDeal + "\' "
                        + ",MaxDressing= " + maxOfDressing + " "
                    + " WHERE Category_Id=" + categoryId + "");
            }
            return false;
        }

        [WebMethod]
        public bool AddCategory(string name, int position, bool active)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery("insert into Menu_Category (Name,ItemPosition,Active) values(\'" + name + "\'," +
                                       position.ToString(CultureInfo.InvariantCulture) + ",\'" + active.ToString() + "\')"); // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool AddWebCategory(string name, int position, bool active, string normalImage, string mouseOverImage, bool exclOnlineDiscount)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery(" insert into [Menu_Category] " +
                        " (Name, ItemPosition, Active, NormalImage, MouseOverImage, ExclOnlineDiscount) " +
                        " values(" +
                        "\'" + name + "\'" +
                        "," + position.ToString(CultureInfo.InvariantCulture) +
                        ",\'" + active.ToString() + "\'" +
                        ",\'" + normalImage + "\'" +
                        ",\'" + mouseOverImage + "\'" +
                        ",\'" + exclOnlineDiscount + "\')");
            }
            return false;
        }

        [WebMethod]
        public bool AddWebCategoryInclDeal(string name, int position, bool active, string normalImage, string mouseOverImage, bool exclOnlineDiscount, bool isDeal, bool isAvailableForDeal)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery(" insert into [Menu_Category] " +
                        " (Name, ItemPosition, Active, NormalImage, MouseOverImage, ExclOnlineDiscount,IsDeal,IsAvailableForDeal) " +
                        " values(" +
                            "\'" + name + "\'" +
                            "," + position.ToString(CultureInfo.InvariantCulture) +
                            ",\'" + active.ToString() + "\'" +
                            ",\'" + normalImage + "\'" +
                            ",\'" + mouseOverImage + "\'" +
                            ",\'" + exclOnlineDiscount + "\'" +
                            ",\'" + isDeal + "\'" +
                            //IsAvailableForDeal
                            ",\'" + isAvailableForDeal + "\'" +
                        ")");
            }
            return false;
        }


        [WebMethod]
        public bool AddWebCategoryInclDealAndMaxDressing(string name, int position, bool active
            , string normalImage, string mouseOverImage, bool exclOnlineDiscount, bool isDeal
            , bool isAvailableForDeal, int maxOfDressing)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery(" insert into [Menu_Category] " +
                        " (Name, ItemPosition, Active, NormalImage, MouseOverImage, ExclOnlineDiscount,IsDeal,IsAvailableForDeal,MaxDressing) " +
                        " values(" +
                            "\'" + name + "\'" +
                            "," + position.ToString(CultureInfo.InvariantCulture) +
                            ",\'" + active.ToString() + "\'" +
                            ",\'" + normalImage + "\'" +
                            ",\'" + mouseOverImage + "\'" +
                            ",\'" + exclOnlineDiscount + "\'" +
                            ",\'" + isDeal + "\'" +
                    //IsAvailableForDeal
                            ",\'" + isAvailableForDeal + "\'" +
                    //MaxDressing
                            "," + maxOfDressing + "" +
                        ")");
            }
            return false;
        }

        [WebMethod]
        public DataSet GetCategoryInfo(long catId)
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Menu_Category] where Category_Id=" + catId + ""); // change the query here
            }
            return null;
        }

        [WebMethod]
        public bool DeleteCategory(long categoryId)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery("delete from [Menu_Category] where Category_Id=" + categoryId + "");
                // change the query here
            }
            return false;
        }

        [WebMethod]
        public DataSet GetAllMenuItems(long categoryId)
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Menu_Item] where Category_Id=" + categoryId + " order by ItemPosition ");
                // change the query here
            }
            return null;
        }

        [WebMethod]
        public DataSet GetMenuItemInfo(long menuId)
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Menu_Item] where Menu_Id=" + menuId + ""); // change the query here
            }
            return null;
        }

        [WebMethod]
        public bool UpdateMenuItem(long menuId, string Name, string PromotText, bool IsActive, double Collection_Price,
                                   double Delivery_Price, int PreparationTime, long CategoryId, bool HasSubMenu,
                                   string Remarks, string MenuImage, bool bHasDressing, bool bHasTopping, bool bHasBase,
                                   int ItemPosition, double Topping_Price, string LargeImage)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery("update [Menu_Item] set Name=\'" + Name + "\',PromotText=\'" + PromotText +
                                       "\',IsActive=\'" + IsActive.ToString() + "\',Collection_Price=" +
                                       Collection_Price.ToString() + ",Delivery_Price=" + Delivery_Price.ToString() +
                                       ",PreparationTime=" + PreparationTime.ToString() + ",Category_Id=" + CategoryId +
                                       ",HasSubMenu=\'" + HasSubMenu.ToString() + "\',Remarks=\'" + Remarks +
                                       "\',MenuImage=\'" + MenuImage + "\',bHasDressing=\'" + bHasDressing.ToString() +
                                       "\',bHasTopping=\'" + bHasTopping.ToString() + "\',bHasBase=\'" + bHasBase.ToString() +
                                       "\',ItemPosition=" + ItemPosition.ToString() + " ,LargeImage=\'" + LargeImage +
                                       "\' where Menu_Id=" + menuId + ""); // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool AddMenuItem(string Name, string PromotText, bool IsActive, double Collection_Price,
                                double Delivery_Price, int PreparationTime, long CategoryId, bool HasSubMenu, string Remarks,
                                string MenuImage, bool bHasDressing, bool bHasTopping, bool bHasBase, int ItemPosition,
                                double Topping_Price, string LargeImage)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery(
                        "insert into Menu_Item (Name,PromotText,IsActive,Collection_Price,Delivery_Price,PreparationTime,Category_Id,HasSubMenu,Remarks,MenuImage,bHasDressing,bHasTopping,bHasBase,ItemPosition,Topping_Price,LargeImage) values(\'" +
                        Name + "\',\'" + PromotText + "\',\'" + IsActive.ToString() + "\'," + Collection_Price.ToString() +
                        "," + Delivery_Price.ToString() + "," + PreparationTime.ToString() + "," + CategoryId + ",\'" +
                        HasSubMenu.ToString() + "\',\'" + Remarks + "\',\'" + MenuImage + "\',\'" + bHasDressing.ToString() +
                        "\',\'" + bHasTopping.ToString() + "\',\'" + bHasBase.ToString() + "\'," + ItemPosition.ToString() +
                        "," + Topping_Price.ToString() + ",\'" + LargeImage + "\')"); // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool DeleteMenuItem(long MenuId)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery("delete from [Menu_Item] where Menu_Id=" + MenuId + ""); // change the query here
            }
            return false;
        }

        [WebMethod]
        public DataSet GetAllSubMenuItems(long MenuId)
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [SubMenu_Item] where Menu_Id=" + MenuId + " order by ItemPosition ");
                // change the query here
            }
            return null;
        }

        [WebMethod]
        public DataSet GetSubMenuItemInfo(long SubMenuId)
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [SubMenu_Item] where SubMenu_Id=" + SubMenuId + "");
                // change the query here
            }
            return null;
        }

        [WebMethod]
        public bool AddSubMenuItem(string Name, long MenuId, double DeliveryPrice, double CollectionPrice,
                                   int PreparationTime, int ItemPosition, bool Active, double ToppingPrice)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery(
                        "insert into SubMenu_Item (Name,Menu_Id,Delivery_Price,Collection_Price,PreparationTime,ItemPosition,IsActive,Topping_Price) values(\'" +
                        Name + "\'," + MenuId + "," + DeliveryPrice.ToString() + "," + CollectionPrice.ToString() + "," +
                        PreparationTime.ToString() + "," + ItemPosition.ToString() + ",\'" + Active.ToString() + "\'," +
                        ToppingPrice.ToString() + ")"); // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool UpdateSubMenuItem(long SubMenuId, string Name, long MenuId, double DeliveryPrice, double CollectionPrice,
                                      int PreparationTime, int ItemPosition, bool Active, double ToppingPrice)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery("update [SubMenu_Item] set Name=\'" + Name + "\', Menu_Id=\'" + MenuId +
                                       "\',Delivery_Price=\'" + DeliveryPrice.ToString() + "\',Collection_Price=\'" +
                                       CollectionPrice.ToString() + "\',PreparationTime=\'" + PreparationTime.ToString() +
                                       "\',ItemPosition=" + ItemPosition.ToString() + ",[IsActive]=\'" + Active.ToString() +
                                       "\',Topping_Price=\'" + ToppingPrice.ToString() + "\'  where SubMenu_Id=" + SubMenuId +
                                       ""); // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool DeleteSubMenuItem(long SubMenuId)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery("delete from [SubMenu_Item] where SubMenu_Id=" + SubMenuId + "");
                // change the query here
            }
            return false;
        }


        [WebMethod]
        public DataSet GetAllDressings()
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Menu_Dressing] order by ItemPosition"); // change the query here
            }
            return null;
        }

        [WebMethod]
        public bool UpdateDressing(long DressingId, string Name, int Position)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery("update [Menu_Dressing] set Name=\'" + Name + "\',ItemPosition=" +
                                       Position.ToString() + "  where ID=" + DressingId + ""); // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool AddDressing(string Name, int Position)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery("insert into Menu_Dressing (Name,ItemPosition) values(\'" + Name + "\'," +
                                       Position.ToString() + ")"); // change the query here
            }
            return false;
        }

        [WebMethod]
        public DataSet GetDressingInfo(long DressId)
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Menu_Dressing] where Id=" + DressId + ""); // change the query here
            }
            return null;
        }

        [WebMethod]
        public bool DeleteDressing(long DressId)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery("delete from [Menu_Dressing] where Id=" + DressId + ""); // change the query here
            }
            return false;
        }


        [WebMethod]
        public DataSet GetAllTopping()
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Menu_Topping] order by Position"); // change the query here
            }
            return null;
        }

        [WebMethod]
        public bool UpdateTopping(long ToppingId, string Name, int Position)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery("update [Menu_Topping] set Name=\'" + Name + "\',Position=" + Position.ToString() +
                                       "  where Topping_Id=" + ToppingId + ""); // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool AddTopping(string Name, int Position)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery("insert into Menu_Topping (Name,Position) values(\'" + Name + "\'," +
                                       Position.ToString() + ")"); // change the query here
            }
            return false;
        }

        [WebMethod]
        public DataSet GetToppingInfo(long ToppingId)
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Menu_Topping] where Topping_Id=" + ToppingId + "");
                // change the query here
            }
            return null;
        }

        [WebMethod]
        public bool DeleteTopping(long ToppingId)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery("delete from [Menu_Topping] where Id=" + ToppingId + ""); // change the query here
            }
            return false;
        }


        //Side Order management

        [WebMethod]
        public DataSet GetAllSideMenuItems()
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Menu_Item_SideOrder] order by ItemPosition "); // change the query here
            }
            return null;
        }

        [WebMethod]
        public DataSet GetSideMenuItemInfo(long MenuId)
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Menu_Item_SideOrder] where Menu_Id=" + MenuId + "");
                // change the query here
            }
            return null;
        }

        [WebMethod]
        public bool UpdateSideMenuItem(long MenuId, string Name, double Collection_Price, double Delivery_Price,
                                       string MenuImage, int ItemPosition, string LargeImage)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery("update [Menu_Item_SideOrder] set Name=\'" + Name + "\',Collection_Price=" +
                                       Collection_Price.ToString() + ",Delivery_Price=" + Delivery_Price.ToString() +
                                       ",MenuImage=\'" + MenuImage + "\',ItemPosition=" + ItemPosition.ToString() +
                                       " ,LargeImage=\'" + LargeImage + "\' where Menu_Id=" + MenuId + "");
                // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool AddSideMenuItem(string Name, double Collection_Price, double Delivery_Price, string MenuImage,
                                    int ItemPosition, string LargeImage)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery(
                        "insert into Menu_Item_SideOrder (Name,Collection_Price,Delivery_Price,MenuImage,ItemPosition,LargeImage) values(\'" +
                        Name + "\'," + Collection_Price.ToString() + "," + Delivery_Price.ToString() + ",\'" + MenuImage +
                        "\'," + ItemPosition.ToString() + ",\'" + LargeImage + "\')"); // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool DeleteSideMenuItem(long MenuId)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery("delete from [Menu_Item_SideOrder] where Menu_Id=" + MenuId + "");
                // change the query here
            }
            return false;
        }


        //Restaurant Timing Information
        [WebMethod]
        public DataSet GetRestaurantTiming()
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Restaurant_Timing] "); // change the query here
            }
            return null;
        }

        [WebMethod]
        public DataSet GetRestaurantTimingInfo(long TimeId)
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Restaurant_Timing] where Time_Id=" + TimeId + "");
                // change the query here
            }
            return null;
        }

        [WebMethod]
        public bool UpdateRestaurantTiming(long TimeId, short DayNo, string OpeningTime, string ClosingTime,
                                           double DiscountOver, int DiscountPercent, double DiscountValue, string OfferText)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery("update [Restaurant_Timing] set Day=" + DayNo + ",Opening_Time=\'" + OpeningTime +
                                       "\',Closing_Time=\'" + ClosingTime + "\',DiscountOver=" + DiscountOver.ToString() +
                                       ",DiscountPercent=" + DiscountPercent.ToString() + ",DiscountValue=" +
                                       DiscountValue.ToString() + ",OfferText=\'" + OfferText + "\' where Time_Id=" + TimeId +
                                       ""); // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool AddRestaurantTiming(short DayNo, string OpeningTime, string ClosingTime)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery("insert into Restaurant_Timing (DayNo,Opening_Time,Closing_Time) values(" + DayNo +
                                       ",\'" + OpeningTime + "\',\'" + ClosingTime + "\')"); // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool DeleteRestaurantTiming(long TimeId)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery("delete from [Restaurant_Timing] where Time_Id=" + TimeId + "");
                // change the query here
            }
            return false;
        }


        //Delivery Timing Information
        [WebMethod]
        public DataSet GetDeliveryTiming()
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Delivery_Timing]"); // change the query here
            }
            return null;
        }

        [WebMethod]
        public DataSet GetDeliveryTimingInfo(short DTId)
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from [Delivery_Timing] where DeliveryTime_Id=" + DTId + "");
                // change the query here
            }
            return null;
        }

        [WebMethod]
        public bool UpdateDeliveryTiming(long DTId, short DayNo, string StartTime, string EndTime, double DiscountOver,
                                         int DiscountPercent, double DiscountValue, string OfferText)
        {
            try
            {
                if (Authenticate())
                {
                    return
                        ExecuteUpdateQuery("update [Delivery_Timing] set Delivery_Day=" + DayNo + ",Start_Time=\'" +
                                           StartTime + "\',End_Time=\'" + EndTime + "\',DiscountOver=" +
                                           DiscountOver.ToString() + ",DiscountPercent=" + DiscountPercent.ToString() +
                                           ",DiscountValue=" + DiscountValue.ToString() + ",OfferText=\'" + OfferText +
                                           "\' where DeliveryTime_Id=" + DTId + ""); // change the query here
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        [WebMethod]
        public bool AddDeliveryTiming(short DayNo, string StartTime, string EndTime)
        {
            if (Authenticate())
            {
                return
                    ExecuteUpdateQuery("insert into Delivery_Timing (Delivery_Day,Start_Time,End_Time) values(" + DayNo +
                                       ",\'" + StartTime + "\',\'" + EndTime + "\')"); // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool DeleteDeliveryTiming(long DTId)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery("delete from [Delivery_Timing] where DeliveryTime_Id=" + DTId + "");
                // change the query here
            }
            return false;
        }

        [WebMethod]
        public bool UpdateMinOrderValue(double MinOrderValue)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery("update [Restaurant] set MinDeliveryValue=" + MinOrderValue.ToString() + "");
                // change the query here
            }
            return false;
        }

        private DataSet GetDataSet(string strSQL)
        {
            try
            {
                //1. Create a connection
                //retrive this connection string from Web.Config file and decrypt it
                string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

                var myConnection = new SqlConnection(constr);
                var myDataSet = new DataSet();

                //2. Create the command object, passing in the SQL string
                var myCommand = new SqlCommand(strSQL, myConnection);
                if (myConnection != null)
                {
                    myConnection.Open();
                }
                if (myConnection.State == ConnectionState.Open)
                {
                    //3. Create the DataAdapter
                    var myDataAdapter = new SqlDataAdapter();
                    myDataAdapter.SelectCommand = myCommand;

                    //4. Populate the DataSet and close the connection

                    myDataAdapter.Fill(myDataSet);
                    myConnection.Close();
                }

                //Return the DataSet
                return myDataSet;
            }
            catch (Exception)
            {
            }


            return null;
        }

        private bool ExecuteUpdateQuery(string strSQL)
        {
            try
            {
                //1. Create a connection
                //retrive this connection string from Web.Config file and decrypt it
                string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

                var myConnection = new SqlConnection(constr);


                //2. Create the command object, passing in the SQL string
                var myCommand = new SqlCommand(strSQL, myConnection);
                if (myConnection != null)
                {
                    myConnection.Open();
                }
                if (myConnection.State == ConnectionState.Open)
                {
                    //3. Create the DataAdapter
                    myCommand.ExecuteNonQuery();

                    //4.  close the connection

                    myConnection.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //Voucher Code Functions

        [WebMethod]
        public bool AddVoucherCode(string Name, double DiscountOver, int DiscountPercent, double DiscountValue,
                                   string OfferText, bool ValidwithOffers, DateTime Expiry_Date, bool IsActive, bool Sunday,
                                   bool Monday, bool Tuesday, bool Wednesday, bool Thursday, bool Friday, bool Saturday)
        {
            try
            {
                if (Authenticate())
                {
                    //Dim Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday As Boolean
                    //Select Case DayNo
                    //    Case 0 'sunday
                    //        Sunday = True
                    //    Case 1 'monday
                    //        Monday = False
                    //    Case 2 'tuesday
                    //        Tuesday = False
                    //    Case 3 'wednesday
                    //        Wednesday = False
                    //    Case 4 'thursday
                    //        Thursday = False
                    //    Case 5 'friday
                    //        Friday = False
                    //    Case 6 'saturday
                    //        Saturday = False
                    //End Select
                    return
                        ExecuteUpdateQuery(
                            "insert into Voucher_Codes (Name,DiscuntOver,DiscountPercent,DiscountValue,OfferText,ValidwithOffers,Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Expiry_Date,IsActive) values(" +
                            Name + "," + DiscountOver.ToString() + "," + DiscountPercent.ToString() + "," +
                            DiscountValue.ToString() + ",\'" + OfferText + "\',\'" + ValidwithOffers + "\',\'" + Sunday +
                            "\',\'" + Monday + "\',\'" + Tuesday + "\',\'" + Wednesday + "\',\'" + Thursday + "\',\'" +
                            Friday + "\',\'" + Saturday + "\',\'" + Expiry_Date + "\',\'" + IsActive + "\')");
                    // change the query here
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        [WebMethod]
        public bool UpdateVoucherCode(long voucherid, string Name, double DiscountOver, int DiscountPercent,
                                      double DiscountValue, string OfferText, bool ValidwithOffers, DateTime Expiry_Date,
                                      bool IsActive, bool Sunday, bool Monday, bool Tuesday, bool Wednesday, bool Thursday,
                                      bool Friday, bool Saturday)
        {
            try
            {
                if (Authenticate())
                {
                    return
                        ExecuteUpdateQuery("update Voucher_Codes set Name=\'" + Name + "\',DiscountOver=" +
                                           DiscountOver.ToString() + ",DiscountPercent=" + DiscountPercent.ToString() +
                                           ",DiscountValue=" + DiscountValue.ToString() + ",OfferText=\'" + OfferText +
                                           "\',ValidWithOffers=" + ValidwithOffers + ",Sunday=" + Sunday + ",Monday=" +
                                           Monday + ",Tuesday=" + Tuesday + ",Wednesday=" + Wednesday + ",Thursday=" +
                                           Thursday + ",Friday=" + Friday + ",Saturday=" + Saturday + ",Expiry_Date=\'" +
                                           Expiry_Date + "\',isActive=" + IsActive + ""); // change the query here
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        [WebMethod]
        public bool DeleteVoucherCode(long VCodeId)
        {
            if (Authenticate())
            {
                return ExecuteUpdateQuery("delete from [Voucher_Codes] where Voucher_Id=" + VCodeId + "");
                // change the query here
            }
            return false;
        }

        [WebMethod]
        public DataSet GetAllVoucherCodes()
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from Voucher_Codes"); // change the query here
            }
            return null;
        }

        [WebMethod]
        public DataSet GetVoucherInfo(long VoucherId)
        {
            if (Authenticate())
            {
                return GetDataSet("Select * from Voucher_Codes where Voucher_Id=" + VoucherId + "");
                // change the query here
            }
            return null;
        }

        #region "PostCodeCharges Management"

        [WebMethod]
        public List<CsPostCodePrice> GetAllPostCodeCharges()
        {
            if (!Authenticate())
            {
                return null;
            }
            var cms = new ContentManagementServices();
            return cms.GetAllPostCodeCharges();
        }

        [WebMethod]
        public CsPostCodePrice SavePostCodeCharges(CsPostCodePrice postCodesPrices)
        {
            if (!Authenticate())
            {
                return null;
            }
            var cms = new ContentManagementServices();
            return cms.SavePostCodeCharge(postCodesPrices);
        }

        [WebMethod]
        public void DeletePostCodeCharges(CsPostCodePrice postCodesPrices)
        {
            if (!Authenticate())
            {
                return;
            }
            var cms = new ContentManagementServices();
            cms.DeletePostCodeCharge(postCodesPrices);
        }

        [WebMethod]
        public CsPostCodePrice GetPostCodePrice(string postCode)
        {
            if (!Authenticate())
            {
                return null;
            }
            var cms = new ContentManagementServices();
            return cms.GetPostCodePrice(postCode);
        }

        #endregion

        #region "Restaurant Managements"

        [WebMethod]
        public CsRestaurant SaveRestaurantChanges(CsRestaurant restaurant)
        {
            if (!Authenticate())
                return null;

            var cms = new ContentManagementServices();
            return cms.SaveRestaurantChanges(restaurant);
        }

        [WebMethod]
        public CsRestaurant LoadRestaurantInfo()
        {
            if (!Authenticate())
                return null;

            var cms = new ContentManagementServices();
            return cms.LoadRestaurantInfo();
        }

        #endregion

        #region "Menu-Options"

        [WebMethod]
        public CsMenuOption SaveMenuOption(CsMenuOption menuOpt)
        {
            if (!Authenticate())
                return null;

            var cms = new ContentManagementServices();
            return cms.SaveMenuOption(menuOpt);
        }

        [WebMethod]
        public bool DeleteMenuOption(CsMenuOption menuOpt)
        {
            if (!Authenticate())
                return false;

            var cms = new ContentManagementServices();
            return cms.DeleteMenuOption(menuOpt);
        }

        [WebMethod]
        public List<CsMenuOption> LoadAllMenuOptions()
        {
            if (!Authenticate())
                return null;

            var cms = new ContentManagementServices();
            return cms.GetAllMenuOptions();
        }

        [WebMethod]
        public CsOptionDetail SaveOptionDetail(CsOptionDetail menuOpt)
        {
            if (!Authenticate())
                return null;

            var cms = new ContentManagementServices();
            return cms.SaveOptionDetail(menuOpt);
        }

        [WebMethod]
        public bool DeleteOptionDetail(CsOptionDetail menuOpt)
        {
            if (!Authenticate())
                return false;

            var cms = new ContentManagementServices();
            return cms.DeleteOptionDetail(menuOpt);
        }

        [WebMethod]
        public List<CsOptionDetail> LoadAllOptionDetails(int optionID)
        {
            if (!Authenticate())
                return null;

            var cms = new ContentManagementServices();
            return cms.GetAllOptionDetails(optionID);
        }

        [WebMethod]
        public bool UpdateMenuItemWithLinkedOptions(CsMenuItem item)
        {
            if (!Authenticate())
                return false;

            var sbBuilder = new StringBuilder();
            sbBuilder.AppendLine("");
            sbBuilder.AppendLine("UPDATE [Menu_Item]");
            sbBuilder.AppendLine("    SET [Name] = '" + item.Name + "'");
            sbBuilder.AppendLine("      ,[PromotText] = '" + item.PromotionText + "'");
            sbBuilder.AppendLine("      ,[IsActive] = '" + item.IsActive + "'");
            sbBuilder.AppendLine("      ,[Collection_Price] = '" + item.CollectionPrice.ToString("N2") + "'");
            sbBuilder.AppendLine("      ,[Delivery_Price] = '" + item.DeliveryPrice.ToString("N2") + "'");
            sbBuilder.AppendLine("      ,[MultisaveQuantity] = " + item.MutilsaveQuantity + "");
            sbBuilder.AppendLine("      ,[MultiSaveDiscount] = '" + item.MultiSaveDiscount.ToString("N2") + "'");
            sbBuilder.AppendLine("      ,[PreparationTime] = " + item.PreparationTime + "");
            sbBuilder.AppendLine("      ,[Category_Id] = " + item.CategoryID + "");
            sbBuilder.AppendLine("      ,[HasSubMenu] = '" + item.HasSubMenu + "'");
            sbBuilder.AppendLine("      ,[Remarks] = '" + item.Remarks + "'");
            sbBuilder.AppendLine("      ,[MenuImage] = '" + item.MenuImage + "'");
            sbBuilder.AppendLine("      ,[bHasDressing] = '" + item.HasDressing + "'");
            sbBuilder.AppendLine("      ,[bHasTopping] = '" + item.HasTopping + "'");
            sbBuilder.AppendLine("      ,[bHasBase] = '" + item.HasBase + "'");
            sbBuilder.AppendLine("      ,[ItemPosition] = " + item.ItemPosition + "");
            sbBuilder.AppendLine("      ,[Topping_Price] = '" + item.ToppingPrice.ToString("N2") + "'");
            sbBuilder.AppendLine("      ,[LargeImage] = '" + item.LargeImage + "'");
            sbBuilder.AppendLine("      ,[Option_ID_1] = " + item.OptionId1 + "");
            sbBuilder.AppendLine("      ,[Option_ID_2] = " + item.OptionId2 + "");
            sbBuilder.AppendLine("      ,[ToppingPrice1] = '" + item.ToppingPrice1.ToString("N2") + "'");
            sbBuilder.AppendLine("      ,[ToppingPrice2] = '" + item.ToppingPrice2.ToString("N2") + "'");
            sbBuilder.AppendLine("      ,[ToppingPrice3] = '" + item.ToppingPrice3.ToString("N2") + "'");
            sbBuilder.AppendLine(" WHERE ");
            sbBuilder.AppendLine("      [Menu_Id] = " + item.ID);

            return ExecuteUpdateQuery(sbBuilder.ToString());
        }

        [WebMethod]
        public bool AddMenuItemWithLinkedOptions(CsMenuItem item)
        {
            if (!Authenticate())
                return false;

            var sbBuilder = new StringBuilder();
            sbBuilder.AppendLine("");

            sbBuilder.AppendLine("INSERT INTO [Menu_Item]");
            sbBuilder.AppendLine("           ([Name]");
            sbBuilder.AppendLine("           ,[PromotText]");
            sbBuilder.AppendLine("           ,[IsActive]");
            sbBuilder.AppendLine("           ,[Collection_Price]");
            sbBuilder.AppendLine("           ,[Delivery_Price]");
            sbBuilder.AppendLine("           ,[MultisaveQuantity]");
            sbBuilder.AppendLine("           ,[MultiSaveDiscount]");
            sbBuilder.AppendLine("           ,[PreparationTime]");
            sbBuilder.AppendLine("           ,[Category_Id]");
            sbBuilder.AppendLine("           ,[HasSubMenu]");
            sbBuilder.AppendLine("           ,[Remarks]");
            sbBuilder.AppendLine("           ,[MenuImage]");
            sbBuilder.AppendLine("           ,[bHasDressing]");
            sbBuilder.AppendLine("           ,[bHasTopping]");
            sbBuilder.AppendLine("           ,[bHasBase]");
            sbBuilder.AppendLine("           ,[ItemPosition]");
            sbBuilder.AppendLine("           ,[Topping_Price]");
            sbBuilder.AppendLine("           ,[LargeImage]");
            sbBuilder.AppendLine("           ,[Option_ID_1]");
            sbBuilder.AppendLine("           ,[Option_ID_2]");
            sbBuilder.AppendLine("           ,[ToppingPrice1]");
            sbBuilder.AppendLine("           ,[ToppingPrice2]");
            sbBuilder.AppendLine("           ,[ToppingPrice3])");
            sbBuilder.AppendLine("     VALUES");
            sbBuilder.AppendLine("           ('" + item.Name + "'");
            sbBuilder.AppendLine("           ,'" + item.PromotionText + "'");
            sbBuilder.AppendLine("           ,'" + item.IsActive + "'");
            sbBuilder.AppendLine("           ,'" + item.CollectionPrice.ToString("N2") + "'");
            sbBuilder.AppendLine("           ,'" + item.DeliveryPrice.ToString("N2") + "'");
            sbBuilder.AppendLine("           ," + item.MutilsaveQuantity + "");
            sbBuilder.AppendLine("           ,'" + item.MultiSaveDiscount.ToString("N2") + "'");
            sbBuilder.AppendLine("           ," + item.PreparationTime + "");
            sbBuilder.AppendLine("           ," + item.CategoryID + "");
            sbBuilder.AppendLine("           ,'" + item.HasSubMenu + "'");
            sbBuilder.AppendLine("           ,'" + item.Remarks + "'");
            sbBuilder.AppendLine("           ,'" + item.MenuImage + "'");
            sbBuilder.AppendLine("           ,'" + item.HasDressing + "'");
            sbBuilder.AppendLine("           ,'" + item.HasTopping + "'");
            sbBuilder.AppendLine("           ,'" + item.HasBase + "'");
            sbBuilder.AppendLine("           ," + item.ItemPosition + "");
            sbBuilder.AppendLine("           ,'" + item.ToppingPrice.ToString("N2") + "'");
            sbBuilder.AppendLine("           ,'" + item.LargeImage + "'");
            sbBuilder.AppendLine("           ," + item.OptionId1 + "");
            sbBuilder.AppendLine("           ," + item.OptionId2 + "");
            sbBuilder.AppendLine("           ,'" + item.ToppingPrice1.ToString("N2") + "'");
            sbBuilder.AppendLine("           ,'" + item.ToppingPrice2.ToString("N2") + "'");
            sbBuilder.AppendLine("           ,'" + item.ToppingPrice3.ToString("N2") + "')");

            return ExecuteUpdateQuery(sbBuilder.ToString());
        }
        #endregion

        #region "Special Discount For Collection Orders"

        [WebMethod]
        public bool SaveSpecialDiscount(bool enabled, double value, double discount)
        {
            if (!Authenticate())
                return false;

            var cms = new ContentManagementServices();
            cms.SaveFlatField(SpeicalDiscountEnabled, enabled.ToString().ToLower());
            cms.SaveFlatField(SpeicalDiscountOrderValue, value.ToString("##.00"));
            cms.SaveFlatField(SpeicalDiscountPercent, discount.ToString("##.00"));
            return true;
        }

        [WebMethod]
        public string[] GetSpecialDiscount()
        {
            var resultList = new List<string>();
            if (!Authenticate())
                return resultList.ToArray();

            var cms = new ContentManagementServices();
            resultList.Add(cms.GetFlatFieldValue(SpeicalDiscountEnabled));
            resultList.Add(cms.GetFlatFieldValue(SpeicalDiscountOrderValue));
            resultList.Add(cms.GetFlatFieldValue(SpeicalDiscountPercent));

            return resultList.ToArray();
        }
        #endregion

        private const string SpeicalDiscountEnabled = "f_special_discount_enabled";
        private const string SpeicalDiscountOrderValue = "f_special_discount_order_value";
        private const string SpeicalDiscountPercent = "f_special_discount_percent";

        #region "Special Discount For Delivery Orders"

        [WebMethod]
        public bool SaveDeliverySpecialDiscount(bool enabled, double value, double discount)
        {
            if (!Authenticate())
                return false;

            var cms = new ContentManagementServices();
            cms.SaveFlatField(DELIVERY_SPEICAL_DISCOUNT_ENABLED, enabled.ToString().ToLower());
            cms.SaveFlatField(DELIVERY_SPEICAL_DISCOUNT_ORDER_VALUE, value.ToString("##.00"));
            cms.SaveFlatField(DELIVERY_SPEICAL_DISCOUNT_PERCENT, discount.ToString("##.00"));
            return true;
        }

        [WebMethod]
        public string[] GetDeliverySpecialDiscount()
        {
            var resultList = new List<string>();
            if (!Authenticate())
                return resultList.ToArray();

            var cms = new ContentManagementServices();
            resultList.Add(cms.GetFlatFieldValue(DELIVERY_SPEICAL_DISCOUNT_ENABLED));
            resultList.Add(cms.GetFlatFieldValue(DELIVERY_SPEICAL_DISCOUNT_ORDER_VALUE));
            resultList.Add(cms.GetFlatFieldValue(DELIVERY_SPEICAL_DISCOUNT_PERCENT));

            return resultList.ToArray();
        }
        #endregion

        private const string DELIVERY_SPEICAL_DISCOUNT_ENABLED = "f_delivery_special_discount_enabled";
        private const string DELIVERY_SPEICAL_DISCOUNT_ORDER_VALUE = "f_delivery_special_discount_order_value";
        private const string DELIVERY_SPEICAL_DISCOUNT_PERCENT = "f_delivery_special_discount_percent";


        #region "Topping"

        private BzMenuTopping _bzMenuTopping;

        [WebMethod]
        public CsToppingCategory[] GetToppingCategories()
        {
            if (!Authenticate()) return new CsToppingCategory[] {};
            if (_bzMenuTopping == null)
                _bzMenuTopping = new BzMenuTopping(GetDbConnectionString());
            return _bzMenuTopping.GetToppingCategories().ToArray();
        }

        [WebMethod]
        public CsToppingCategory GetToppingCategory(int id)
        {
            if (!Authenticate()) return null;
            if (_bzMenuTopping == null)
                _bzMenuTopping = new BzMenuTopping(GetDbConnectionString());
            return _bzMenuTopping.GetToppingCategory(id);
        }

        [WebMethod]
        public CsMenuTopping[] GetMenuToppings(int categoryId)
        {
            if (!Authenticate()) return new CsMenuTopping[] {};
            if (_bzMenuTopping == null)
                _bzMenuTopping = new BzMenuTopping(GetDbConnectionString());
            return _bzMenuTopping.GetMenuToppings(categoryId).ToArray();
        }

        [WebMethod]
        public CsMenuTopping GetMenuTopping(int id)
        {
            if (!Authenticate()) return null;
            if (_bzMenuTopping == null)
                _bzMenuTopping = new BzMenuTopping(GetDbConnectionString());
            return _bzMenuTopping.GetMenuTopping(id);
        }

        [WebMethod]
        public CsToppingCategory SaveToppingCategory(CsToppingCategory toppingCategory)
        {
            if (!Authenticate()) return null;
            if (_bzMenuTopping == null)
                _bzMenuTopping = new BzMenuTopping(GetDbConnectionString());
            return _bzMenuTopping.SaveToppingCategory(toppingCategory);
        }

        [WebMethod]
        public bool DeleteToppingCategory(int id)
        {
            if (!Authenticate()) return false;
            if (_bzMenuTopping == null)
                _bzMenuTopping = new BzMenuTopping(GetDbConnectionString());
            return _bzMenuTopping.DeleteToppingCategory(id);
        }

        [WebMethod]
        public CsMenuTopping SaveMenuTopping(CsMenuTopping menuTopping)
        {
            if (!Authenticate()) return null;
            if (_bzMenuTopping == null)
                _bzMenuTopping = new BzMenuTopping(GetDbConnectionString());
            return _bzMenuTopping.SaveMenuTopping(menuTopping);
        }

        [WebMethod]
        public bool DeleteMenuTopping(int id)
        {
            if (!Authenticate()) return false;
            if (_bzMenuTopping == null)
                _bzMenuTopping = new BzMenuTopping(GetDbConnectionString());
            return _bzMenuTopping.DeleteMenuTopping(id);
        }

        #endregion

        #region "MenuItem & SubMenuItem"

        private MenuBusinessLogic _bzMenuItem;

        [WebMethod]
        public CsSubMenuItem GetSubMenuItem(int id)
        {
            if (!Authenticate()) return null;
            if (_bzMenuItem == null)
            {
                _bzMenuItem = new MenuBusinessLogic(GetDbConnectionString());
            }
            return _bzMenuItem.GetSubMenuItem(id);
        }

        [WebMethod]
        public CsSubMenuItem SaveSubMenuItem(CsSubMenuItem subMenuItem)
        {
            if (!Authenticate()) return null;
            if (_bzMenuItem == null)
            {
                _bzMenuItem = new MenuBusinessLogic(GetDbConnectionString());
            }
            _bzMenuItem.SaveSubMenuItem(subMenuItem);
            return subMenuItem;
        }

        [WebMethod]
        public CsDealDetail SaveDealDetail(CsDealDetail detail)
        {
            if (!Authenticate()) return null;
            if (_bzMenuItem == null)
            {
                _bzMenuItem = new MenuBusinessLogic(GetDbConnectionString());
            }
            return _bzMenuItem.SaveDealDetail(detail);
        }

        [WebMethod]
        public bool DeleteDealDetail(int id)
        {
            if (!Authenticate()) return false;
            if (_bzMenuItem == null)
            {
                _bzMenuItem = new MenuBusinessLogic(GetDbConnectionString());
            }
            return _bzMenuItem.DeleteDealDetail(id);
        }

        [WebMethod]
        public CsDealDetail GetDealDetailById(int id, bool includeLinkedMenu)
        {
            if (!Authenticate()) return null;
            if (_bzMenuItem == null)
            {
                _bzMenuItem = new MenuBusinessLogic(GetDbConnectionString());
            }
            return _bzMenuItem.GetDealDetailById(id, includeLinkedMenu);
        }

        [WebMethod]
        public CsDealDetail[] GetDealDetailsByCategory(int categoryId)
        {
            if (!Authenticate()) return new CsDealDetail[] {};
            if (_bzMenuItem == null)
            {
                _bzMenuItem = new MenuBusinessLogic(GetDbConnectionString());
            }
            return _bzMenuItem.GetDealDetailsByCategory(categoryId, false);
        }

        [WebMethod]
        public CsMenuCategory GetMenuCategoryById(int id)
        {
            if (!Authenticate()) return null;
            if (_bzMenuItem == null)
            {
                _bzMenuItem = new MenuBusinessLogic(GetDbConnectionString());
            }
            return _bzMenuItem.GetMenuCategoryByID(id);
        }

        [WebMethod]
        public CsMenuCategory[] GetDealsCategories()
        {
            if (!Authenticate()) return new CsMenuCategory[] {};
            if (_bzMenuItem == null)
            {
                _bzMenuItem = new MenuBusinessLogic(GetDbConnectionString());
            }
            var allCategories = _bzMenuItem.GetAllMenuCategories();
            return allCategories.Where(c => c.IsDeal && c.IsActive).ToArray();
        }

        [WebMethod]
        public CsMenuCategory[] GetCategoriesAreAvailableForDeals()
        {
            if (!Authenticate()) return new CsMenuCategory[] {};
            if (_bzMenuItem == null)
            {
                _bzMenuItem = new MenuBusinessLogic(GetDbConnectionString());
            }
            var allCategories = _bzMenuItem.GetAllMenuCategories();
            return allCategories.Where(c => c.IsActive && c.IsAvailableForDeal).ToArray();
        }

        [WebMethod]
        public bool SaveWebCategoryInclDealAndMaxDressing(CsMenuCategory category)
        {
            if (!Authenticate()) return false;
            if (_bzMenuItem == null)
            {
                _bzMenuItem = new MenuBusinessLogic(GetDbConnectionString());
            }
            return _bzMenuItem.SaveMenuCategory(category);
        }

        #endregion

        /// <summary>
        /// Gets connection string that is specified in the appp.config
        /// </summary>
        /// <returns>Return the connection string. If it's not specified in app.config, this will retun the detault connection string</returns>
        /// <remarks></remarks>
        private string GetDbConnectionString()
        {
            string connectionStr = string.Empty;
            string connectionStringKey = string.Empty;
            if (ConfigurationManager.AppSettings[DEFAULT_CONNECTION_STRING] != null)
            {
                connectionStringKey = ConfigurationManager.AppSettings[DEFAULT_CONNECTION_STRING].Trim();
            }

            if (!string.IsNullOrEmpty(connectionStringKey))
            {
                connectionStr = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
            }

            return connectionStr;
        }
    }

    public class AuthenticateHeader : SoapHeader
    {
        public string SiteId;
        public string SitePwd;
    }
}