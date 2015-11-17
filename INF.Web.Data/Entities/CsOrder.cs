using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("OrderInfo")]
    public class CsOrder : EntityBase
    {
        [PrimaryKey("OrderId")]
        public decimal ID { get; set; }

        [Column("CustomerID")]
        public decimal CustomerID { get; set; }

        [Column("OrderDate")]
        public DateTime OrderDate { get; set; }

        [Column("OrderType")]
        public string OrderType { get; set; }

        [Column("OrderStatus")]
        public string OrderStatus { get; set; }

        [Column("ProcessingTime")]
        public int ProcessingTime { get; set; }

        [Column("TotalAmount")]
        public double TotalAmount { get; set; }

        [Column("AmountReceived")]
        public double AmountReceived { get; set; }

        [Column("AmountDue")]
        public double AmountDue { get; set; }

        [Column("DiscountType")]
        public string DiscountType { get; set; }

        [Column("Discount")]
        public double Discount { get; set; }

        [Column("VoucherCode")]
        public string VoucherCode { get; set; }

        [Column("DeliveryCharges")]
        public double DeliveryCharges { get; set; }

        [Column("PaymentCharges")]
        public double PaymentCharges { get; set; }

        [Column("PaymentType")]
        public string PaymentType { get; set; }

        [Column("PayStatus")]
        public string PayStatus { get; set; }

        [Column("Special_Instructions")]
        public string SpecialInstructions { get; set; }

        [Column("IsEdited")]
        public bool IsEdited { get; set; }

        [Column("AnyReason")]
        public string AnyReason { get; set; }

        [Column("AddressId")]
        public decimal AddressId { get; set; }

        [Column("Shop_PostCode")]
        public string ShopPostCode { get; set; }

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

        public virtual string CustomerAddress { get; set; }
    }
}
