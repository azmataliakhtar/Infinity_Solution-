using System;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Serializable]
    [Table("Menu_Item")]
    public class CsMenuItem
    {
        [PrimaryKey("Menu_Id")]
        public decimal ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("PromotText")]
        public string PromotionText { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("Collection_Price")]
        public decimal CollectionPrice { get; set; }

        [Column("Delivery_Price")]
        public decimal DeliveryPrice { get; set; }

        [Column("MultisaveQuantity")]
        public int MutilsaveQuantity { get; set; }

        [Column("MultiSaveDiscount")]
        public decimal MultiSaveDiscount { get; set; }

        [Column("PreparationTime")]
        public int PreparationTime { get; set; }

        [Column("Category_Id")]
        public int CategoryID { get; set; }

        [Column("HasSubMenu")]
        public bool HasSubMenu { get; set; }

        [Column("Remarks")]
        public string Remarks { get; set; }

        [Column("MenuImage")]
        public string MenuImage { get; set; }

        [Column("bHasDressing")]
        public bool HasDressing { get; set; }

        [Column("bHasTopping")]
        public bool HasTopping { get; set; }

        [Column("bHasBase")]
        public bool HasBase { get; set; }

        [Column("ItemPosition")]
        public int ItemPosition { get; set; }

        [Column("Topping_Price")]
        public double ToppingPrice { get; set; }

        [Column("LargeImage")]
        public string LargeImage { get; set; }

        [Column("Option_ID_1")]
        public int OptionId1 { get; set; }

        [Column("Option_ID_2")]
        public int OptionId2 { get; set; }

        [Column("ToppingPrice1")]
        public decimal ToppingPrice1 { get; set; }

        [Column("ToppingPrice2")]
        public decimal ToppingPrice2 { get; set; }

        [Column("ToppingPrice3")]
        public decimal ToppingPrice3 { get; set; }

        public  virtual CsMenuCategory Category { get; set; }
    }
}
