using System;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Serializable]
    [Table("PostCodesPrices")]
    public class CsPostCodePrice : EntityBase
    {
        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("POST_CODE")]
        public string PostCode { get; set; }

        [Column("PRICE")]
        public decimal Price { get; set; }

        [Column("ALLOW_DELIVERY")]
        public bool AllowDelivery { get; set; }

        [Column("MIN_ORDER")]
        public decimal MinOrder { get; set; }
    }
}
