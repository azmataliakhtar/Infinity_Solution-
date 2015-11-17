using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

namespace INF.Web.Services
{
    /// <summary>
    /// Summary description for Orders
    /// </summary>
    [WebService(Namespace = "http://orders.infinitysol.co.uk/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Orders : WebService
    {
        public Orders()
        {
            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod()]
        public DataSet GetLatestOrders()
        {
            //Return GetOrderDataSet("Select ord.ID as OrderId,Cust.ID as CustomerId,Cust.FirstName,Cust.LastName,Cust.Address1,Cust.Address2,Cust.Address3,Cust.Telephone,Cust.Mobile,Cust.Email,Cust.City,Cust.PostCode,ord.OrderDate,Ord.PayStatus,ord.TotalAmount,ord.DeliveryCharges ,Ord.OrderStatus,Ord.OrderType,Ord.Special_Instructions from [Order] ord, Customer Cust where ord.CustomerID=Cust.Id and ord.OrderStatus='NEW' order by ord.OrderDate") ' change the query here
            var sbQuery = new StringBuilder();
            sbQuery.AppendLine("");
            sbQuery.AppendLine("Select ");
            sbQuery.AppendLine("    ord.OrderId as OrderId");
            sbQuery.AppendLine("    ,ord.OrderDate");
            sbQuery.AppendLine("    ,Ord.OrderType");
            sbQuery.AppendLine("    ,Ord.OrderStatus");
            sbQuery.AppendLine("    ,ord.TotalAmount");
            sbQuery.AppendLine("    ,Ord.Discount");
            sbQuery.AppendLine("    ,Ord.DeliveryCharges");
            sbQuery.AppendLine("    ,Ord.PaymentCharges");
            sbQuery.AppendLine("    ,Ord.PayStatus");
            sbQuery.AppendLine("    ,Ord.Special_Instructions");
            sbQuery.AppendLine("    ,Ord.ExpectedTime");
            sbQuery.AppendLine("    ,Cust.Customer_Id as CustomerId");
            sbQuery.AppendLine("    ,Cust.First_Name");
            sbQuery.AppendLine("    ,Cust.Last_Name");
            sbQuery.AppendLine("    ,Cust.Telephone");
            sbQuery.AppendLine("    ,Cust.Mobile");
            sbQuery.AppendLine("    ,Cust.Email");
            sbQuery.AppendLine("    ,CustAdd.[Address]");
            sbQuery.AppendLine("    ,CustAdd.City");
            sbQuery.AppendLine("    ,CustAdd.PostCode ");
            sbQuery.AppendLine("from ");
            sbQuery.AppendLine("    Customer Cust");
            sbQuery.AppendLine("    ,(Orderinfo ord left join Customer_Address CustAdd on ord.AddressId=CustAdd.Address_Id) ");
            sbQuery.AppendLine("where ");
            sbQuery.AppendLine("    ord.CustomerId=Cust.Customer_Id ");
            sbQuery.AppendLine("    and ord.OrderStatus=\'NEW\' ");
            sbQuery.AppendLine("order by ord.OrderDate");

            return GetOrderDataSet(sbQuery.ToString());

            //return
            //    GetOrderDataSet(
            //        "Select ord.OrderId as OrderId,ord.OrderDate,Ord.OrderType,Ord.OrderStatus,ord.TotalAmount,Ord.Discount,Ord.DeliveryCharges,Ord.PaymentCharges,Ord.PayStatus,Ord.Special_Instructions,Cust.Customer_Id as CustomerId,Cust.First_Name,Cust.Last_Name,Cust.Telephone,Cust.Mobile,Cust.Email,CustAdd.[Address],CustAdd.City,CustAdd.PostCode from Customer Cust,(Orderinfo ord left join Customer_Address CustAdd on ord.AddressId=CustAdd.Address_Id) where ord.CustomerId=Cust.Customer_Id and ord.OrderStatus=\'NEW\' order by ord.OrderDate");
            // change the query here
        }

        [WebMethod()]
        public DataSet GetLatestOrdersDetail(long orderId)
        {
            return GetOrderDetailDataSet("Select * from Order_Detail where OrderID=" + orderId + "");
            // change the query here
        }

        [WebMethod()]
        public bool UpdateOrderStatus(long orderId, string status)
        {
            return UpdateOrderStatus("update [orderinfo] set OrderStatus=\'" + status + "\' where OrderId=" + orderId + "");
            // change the query here
        }

        [WebMethod()]
        public bool UpdateOrderStatusWithProcessingTime(long orderId, string status, int processingTime)
        {
            return UpdateOrderStatus("update [orderinfo] set OrderStatus=\'" + status + "\', ProcessingTime=" + processingTime + " where OrderId=" + orderId + "");
            // change the query here
        }

        [WebMethod()]
        public bool SetShopStatus(bool shopStatus)
        {
            return UpdateShopStatus("update Restaurant set websiteStatus=\'" + shopStatus.ToString() + "\'");
            // change the query here
        }

        [WebMethod()]
        public bool SetCashMethod(bool cashStatus)
        {
            return UpdateShopStatus("update Restaurant set EnableCashPayments=\'" + cashStatus.ToString() + "\'");
            // change the query here
        }

        [WebMethod()]
        public bool SetCardMethod(bool cardStatus)
        {
            return UpdateShopStatus("update Restaurant set EnableNochex=\'" + cardStatus.ToString() + "\'");
            // change the query here
        }

        [WebMethod()]
        public bool SetOpeningTime(string openingTime)
        {
            return UpdateShopStatus("update Restaurant set StartTime=\'" + openingTime + "\'"); // change the query here
        }

        [WebMethod()]
        public bool SetClosingTime(string closingTime)
        {
            return UpdateShopStatus("update Restaurant set CloseTime=\'" + closingTime.ToString() + "\'");
            // change the query here
        }

        //// Implementations

        private DataSet GetOrderDataSet(string strSql)
        {
            //1. Create a connection
            //retrive this connection string from Web.Config file and decrypt it
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            SqlConnection myConnection = new SqlConnection(constr);
            DataSet myDataSet = new DataSet();

            //2. Create the command object, passing in the SQL string
            SqlCommand myCommand = new SqlCommand(strSql, myConnection);
            if (myConnection != null)
            {
                myConnection.Open();
            }
            if (myConnection.State == ConnectionState.Open)
            {
                //3. Create the DataAdapter
                SqlDataAdapter myDataAdapter = new SqlDataAdapter();
                myDataAdapter.SelectCommand = myCommand;


                //4. Populate the DataSet and close the connection

                myDataAdapter.Fill(myDataSet);
                myConnection.Close();
            }

            //Return the DataSet
            return myDataSet;
        }

        private DataSet GetOrderDetailDataSet(string strSql)
        {
            //1. Create a connection
            //retrive this connection string from Web.Config file and decrypt it
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            SqlConnection myConnection = new SqlConnection(constr);
            DataSet myDataSet = new DataSet();

            //2. Create the command object, passing in the SQL string
            SqlCommand myCommand = new SqlCommand(strSql, myConnection);
            if (myConnection != null)
            {
                myConnection.Open();
            }
            if (myConnection.State == ConnectionState.Open)
            {
                //3. Create the DataAdapter
                SqlDataAdapter myDataAdapter = new SqlDataAdapter();
                myDataAdapter.SelectCommand = myCommand;


                //4. Populate the DataSet and close the connection

                myDataAdapter.Fill(myDataSet);
                myConnection.Close();
            }

            //Return the DataSet
            return myDataSet;
        }

        private bool UpdateOrderStatus(string strSql)
        {
            try
            {
                //1. Create a connection
                //retrive this connection string from Web.Config file and decrypt it
                string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

                SqlConnection myConnection = new SqlConnection(constr);


                //2. Create the command object, passing in the SQL string
                SqlCommand myCommand = new SqlCommand(strSql, myConnection);
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

        private bool UpdateShopStatus(string strSql)
        {
            try
            {
                //1. Create a connection
                //retrive this connection string from Web.Config file and decrypt it
                string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

                SqlConnection myConnection = new SqlConnection(constr);


                //2. Create the command object, passing in the SQL string
                SqlCommand myCommand = new SqlCommand(strSql, myConnection);
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
    }
}
