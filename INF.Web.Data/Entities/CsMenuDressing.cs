using System;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Serializable]
    [Table("Menu_Dressing")]
    public class CsMenuDressing : EntityBase
    {
        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("ItemPosition")]
        public int ItemPosition { get; set; }
    }
}
