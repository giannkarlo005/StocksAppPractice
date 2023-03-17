﻿using ServiceContracts.DTO;
using System.Diagnostics;

namespace StocksAppAssignment.Models
{
    public class Orders
    {
        public List<BuyOrderResponse> BuyOrders { get; set; }
        public List<SellOrderResponse> SellOrders { get; set; }
    }

    public class OrderSummary
    {
        public DateTime? DateAndTime { get; set; }
        public string? Stock { get; set; }
        public string? OrderType { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string TradeAmount { get; set; }
    }
}
