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
            //string url = _finnhubURL + "/stock/symbol?exchange=US&token=" + _finnhubToken;
            //HttpResponseMessage response = GetAsyncUrl(url).Result;
            //List<USExchange> usExchange = await response.Content.ReadFromJsonAsync<List<USExchange>>();

            List<USExchange> usExchange = new List<USExchange>()
            {
                new USExchange()
                {
                    Currency = "US",
                    Description = "Apple",
                    DisplaySymbol = "AAPL",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "AAPL",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Microsoft",
                    DisplaySymbol = "MSFT",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "MSFT",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Amazon",
                    DisplaySymbol = "AMZN",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "AMZN",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Tesla",
                    DisplaySymbol = "TSLA",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "TSLA",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Alphabet Inc - A",
                    DisplaySymbol = "GOOGL",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "GOOGL",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Alphabet Inc - C",
                    DisplaySymbol = "GOOG",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "GOOG",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Nvidia",
                    DisplaySymbol = "NVDA",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "NVDA",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Berkshire Hathaway Inc Class B",
                    DisplaySymbol = "BRK.B",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "BRK.B",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Meta",
                    DisplaySymbol = "META",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "META",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "UnitedHealth Group Inc",
                    DisplaySymbol = "UNH",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "UNH",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Johnson & Johnson",
                    DisplaySymbol = "JNJ",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "JNJ",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "JPMorgan Chase & Co",
                    DisplaySymbol = "JPM,V",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "JPM,V",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Procter & Gamble Co",
                    DisplaySymbol = "PG",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "PG",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Exxon Mobil Corp",
                    DisplaySymbol = "XOM",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "XOM",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Home Depot Inc",
                    DisplaySymbol = "HD",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "HD",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Chevron Corporation",
                    DisplaySymbol = "CVX",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "CVX",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Mastercard Inc",
                    DisplaySymbol = "MA",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "MA",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Bank of America Corp",
                    DisplaySymbol = "BAC",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "BAC",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "AbbVie Inc",
                    DisplaySymbol = "ABBV",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "ABBV",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Pfizer Inc.",
                    DisplaySymbol = "PFE",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "PFE",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Broadcom Inc",
                    DisplaySymbol = "AVGO",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "AVGO",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Costco Wholesale Corporation",
                    DisplaySymbol = "COST",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "COST",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "DIS",
                    DisplaySymbol = "DIS",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "DIS",
                    Symbol2  = null,
                    Type = null
                },
                new USExchange()
                {
                    Currency = "US",
                    Description = "Coca-Cola Co",
                    DisplaySymbol = "KO",
                    FIGI = null,
                    ISIN = null,
                    MIC = null,
                    ShareClassFIGI = null,
                    Symbol = "KO",
                    Symbol2  = null,
                    Type = null
                }
            };

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