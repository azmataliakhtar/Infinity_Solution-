using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("BasketItemTemp")]
    public class CsBasketItemTemp : EntityBase
    {
        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("BasketID")]
        public int BasketID { get; set; }

        [Column("UnitPrice")]
        public decimal UnitPrice { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        [Column("SpecialRequest")]
        public string SpecialRequest { get; set; }

        [Column("ItemName")]
        public string ItemName { get; set; }

        [Column("Status")]
        public bool Status { get; set; }

        [Column("Dressing")]
        public string Dressing { get; set; }
    }
}
