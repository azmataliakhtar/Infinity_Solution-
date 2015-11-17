using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("FlatFieldsValue")]
    public class CsFlatFieldsValue : EntityBase
    {
        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("FieldName")]
        public string FieldName { get; set; }

        [Column("FieldValue")]
        public string FieldValue { get; set; }
    }
}
