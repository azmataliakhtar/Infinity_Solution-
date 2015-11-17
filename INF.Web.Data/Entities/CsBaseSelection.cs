using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("Base_Selection")]
    public class CsBaseSelection : EntityBase
    {
        [PrimaryKey("Base_Id")]
        public decimal ID { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Price")]
        public double Price { get; set; }

        [Column("SelectedIndex")]
        public int SelectedIndex { get; set; }

        [Column("Topping_Price")]
        public double ToppingPrice { get; set; }

        [Column("Remarks")]
        public string Remarks { get; set; }
    }
}
