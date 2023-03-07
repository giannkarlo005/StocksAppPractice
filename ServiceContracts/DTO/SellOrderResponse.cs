using Entities;

namespace ServiceContracts.DTO
{
    public class SellOrderResponse
    {
        public Guid? OrderID { get; set; }
        public string? StockSymbol { get; set; }
        public double OrderQuantity { get; set; }
        public double OrderPrice { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(SellOrderResponse))
            {
                return false;
            }

            SellOrderResponse sellOrderResponse = obj as SellOrderResponse;
            return this.OrderID == sellOrderResponse.OrderID
                   && this.StockSymbol == sellOrderResponse.StockSymbol
                   && this.OrderQuantity == sellOrderResponse.OrderQuantity
                   && this.OrderPrice == sellOrderResponse.OrderPrice
                   && DateTime.Compare(this.DateAndTimeOfOrder, sellOrderResponse.DateAndTimeOfOrder) == 0;
        }
    }

    public static class SellOrderResponseExtensions
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse()
            {
                OrderID = sellOrder.OrderID,
                StockSymbol = sellOrder.StockSymbol,
                OrderQuantity = sellOrder.OrderQuantity,
                OrderPrice = sellOrder.OrderPrice,
                DateAndTimeOfOrder  = sellOrder.DateAndTimeOfOrder
            };
        }
    }
}
