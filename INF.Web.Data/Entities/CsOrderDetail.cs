using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("Order_Detail")]
    public class CsOrderDetail:EntityBase
    {
        [PrimaryKey("OrderDetail_Id")]
        public decimal ID { get; set; }

        [Column("OrderID")]
        public decimal OrderID { get; set; }

        [Column("MenuItemName")]
        public string MenuItemName { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("SpecialRequest")]
        public string SpecialRequest { get; set; }

        [Column("Dressing")]
        public string Dressing { get; set; }

        [Column("Status")]
        public bool Status { get; set; }
    }
}
