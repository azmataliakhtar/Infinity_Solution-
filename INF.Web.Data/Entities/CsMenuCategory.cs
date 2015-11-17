using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("Menu_Category")]
    public class CsMenuCategory : EntityBase
    {
        [PrimaryKey("Category_Id")]
        public decimal ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Active")]
        public bool IsActive { get; set; }

        [Column("ItemPosition")]
        public int ItemPosition { get; set; }

        [Column("NormalImage")]
        public string NormalImage { get; set; }

        [Column("MouseOverImage")]
        public string MouseOverImage { get; set; }

        [Column("ExclOnlineDiscount")]
        public bool ExclOnlineDiscount { get; set; }

        //IsDeal
        [Column("IsDeal")]
        public bool IsDeal { get; set; }

        //IsAvailableForDeal
        [Column("IsAvailableForDeal")]
        public bool IsAvailableForDeal { get; set; }

        [Column("MaxDressing")]
        public int MaxDressing { get; set; }
    }
}
