using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("FlatFieldsName")]
    public class CsFlatFieldsName : EntityBase
    {
        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("FieldName")]
        public string FieldName { get; set; }
    }
}
