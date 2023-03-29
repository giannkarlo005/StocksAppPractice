using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class BuyOrderRequest
    {
        [Required(ErrorMessage = "Stock Name is required")]
        public string? StockName { get; set; }
        [Required(ErrorMessage = "Stock Symbol is required")]
        public string? StockSymbol { get; set; }
        [Required(ErrorMessage = "Sell Order Quantity is required")]
        public double OrderQuantity { get; set; }
        [Required(ErrorMessage = "Sell Order Price is required")]
        public double OrderPrice { get; set; }
        [Required(ErrorMessage = "Order Date and Time is required")]
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
