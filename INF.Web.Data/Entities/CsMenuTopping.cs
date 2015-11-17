using System;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Serializable]
    [Table("Menu_Topping")]
    public class CsMenuTopping : EntityBase
    {
        [PrimaryKey("Topping_Id")]
        public decimal ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Position")]
        public int Position { get; set; }

        [Column("CategoryID")]
        public int CategoryID { get; set; }

        public virtual CsToppingCategory ToppingCategory { get; set; }
    }
}
