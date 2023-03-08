using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class BuyOrderResponse
    {
        public Guid? OrderID { get; set; }
        public string? StockName { get; set; }
        public string? StockSymbol { get; set; }
        public double OrderQuantity { get; set; }
        public double OrderPrice { get; set; }
        public DateTime? DateAndTimeOfOrder { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(BuyOrderResponse))
            {
                return false;
            }

            BuyOrderResponse buyOrderResponse = obj as BuyOrderResponse;
            return this.OrderID == buyOrderResponse.OrderID
                   && this.StockName == buyOrderResponse.StockName
                   && this.StockSymbol == buyOrderResponse.StockSymbol
                   && this.OrderQuantity == buyOrderResponse.OrderQuantity
                   && this.OrderPrice == buyOrderResponse.OrderPrice
                   && DateTime.Compare(this.DateAndTimeOfOrder.Value, buyOrderResponse.DateAndTimeOfOrder.Value) == 0;
        }
    }
    public static class BuyOrderResponseExtensions
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse()
            {
                OrderID = buyOrder.OrderID,
                StockName = buyOrder.StockName,
                StockSymbol = buyOrder.StockSymbol,
                OrderQuantity = buyOrder.OrderQuantity,
                OrderPrice = buyOrder.OrderPrice,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder
            };
        }
    }
}
