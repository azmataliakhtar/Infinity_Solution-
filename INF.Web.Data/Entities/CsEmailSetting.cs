using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("EmailSettings")]
    public class CsEmailSetting : EntityBase
    {
        [PrimaryKey("ID")]
        public int ID { get; set; }

        [Column("Sender")]
        public string Sender { get; set; }

        [Column("Host")]
        public string Host { get; set; }

        [Column("AuthenticationUser")]
        public string AuthenticationUser { get; set; }

        [Column("AuthenticationPassword")]
        public string AuthenticationPassword { get; set; }

        [Column("ApplicationServerURL")]
        public string ApplicationServerURL { get; set; }

        [Column("FeedbackEmail")]
        public string FeedbackEmail { get; set; }

        [Column("Port")]
        public int Port { get; set; }

        [Column("Timeout")]
        public int Timeout { get; set; }

        [Column("EnableSsl")]
        public bool EnableSsl { get; set; }
    }
}
