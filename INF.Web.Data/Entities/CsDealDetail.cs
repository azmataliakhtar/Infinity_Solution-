using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("DealDetail")]
    public class CsDealDetail : EntityBase
    {
        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("CategoryID")]
        public int CategoryID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Position")]
        public int Position { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("IsOneSize")]
        public bool IsOneSize { get; set; }

        [Column("CollectionUnitPrice")]
        public decimal CollectionUnitPrice { get; set; }

        [Column("DeliveryUnitPrice")]
        public decimal DeliveryUnitPrice { get; set; }

        [Column("PromotionText")]
        public string PromotionText { get; set; }

        [Column("Remarks")]
        public string Remarks { get; set; }

        [Column("OptionEnabled_1")]
        public bool OptionEnabled1 { get; set; }

        [Column("OptionEnabled_2")]
        public bool OptionEnabled2 { get; set; }

        [Column("OptionEnabled_3")]
        public bool OptionEnabled3 { get; set; }

        [Column("OptionEnabled_4")]
        public bool OptionEnabled4 { get; set; }

        [Column("OptionEnabled_5")]
        public bool OptionEnabled5 { get; set; }

        [Column("OptionEnabled_6")]
        public bool OptionEnabled6 { get; set; }

        [Column("OptionEnabled_7")]
        public bool OptionEnabled7 { get; set; }

        [Column("OptionEnabled_8")]
        public bool OptionEnabled8 { get; set; }

        [Column("OptionEnabled_9")]
        public bool OptionEnabled9 { get; set; }

        [Column("LinkedMenu_1")]
        public int LinkedMenu1 { get; set; }

        [Column("LinkedMenu_2")]
        public int LinkedMenu2 { get; set; }

        [Column("LinkedMenu_3")]
        public int LinkedMenu3 { get; set; }

        [Column("LinkedMenu_4")]
        public int LinkedMenu4 { get; set; }

        [Column("LinkedMenu_5")]
        public int LinkedMenu5 { get; set; }

        [Column("LinkedMenu_6")]
        public int LinkedMenu6 { get; set; }

        [Column("LinkedMenu_7")]
        public int LinkedMenu7 { get; set; }

        [Column("LinkedMenu_8")]
        public int LinkedMenu8 { get; set; }

        [Column("LinkedMenu_9")]
        public int LinkedMenu9 { get; set; }

        public virtual CsMenuCategory Category { get; set; }

        //public virtual CsMenuCategory LinkedMenuCategory1 { get; set; }

        //public virtual CsMenuCategory LinkedMenuCategory2 { get; set; }

        //public virtual CsMenuCategory LinkedMenuCategory3 { get; set; }

        //public virtual CsMenuCategory LinkedMenuCategory4 { get; set; }

        //public virtual CsMenuCategory LinkedMenuCategory5 { get; set; }

        //public virtual CsMenuCategory LinkedMenuCategory6 { get; set; }

        //public virtual CsMenuCategory LinkedMenuCategory7 { get; set; }

        //public virtual CsMenuCategory LinkedMenuCategory8 { get; set; }

        //public virtual CsMenuCategory LinkedMenuCategory9 { get; set; }

        public virtual List<CsMenuItem> LinkedMenuList1 { get; set; }
        public virtual List<CsMenuItem> LinkedMenuList2 { get; set; }
        public virtual List<CsMenuItem> LinkedMenuList3 { get; set; }
        public virtual List<CsMenuItem> LinkedMenuList4 { get; set; }
        public virtual List<CsMenuItem> LinkedMenuList5 { get; set; }
        public virtual List<CsMenuItem> LinkedMenuList6 { get; set; }
        public virtual List<CsMenuItem> LinkedMenuList7 { get; set; }
        public virtual List<CsMenuItem> LinkedMenuList8 { get; set; }
        public virtual List<CsMenuItem> LinkedMenuList9 { get; set; }

    }
}
