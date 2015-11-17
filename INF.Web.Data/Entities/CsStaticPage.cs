using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("StaticPage")]
    public class CsStaticPage : EntityBase
    {
        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("StaticPage")]
        public string PageName { get; set; }

        [Column("Image")]
        public string Image { get; set; }

        [Column("Abstract")]
        public string Abstract { get; set; }

        [Column("Body")]
        public string Body { get; set; }
    }
}
