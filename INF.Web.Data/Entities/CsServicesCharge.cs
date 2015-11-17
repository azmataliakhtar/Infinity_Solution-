using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("ServicesCharge")]
    public class CsServicesCharge:EntityBase
    {
        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Charge")]
        public double Charge { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("IsActived")]
        public bool IsActived { get; set; }

        [Column("ChargeOnOrder")]
        public bool ChargeOnOrder { get; set; }
    }
}
