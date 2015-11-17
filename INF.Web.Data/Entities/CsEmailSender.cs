using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("EmailSender")]
    public class CsEmailSender : EntityBase
    {
        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("FullName")]
        public string FullName { get; set; }
    }
}
