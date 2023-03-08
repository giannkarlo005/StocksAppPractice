namespace Entities
{
    public class BuyOrder
    {
        public Guid? OrderID { get; set; }
        public string? StockName { get; set; }
        public string? StockSymbol { get; set; }
        public double OrderQuantity { get; set; }
        public double OrderPrice { get; set; }
        public DateTime? DateAndTimeOfOrder { get; set; }
    }
}
