using System;
using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("Customer")]
    public class CsCustomer : EntityBase
    {
        [PrimaryKey("Customer_Id")]
        public decimal ID { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("First_Name")]
        public string FirstName { get; set; }

        [Column("Last_Name")]
        public string LastName { get; set; }

        [Column("Telephone")]
        public string Telephone { get; set; }

        [Column("Mobile")]
        public string Mobile { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("PasswordHint")]
        public string PasswordHint { get; set; }

        [Column("Image_Location")]
        public string ImageUrl { get; set; }

        [Column("Points")]
        public int Points { get; set; }

        [Column("Rating")]
        public int Rating { get; set; }

        [Column("Credit_Customer")]
        public bool IsCreditCustomer { get; set; }

        [Column("CreditLimit")]
        public decimal CreditLimit { get; set; }

        [Column("Current_Credit")]
        public decimal CurrentCredit { get; set; }

        [Column("Invoice_Period")]
        public int InvoicePeriod { get; set; }

        [Column("Active")]
        public bool IsActive { get; set; }

        [Column("Member_Since")]
        public DateTime MemberSince { get; set; }

        [Column("Remarks")]
        public string Remarks { get; set; }
    }
}
