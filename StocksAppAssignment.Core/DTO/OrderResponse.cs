namespace StocksAppAssignment.Core.DTO
{
    public class OrderResponse
    {
        public Guid? OrderID { get; set; }
        public string? StockName { get; set; }
        public string? StockSymbol { get; set; }
        public double OrderQuantity { get; set; }
        public double OrderPrice { get; set; }
        public DateTime? DateAndTimeOfOrder { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(OrderResponse))
            {
                return false;
            }

            OrderResponse? orderResponse = obj as OrderResponse;

            return this.OrderID == orderResponse?.OrderID
                   && this.StockName == orderResponse?.StockName
                   && this.StockSymbol == orderResponse?.StockSymbol
                   && this.OrderQuantity == orderResponse?.OrderQuantity
                   && this.OrderPrice == orderResponse?.OrderPrice
                   && DateTime.Compare(this.DateAndTimeOfOrder.Value, orderResponse.DateAndTimeOfOrder.Value) == 0;
        }
    }
    public static class OrderResponseExtensions
    {
        public static OrderResponse ToOrderResponse(this Order order)
        {
            return new OrderResponse()
            {
                OrderID = order.OrderID,
                StockName = order.StockName,
                StockSymbol = order.StockSymbol,
                OrderQuantity = order.OrderQuantity,
                OrderPrice = order.OrderPrice,
                DateAndTimeOfOrder = order.DateAndTimeOfOrder
            };
        }
    }
}
