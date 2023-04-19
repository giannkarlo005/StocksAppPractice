using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using FluentAssertions;
using Moq;
using Serilog;

using StocksAppAssignment.Core.DTO;
using StocksAppAssignment.Core.ServiceContracts;
using StocksAppAssignment.UI.Models;
using StocksAppAssignment.UI.Controllers.v1;

namespace StockAppTest
{
    public class StockControllerTest
    {
        private readonly IConfiguration _configuration;
        private readonly IFinnhubService _finnhubService;
        private readonly ILogger<StocksController> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IFinnhubService> _finnhubServiceMock;
        private readonly Mock<ILogger<StocksController>> _loggerMock;
        private readonly Mock<IDiagnosticContext> _diagnosticContextMock;

        public StockControllerTest() 
        {
            _configurationMock = new Mock<IConfiguration>();
            _finnhubServiceMock = new Mock<IFinnhubService>();
            _loggerMock = new Mock<ILogger<StocksController>>();
            _diagnosticContextMock = new Mock<IDiagnosticContext>();

            _configuration = _configurationMock.Object;
            _finnhubService = _finnhubServiceMock.Object;
            _logger = _loggerMock.Object;
            _diagnosticContext = _diagnosticContextMock.Object;
        }

        #region explore
        [Fact]
        public async Task Explore_NullStockSymbol_ShouldReturnExploreView()
        {
            //Arrange
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

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                { "Top25PopularStocks", "AAPL,MSFT,AMZN,TSLA,GOOGL,GOOG,NVDA,BRK.B,META,UNH,JNJ,JPM,V,PG,XOM,HD,CVX,MA,BAC,ABBV,PFE,AVGO,COST,DIS,KO" }
            };
            var configBuilder = new ConfigurationBuilder();
            var mockConfiguration = configBuilder.AddInMemoryCollection(keyValuePairs)
                                                 .Build()
                                                 .GetSection("Top25PopularStocks")
                                                 .GetChildren();

            _finnhubServiceMock.Setup(mock => mock.SetFinnhubUrlToken(It.IsAny<string>(), It.IsAny<string>()));

            _configurationMock.Setup(mock => mock.GetSection(It.IsAny<string>()).GetChildren())
                  .Returns(mockConfiguration);

            _finnhubServiceMock.Setup(mock => mock.GetAllStocks())
                                .ReturnsAsync(usExchange);

            StocksController stocksController = new StocksController(_configuration,
                                                                     _finnhubService,
                                                                     _logger,
                                                                     _diagnosticContext);



            //Act
            Task<IActionResult> result = stocksController.Explore();

            //Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<Stock>>();
        }
        #endregion
    }
}
