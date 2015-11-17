using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Web.Data.DAL.SqlClient;
using INF.Web.Data.Entities;

namespace INF.Web.Data.BLL
{
    public class CustomerBusinessLogic : BaseBusinessLogic
    {
        public bool CheckDuplicateEmail(string email)
        {
            var customer = CustomerProvider.Instance.GetCustomerByEmail(email);
            return !(customer != null && customer.ID > 0);
        }

        public bool CheckDuplicateMobile(string mobile)
        {
            var customer = CustomerProvider.Instance.GetCustomerByMobile(mobile);
            return !(customer != null && customer.ID > 0);
        }

        public bool CheckDuplicateTelephone(string telephone)
        {
            var customer = CustomerProvider.Instance.GetCustomerByTelephone(telephone);
            return !(customer != null && customer.ID > 0);
        }

        public CsCustomer ValidateCustomer(string email, string password)
        {
            var customer = CustomerProvider.Instance.GetCustomerByEmail(email);
            if(customer != null)
            {
                // Do not encrypt the password... because of the old data do not...
                //if(customer.Password == CryptoUtility.EncryptText(password))
                if (customer.Password == password)
                {
                    return customer;
                }
            }
            return null;
        }

        public CsCustomer SaveCustomer(CsCustomer customer)
        {
            if (customer.ID == 0)
            {
                // Do not encrypt the password... because of the old data do not...
                //customer.Password = CryptoUtility.EncryptText(customer.Password);
            }

            var savedCustomer = CustomerProvider.Instance.SaveCustomer(customer);
            if (savedCustomer != null)
            {
                string key = string.Format("CustomerByID_{0}", customer.ID);
                if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
                {
                    Cache[key] = savedCustomer;
                }
                else
                {
                    CacheData(key, savedCustomer);
                }
            }
            return savedCustomer;
        }

        public CsCustomer GetCustomerByEmail(string email)
        {
            return CustomerProvider.Instance.GetCustomerByEmail(email);
        }

        public CsCustomer GetCustomerByID(int id)
        {
            return CustomerProvider.Instance.GetCustomerByID(id);
        }

        public IEnumerable<CsCustomer> GetAllCustomers()
        {
            string key = "AllCustomers_";
            if (WebSettings.Instance.EnaleCachingMasterData && Cache[key] != null)
            {
                return (IEnumerable<CsCustomer>) Cache[key];
            }

            var allCustomers = CustomerProvider.Instance.GetAllCustomers();
            CacheData(key, allCustomers);
            return allCustomers;
        }

        public void UnBlockAllCustomer()
        {
            CustomerProvider.Instance.UnBlockAllCustomer();
        }

        #region "Customer Address"
        
        public CsCustomerAddress SaveCustomerAddress(CsCustomerAddress address)
        {
            return CustomerProvider.Instance.SaveCustomerAddress(address);
        }

        public IEnumerable<CsCustomerAddress> GetCustomerAddresses(decimal customerId)
        {
            return CustomerProvider.Instance.GetCustomerAddresses(customerId);
        }

        public CsCustomerAddress GetCustomerAddressByID(decimal addressId)
        {
            return CustomerProvider.Instance.GetCustomerAddress(addressId);
        }

        public bool DeleteCustomerAddress(decimal addressId)
        {
            return CustomerProvider.Instance.DeleteCustomerAddress(addressId);
        }

        #endregion
    }
}
