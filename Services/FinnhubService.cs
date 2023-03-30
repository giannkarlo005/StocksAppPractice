using Entities;
using ServiceContracts;

using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        private string _finnhubURL = "";
        private string _finnhubToken = "";

        public FinnhubService() 
        {
        }

        public void SetFinnhubUrlToken(string finnhubURL, string finnhubToken)
        {
            _finnhubURL = finnhubURL;
            _finnhubToken = finnhubToken;
        }

        private async Task<HttpResponseMessage> GetAsyncUrl(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);

            return response;
        }

        public async Task<List<USExchange>> GetAllStocks()
        {
            string url = _finnhubURL + "/stock/symbol?exchange=US&token=" + _finnhubToken;
            HttpResponseMessage response = GetAsyncUrl(url).Result;
            List<USExchange> usExchange = await response.Content.ReadFromJsonAsync<List<USExchange>>();

            return usExchange;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string companySymbol)
        {
            string url = _finnhubURL + "/stock/profile2?symbol=" + companySymbol + "&token=" + _finnhubToken;
            HttpResponseMessage response = GetAsyncUrl(url).Result;
            Dictionary<string, object>? companyProfileDict = await response.Content.ReadFromJsonAsync<Dictionary<string, object>?>();

            return companyProfileDict;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string companySymbol)
        {
            string url = _finnhubURL + "/quote?symbol=" + companySymbol + "&token=" + _finnhubToken;
            HttpResponseMessage response = GetAsyncUrl(url).Result;
            Dictionary<string, object>? stockPriceQuote = await response.Content.ReadFromJsonAsync<Dictionary<string, object>?>();

            return stockPriceQuote;
        }
    }
}