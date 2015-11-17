using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace INF.Web.UI.SagePay
{
    public class SagePayConfig
    {
        private static readonly string _serverName;
        private static readonly string _successUrl;
        private static readonly string _cancelUrl;
        private static readonly string _errorUrl;
        private static readonly string _paymentDoneUrl;
        private static readonly string _vendorName;
        private static readonly string _transDescription;
        private static readonly string _transValue;
        private static readonly string _vendorEmail;

        private SagePayConfig()
        {
        }

        static SagePayConfig()
        {
            // For SagePay integration
            _serverName = ConfigurationManager.AppSettings["SERVER_NAME"];
            _successUrl = ConfigurationManager.AppSettings["SUCCESS_URL"];

            _vendorName = ConfigurationManager.AppSettings["VENDOR_NAME"];
            _transDescription = ConfigurationManager.AppSettings["TRANSACTION_DESCRIPTION"];
            _transValue = ConfigurationManager.AppSettings["TRANSACTION_VALUE"];

            _vendorEmail = ConfigurationManager.AppSettings["VENDOR_EMAIL"];
            _cancelUrl = ConfigurationManager.AppSettings["CANCEL_URL"];
            _errorUrl = ConfigurationManager.AppSettings["ERROR_URL"];
            _paymentDoneUrl = ConfigurationManager.AppSettings["PAYMENTDONE_URL"];
        }

        #region "Properties"
        public static string VendorEmail
        {
            get { return _vendorEmail; }
        }

        public static string SuccessUrl
        {
            get { return _successUrl; }
        }
        public static string CancelUrl
        {
            get { return _cancelUrl; }
        }

        public static string ErrorUrl
        {
            get { return _errorUrl; }
        }
        public static string PaymentDoneUrl
        {
            get { return _paymentDoneUrl; }
        }

        public static string VendorName
        {
            get { return _vendorName; }
        }
        public static string TransDescription
        {
            get { return _transDescription; }
        }
        public static string TransValue
        {
            get { return _transValue; }
        }
        public static string ServerName
        {
            get { return _serverName; }
        }
        #endregion
    }
}