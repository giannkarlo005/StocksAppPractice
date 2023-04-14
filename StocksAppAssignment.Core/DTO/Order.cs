using System.ComponentModel.DataAnnotations;

namespace StocksAppAssignment.Core.DTO
{
    public class Order
    {
        [Key]
        public Guid? OrderID { get; set; }
        [StringLength(100)]
        public string? StockName { get; set; }
        [StringLength(15)]
        public string? StockSymbol { get; set; }
        public double OrderQuantity { get; set; }
        public double OrderPrice { get; set; }
        public DateTime? DateAndTimeOfOrder { get; set; }

        public override string ToString()
        {
            return $"Order ID: {OrderID}," +
                   $"Stock Name: {StockName}," +
                   $"Stock Symbol: {StockSymbol}," +
                   $"Order Quantity: {OrderQuantity}," +
                   $"Order Price: {OrderPrice}," +
                   $"Date and Time of Order: {DateAndTimeOfOrder}";
        }
    }

}
