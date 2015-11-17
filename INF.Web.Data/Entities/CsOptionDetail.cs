using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("OptionDetail")]
    public class CsOptionDetail:EntityBase
    {
        public CsOptionDetail():this(0){}

        public CsOptionDetail(int id)
        {
            this.ID = id;
        }

        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Position")]
        public int Position { get; set; }

        [Column("UnitPrice")]
        public decimal UnitPrice { get; set; }

        [Column("OptionID")]
        public int OptionID { get; set; }

        [Column("IsEnabled")]
        public bool IsEnabled { get; set; }
    }
}
