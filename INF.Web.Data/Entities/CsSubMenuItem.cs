using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("SubMenu_Item")]
    public class CsSubMenuItem : EntityBase
    {
        [PrimaryKey("SubMenu_Id")]
        public decimal ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Menu_Id")]
        public decimal MenuID { get; set; }

        [Column("Delivery_Price")]
        public double DeliveryPrice { get; set; }

        [Column("Collection_Price")]
        public double CollectionPrice { get; set; }

        [Column("MultisaveQuantity")]
        public int MultisaveQuantity { get; set; }

        [Column("MultiSaveDiscount")]
        public double MultiSaveDiscount { get; set; }

        [Column("PreparationTime")]
        public int PreparationTime { get; set; }

        [Column("SubMenuItemImage")]
        public string ImageUrl { get; set; }

        [Column("ItemPosition")]
        public int Position { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("Topping_Price")]
        public double ToppingPrice { get; set; }

        [Column("Remarks")]
        public string Remarks { get; set; }

        [Column("ToppingPrice1")]
        public decimal ToppingPrice1 { get; set; }

        [Column("ToppingPrice2")]
        public decimal ToppingPrice2 { get; set; }

        [Column("ToppingPrice3")]
        public decimal ToppingPrice3 { get; set; }

        public virtual CsMenuItem MenuItem { get; set; }
    }
}
