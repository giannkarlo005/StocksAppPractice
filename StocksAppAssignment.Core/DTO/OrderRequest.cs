using StocksAppAssignment.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace StocksAppAssignment.Core.DTO
{
    public class OrderRequest
    {
        [Required(ErrorMessage = "Stock Name is required")]
        public string? StockName { get; set; }
        [Required(ErrorMessage = "Stock Symbol is required")]
        public string? StockSymbol { get; set; }
        [Required(ErrorMessage = "Order Quantity is required")]
        [Range(1, 10000, ErrorMessage = "Order Quantity should be from 1 and 10000")]
        public double OrderQuantity { get; set; }
        [Required(ErrorMessage = "Order Price is required")]
        [Range(1, 10000, ErrorMessage = "Order Price should be from 1 and 10000")]
        public double OrderPrice { get; set; }
        public DateTime? DateAndTimeOfOrder { get; set; } = DateTime.Now;

        public BuyOrder ToBuyOrder()
        {
            return new BuyOrder()
            {
                StockName = StockName,
                StockSymbol = StockSymbol,
                OrderQuantity = OrderQuantity,
                OrderPrice = OrderPrice,
                DateAndTimeOfOrder = DateAndTimeOfOrder
            };
        }

        public SellOrder ToSellOrder()
        {
            return new SellOrder()
            {
                StockName = StockName,
                StockSymbol = StockSymbol,
                OrderQuantity = OrderQuantity,
                OrderPrice = OrderPrice,
                DateAndTimeOfOrder = DateAndTimeOfOrder
            };
        }

        public override string ToString()
        {
            return $"Stock Name: {StockName}," +
                   $"Stock Symbol: {StockSymbol}," +
                   $"Order Quantity: {OrderQuantity}," +
                   $"Order Price: {OrderPrice}," +
                   $"Date and Time of Order: {DateAndTimeOfOrder}";
        }
    }
}
