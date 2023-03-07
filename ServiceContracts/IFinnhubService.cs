using Entities;

namespace ServiceContracts
{
    public interface IFinnhubService
    {
        void SetFinnhubUrlToken(string url, string token);
        Task<List<USExchange>> GetAllStocks();
        Task<Dictionary<string, object>?> GetCompanyProfile(string companySymbol);
        Task<Dictionary<string, object>?> GetStockPriceQuote(string companySymbol);
    }
}