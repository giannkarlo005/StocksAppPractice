namespace StocksAppAssignment.Models
{
    public class StockTrade
    {
        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }
        public string? StockLogo { get; set; }

        public double Price { get; set; }

        public uint? Quantity { get; set; }
    }
}
