using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("Customer_Address")]
    public class CsCustomerAddress : EntityBase
    {
        [PrimaryKey("Address_Id")]
        public decimal ID { get; set; }

        [Column("Customer_Id")]
        public decimal CustomerID { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("PostCode")]
        public string PostCode { get; set; }

        [Column("County")]
        public string Country { get; set; }

        [Column("AddressNotes")]
        public string AddressNotes { get; set; }

        [Column("GridEast")]
        public string GridEast { get; set; }

        [Column("GridNorth")]
        public string GridNorth { get; set; }

        [Column("Distance")]
        public double Distance { get; set; }

        public override string ToString()
        {
            return PostCode + "-" + Address + "-" + City;
        }
    }
}
