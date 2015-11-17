using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("MenuOption")]
    public class CsMenuOption : EntityBase
    {
        public CsMenuOption()
            : this(0)
        {
        }

        public CsMenuOption(int id)
        {
            this.ID = id;
        }

        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Position")]
        public int Position { get; set; }

        [Column("IsEnabled")]
        public bool IsEnabled { get; set; }

        [Column("ItemsAllowed")]
        public int ItemsAllowed { get; set; }
    }
}
