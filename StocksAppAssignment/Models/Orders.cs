using System.Diagnostics;

using StocksAppAssignment.Core.DTO;

namespace StocksAppAssignment.UI.Models
{
    public class Orders
    {
        public List<OrderResponse> BuyOrders { get; set; } = new List<OrderResponse>();
        public List<OrderResponse> SellOrders { get; set; } = new List<OrderResponse>();
    }

    public class OrderSummary
    {
        public DateTime? DateAndTime { get; set; }
        public string? Stock { get; set; } = string.Empty;
        public string? OrderType { get; set; } = string.Empty;
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string TradeAmount { get; set; } = string.Empty;
    }
}
