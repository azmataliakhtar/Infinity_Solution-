using INF.Database.Metadata;

namespace INF.Web.Data.Entities
{
    [Table("Delivery_Timing")]
    public class CsDeliveryTiming:EntityBase
    {
        [PrimaryKey("DeliveryTime_Id")]
        public decimal ID { get; set; }

        [Column("Delivery_Day")]
        public int DayInWeek { get; set; }

        [Column("Start_Time")]
        public string StartTime { get; set; }

        [Column("End_Time")]
        public string EndTime { get; set; }

        [Column("Remarks")]
        public string Remarks { get; set; }

        [Column("Discountpercent")]
        public int DiscountPercent { get; set; }

        [Column("DiscountOver")]
        public decimal DiscountOver { get; set; }

        [Column("DiscountValue")]
        public decimal DiscountValue { get; set; }

        [Column("OfferText")]
        public string OfferText { get; set; }
    }
}
