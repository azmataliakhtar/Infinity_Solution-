using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("Restaurant_Timing")]
    public class CsRestaurantTiming : EntityBase
    {
        [PrimaryKey("Time_Id")]
        public decimal ID { get; set; }

        [Column("Day")]
        public int DayInWeek { get; set; }

        [Column("Opening_Time")]
        public string OpeningTime { get; set; }

        [Column("Closing_Time")]
        public string ClosingTime { get; set; }

        [Column("Remarks")]
        public string Remarks { get; set; }

        [Column("DiscountPercent")]
        public int DiscountPercent { get; set; }

        [Column("DiscountOver")]
        public decimal DiscountOver { get; set; }

        [Column("DiscountValue")]
        public decimal DiscountValue { get; set; }

        [Column("OfferText")]
        public string OfferText { get; set; }
    }
}
