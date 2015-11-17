using System;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("Restaurant")]
    [Serializable()]
    public class CsRestaurant
    {
        [PrimaryKey("RestaurantID")]
        public int ID { get; set; }

        [Column("ShopName")]
        public string ShopName { get; set; }

        [Column("ShopNo")]
        public string ShopNo { get; set; }

        [Column("Building_Name")]
        public string BuildingName { get; set; }

        [Column("Street")]
        public string Street { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("PostCode")]
        public string PostCode { get; set; }

        [Column("County")]
        public string County { get; set; }

        [Column("Telephone1")]
        public string Telephone1 { get; set; }

        [Column("Telephone2")]
        public string Telephone2 { get; set; }

        [Column("Mobile")]
        public string Mobile { get; set; }

        [Column("Fax")]
        public string Fax { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Logo")]
        public byte[] Logo { get; set; }

        [Column("Slogan")]
        public string Slogan { get; set; }

        [Column("WebURL")]
        public string WebURL { get; set; }

        [Column("NumberOfTelLines")]
        public int NumberOfTelLines { get; set; }

        [Column("OrderStartNo")]
        public string OrderStartNo { get; set; }

        [Column("DeliveryValue")]
        public decimal DeliveryValue { get; set; }

        [Column("DeliveryCharge")]
        public decimal DeliveryCharge { get; set; }

        [Column("ServiceCharge")]
        public decimal ServiceCharge { get; set; }

        [Column("BackUpDay")]
        public int BackUpDay { get; set; }

        [Column("WebSiteStatus")]
        public bool WebSiteStatus { get; set; }

        [Column("Messageoftheday")]
        public string Messageoftheday { get; set; }

        [Column("EnableCashPayments")]
        public bool EnableCashPayments { get; set; }

        [Column("EnableNochex")]
        public bool EnableNochex { get; set; }

        [Column("OnlineDiscount")]
        public decimal OnlineDiscount { get; set; }

        //[Column("SelectedTheme")]
        //public string SelectedTheme { get; set; }
    }
}
