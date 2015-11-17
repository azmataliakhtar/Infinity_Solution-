using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using SagePay.IntegrationKit;
using SagePay.IntegrationKit.Messages;

namespace INF.Web.UI.SagePay
{
    public class SagePayFormIntegration : SagePayAPIIntegration
    {
        public IFormPayment FormPaymentRequest()
        {
            IFormPayment request = new DataObject();
            return request;
        }

        public NameValueCollection Validation(IFormPayment formPayment)
        {
            if (SagePaySettings.EnableClientValidation)
                return Validation(ProtocolMessage.FORM_PAYMENT, typeof(IFormPayment), formPayment, SagePaySettings.ProtocolVersion);
            return null;
        }

        public IFormPayment ProcessRequest(IFormPayment formPayment)
        {
            RequestQueryString = BuildQueryString(ConvertSagePayMessageToNameValueCollection(ProtocolMessage.FORM_PAYMENT, typeof(IFormPayment), formPayment, SagePaySettings.ProtocolVersion));

            formPayment.Crypt = Cryptography.EncryptAndEncode(RequestQueryString, SagePaySettings.EncryptionPassword);

            return formPayment;
        }

        public IFormPaymentResult ProcessResult(string crypt)
        {
            IFormPaymentResult formPaymentResult = new DataObject();

            string cryptDecoded = Cryptography.DecodeAndDecrypt(crypt, SagePaySettings.EncryptionPassword);
            cryptDecoded = cryptDecoded.Replace("3DSecureStatus=NOTAVAILABLE", "3DSecureStatus=OK");

            formPaymentResult = (IFormPaymentResult)ConvertToSagePayMessage(cryptDecoded);

            return formPaymentResult;
        }
    }
}