using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using INF.Database.Actions;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("BasketTemp")]
    public class CsBasketTemp : EntityBase
    {
        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("UserLogIn")]
        public string UserLogIn { get; set; }

        [Column("CustomerID")]
        public int CustomerID { get; set; }

        [Column("OrderType")]
        public string OrderType { get; set; }

        [Column("OrderStatus")]
        public string OrderStatus { get; set; }

        [Column("TotalAmount")]
        public decimal TotalAmount { get; set; }

        [Column("AmountReceived")]
        public decimal AmountReceived { get; set; }

        [Column("AmountDue")]
        public decimal AmountDue { get; set; }

        [Column("PayStatus")]
        public string PayStatus { get; set; }

        [Column("VoucherCode")]
        public string VoucherCode { get; set; }

        [Column("DiscountType")]
        public string DiscountType { get; set; }

        [Column("Discount")]
        public decimal Discount { get; set; }

        [Column("DeliveryCharge")]
        public decimal DeliveryCharge { get; set; }

        [Column("PaymentCharge")]
        public decimal PaymentCharge { get; set; }

        [Column("PaymentType")]
        public string PaymentType { get; set; }

        [Column("SpecialInstructions")]
        public string SpecialInstructions { get; set; }

        [Column("AddressID")]
        public int AddressID { get; set; }

        [Column("OrderDate")]
        public DateTime OrderDate { get; set; }

        [Column("SagePayStatus")]
        public string SagePayStatus { get; set; }

        //ExpectedTime
        [Column("ExpectedTime")]
        public string ExpectedTime { get; set; }

        [Column("VpsTxId")]
        public string VpsTxId { get; set; }
        [Column("TxAuthNo")]
        public string TxAuthNo { get; set; }
        [Column("BankAuthCode")]
        public string BankAuthCode { get; set; }
        [Column("Last4Digits")]
        public string Last4Digits { get; set; }

        [Column("CreatedOn")]
        public DateTime? CreatedOn { get; set; }
        [Column("CreatedBy")]
        public string CreatedBy { get; set; }
        [Column("ChangedOn")]
        public DateTime? ChangedOn { get; set; }
        [Column("ChangedBy")]
        public string ChangedBy { get; set; }

        public virtual List<CsBasketItemTemp> Items { get; set; }

        public string Email
        {
            get
            {
                if (!string.IsNullOrEmpty(UserLogIn))
                {
                    int pos = UserLogIn.IndexOf("_", StringComparison.Ordinal);
                    return UserLogIn.Substring(pos + 1);
                }
                return string.Empty;
            }
        }

        public virtual string CustomerAddress { get; set; }
        public virtual string CustomerName { get; set; }
    }

}
