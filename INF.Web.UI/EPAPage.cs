using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using INF.Web.Data.BLL;
using INF.Web.Data.Entities;
using INF.Web.UI.Settings;
using INF.Web.UI.Shopping;
using INF.Web.UI.Utils;
using Microsoft.VisualBasic;

namespace INF.Web.UI
{
    public class EPAPage:BasePage
    {
        #region "Sessions, Values, ViewStates Constants"

        protected const string SSN_LOGGED_IN_USER = "LoggedInUser";

        protected const string SSN_CUSTOMER_ID = "CurrentUserId";

        protected const string SSN_REFERRER_URL = "ReferrerUrl";
        protected const string SSN_ADDRESS_ID = "AddressId";
        protected const string SSN_ORDER_TYPE = "OrderType";
        protected const string SSN_SPEC_INSTR = "SpecInstr";
        protected const string SSN_IS_CART_EMPTY = "IsCartEmpty";
        protected const string SSN_PAYMENT_MODE = "PaymentMode";
        protected const string SSN_PAYMENT_CHARGES = "PaymentCharges";

        protected const string SSN_TOTAL_ORDER_PRICE = "TotalOrderPrice";

        protected const string SSN_VOUCHER_CODE = "VoucherCode";
        protected const string CNS_OT_COLLECTION = "COLLECTION";

        protected const string CNS_OT_DELIVERY = "DELIVERY";
        protected const string CNS_PAYMENT_CASH = "CASH";

        protected const string CNS_PAYMENT_CARD = "CARD";

        protected const string SSN_ORDER_CONFIRMATION_PAGE = "OrderConfirmationPage";

        protected const string SSN_ERROR_MESSAGE = "EPOS_ERROR_MESSAGE";
        #endregion

        #region "Vars"

        //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private double _remainingTime;
        #endregion

        #region "Properties"

        protected string INFTheme
        {
            get
            {
                return WebsiteConfig.Instance.INFTheme;
            }
        }

        protected string INFMasterPageFile
        {
            get
            {
                return WebsiteConfig.Instance.INFMasterPageFile;
            }
        }

        protected string ErrorMessage
        {
            get
            {
                if (Session[SSN_ERROR_MESSAGE] != null)
                {
                    return Session[SSN_ERROR_MESSAGE].ToString();
                }
                return "";
            }
            set
            {
                Session[SSN_ERROR_MESSAGE] = value;
            }
        }
        protected bool IsAuthenticated
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.User != null)
                {
                    return HttpContext.Current.User.Identity.IsAuthenticated;
                }
                return false;
            }
        }

        protected double RemainingTime
        {
            get
            {
                CheckRestaurantTiming();
                return _remainingTime;
            }
        }

        protected bool ShopStatus
        {
            get { return CheckRestaurantTiming(); }
        }

        protected bool IsDeliveryOrder
        {
            get
            {
                var blnDeliveryOrder = (Session[BxShoppingCart.SS_ORDER_TYPE] != null) && Convert.ToString(Session[BxShoppingCart.SS_ORDER_TYPE]) == BxShoppingCart.ORDER_TYPE_DELIVERY;
                return blnDeliveryOrder;
            }
        }

        protected string CurrentPostcode
        {
            get
            {
                string postCodeStr = string.Empty;
                if ((Session[BxShoppingCart.SS_POST_CODE] != null))
                {
                    postCodeStr = Convert.ToString((Session[BxShoppingCart.SS_POST_CODE]));
                }
                return postCodeStr;
            }
        }

        protected double PostcodeCharge
        {
            get
            {
                double dblPostCodeCharge = 0;
                if ((Session[BxShoppingCart.POST_CODE_CHARGE] != null) && Information.IsNumeric(Session[BxShoppingCart.POST_CODE_CHARGE]))
                {
                    dblPostCodeCharge = Convert.ToDouble((Session[BxShoppingCart.POST_CODE_CHARGE]));
                }
                return dblPostCodeCharge;
            }
        }

        #endregion

        #region "Private Methods"

        private bool CheckRestaurantTiming()
        {
            var vBusiness = new RestaurantBusinessLogic();
            _remainingTime = vBusiness.CheckRestaurentTiming((int)DateTime.Now.DayOfWeek);
            return _remainingTime > 0;
        }

        #endregion

        #region "Protected Methods"

        protected CsCustomer GetLoggedInCustomer()
        {

            if ((HttpContext.Current.User != null))
            {
                string vEmail = HttpContext.Current.User.Identity.Name;

                if (!string.IsNullOrWhiteSpace(vEmail))
                {
                    var vBusiness = new CustomerBusinessLogic();
                    return vBusiness.GetCustomerByEmail(vEmail);
                }
            }

            return null;
        }

        protected string BuildCssGradientBackgound(string vBaseColor, string vBackColor)
        {
            return WebUtil.BuildCssGradientBackgound(vBaseColor, vBackColor);
        }

        protected void CleanShoppingCart()
        {
            BxShoppingCart.GetShoppingCart().Clear();
            Session[SSN_VOUCHER_CODE] = string.Empty;
            Session[SSN_SPEC_INSTR] = null;
            Session[SSN_IS_CART_EMPTY] = true;
        }

        #endregion

        protected string ExtractPaymentStatus(string vCrypt)
        {
            if (string.IsNullOrWhiteSpace(vCrypt))
            {
                return string.Empty;
            }

            string paymentStatus = "";
            string[] pairs = vCrypt.Split('&');
            if (pairs.Length > 0)
            {
                foreach (string p in pairs)
                {
                    string[] keyValue = p.Split('=');
                    if (keyValue.Length == 2)
                    {
                        if (keyValue[0].ToLower() == "status")
                        {
                            paymentStatus = keyValue[1];
                        }
                    }
                }
            }
            return paymentStatus.ToUpper();
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            Page.Theme = INFTheme;
            //MasterPageFile = INFMasterPageFile;
        }
    }
}