using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("ToppingCategory")]
    public class CsToppingCategory : EntityBase
    {
        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Position")]
        public int Position { get; set; }

        [Column("Remark")]
        public string Remark { get; set; }

        public virtual List<CsMenuTopping> MenuToppingList { get; set; }
    }
}
