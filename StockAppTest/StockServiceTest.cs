using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace StockAppTest
{
    public class StockServiceTest
    {
        private readonly IStocksService _stocksService;

        public StockServiceTest()
        {
            _stocksService = new StocksService(null);
        }

        #region BuyOrder
        //When supplied BuyOrderRequest is null
        [Fact]
        public void BuyOrder_NullOrderRequest()
        {
            BuyOrderRequest buyOrderRequest = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        //When supplied BuyOrderQuantity is 0
        [Fact]
        public void BuyOrder_OrderQuantityIs0()
        {
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 0,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Now
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        //When BuyOrderQuantity is greater than 10000
        [Fact]
        public void BuyOrder_OrderQuantityIsGreaterThan10000()
        {
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 10001,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Now
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        //When BuyOrderPrice is greater than 10000
        [Fact]
        public void BuyOrder_OrderPriceIsGreaterThan10000()
        {
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 100,
                OrderPrice = 10001,
                DateAndTimeOfOrder = DateTime.Now
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        //When StockSymbol is null
        [Fact]
        public void BuyOrder_StockSymbolIsNull()
        {
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = null,
                OrderQuantity = 100,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Now
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        //When supplied DateAndTimeOfOrder is same or later than 2000-01-01
        [Fact]
        public void BuyOrder_DateAndTimeOfOrderIsSameOrLaterThan20010101()
        {
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 100,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Parse("1999-12-31")
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        //When All values supplied are valid
        [Fact]
        public void BuyOrder_AllValuesAreValid()
        {
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 100,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Parse("2010-01-01")
            };

            BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(buyOrderRequest);
            Assert.NotNull(buyOrderResponse.OrderID);
        }
        #endregion

        #region GetAllBuyOrders
        //If invoked by default, returned list should be empty
        [Fact]
        public void GetAllBuyOrders_DefaultResponse()
        {
            List<BuyOrderResponse> buyOrders = _stocksService.GetAllBuyOrders();

            Assert.Empty(buyOrders);
        }

        //When supplied SellOrderQuantity is 0
        [Fact]
        public void GetAllBuyOrders_AddFewBuyOrders()
        {
            BuyOrderRequest buyOrderRequest1 = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 100,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Parse("2010-01-01")
            };

            BuyOrderRequest buyOrderRequest2 = new BuyOrderRequest()
            {
                StockSymbol = "APPL",
                OrderQuantity = 50,
                OrderPrice = 200,
                DateAndTimeOfOrder = DateTime.Parse("2010-02-15")
            };

            BuyOrderRequest buyOrderRequest3 = new BuyOrderRequest()
            {
                StockSymbol = "AMZN",
                OrderQuantity = 70,
                OrderPrice = 150,
                DateAndTimeOfOrder = DateTime.Parse("2013-06-10")
            };

            BuyOrderResponse buyOrderResponse1 = _stocksService.CreateBuyOrder(buyOrderRequest1);
            BuyOrderResponse buyOrderResponse2 = _stocksService.CreateBuyOrder(buyOrderRequest2);
            BuyOrderResponse buyOrderResponse3 = _stocksService.CreateBuyOrder(buyOrderRequest3);

            List<BuyOrderResponse> buyOrders = _stocksService.GetAllBuyOrders();

            Assert.True(buyOrderResponse1.OrderID != Guid.Empty);
            Assert.True(buyOrderResponse2.OrderID != Guid.Empty);
            Assert.True(buyOrderResponse3.OrderID != Guid.Empty);

            Assert.Contains(buyOrderResponse1, buyOrders);
            Assert.Contains(buyOrderResponse2, buyOrders);
            Assert.Contains(buyOrderResponse3, buyOrders);
        }
        #endregion

        #region SellOrder
        //When supplied SellOrderRequest is null
        [Fact]
        public void SellOrder_NullOrderRequest()
        {
            SellOrderRequest sellOrderRequest = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        //When supplied SellOrderQuantity is 0
        [Fact]
        public void SellOrder_OrderQuantityIs0()
        {
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 0,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Now
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        //When SellOrderQuantity is greater than 10000
        [Fact]
        public void SellOrder_OrderQuantityIsGreaterThan10000()
        {
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 10001,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Now
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        //When SellOrderPrice is greater than 10000
        [Fact]
        public void SellOrder_OrderPriceIsGreaterThan10000()
        {
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 100,
                OrderPrice = 10001,
                DateAndTimeOfOrder = DateTime.Now
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        //When StockSymbol is null
        [Fact]
        public void SellOrder_StockSymbolIsNull()
        {
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = null,
                OrderQuantity = 100,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Now
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        //When supplied DateAndTimeOfOrder is same or later than 2000-01-01
        [Fact]
        public void SellOrder_DateAndTimeOfOrderIsSameOrLaterThan20010101()
        {
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 100,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Parse("1999-12-31")
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        //When All values supplied are valid
        [Fact]
        public void SellOrder_AllValuesAreValid()
        {
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 100,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Parse("2010-01-01")
            };

            SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(sellOrderRequest);
            Assert.NotNull(sellOrderResponse.OrderID);
        }
        #endregion

        #region GetAllSellOrders
        //If invoked by default, returned list should be empty
        [Fact]
        public void GetAllSellOrders_DefaultResponse()
        {
            List<SellOrderResponse> sellOrders = _stocksService.GetAllSellOrders();

            Assert.Empty(sellOrders);
        }

        //When supplied SellOrderQuantity is 0
        [Fact]
        public void GetAllSellOrders_AddFewBuyOrders()
        {
            SellOrderRequest sellOrderRequest1 = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 100,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Parse("2010-01-01")
            };

            SellOrderRequest sellOrderRequest2 = new SellOrderRequest()
            {
                StockSymbol = "APPL",
                OrderQuantity = 50,
                OrderPrice = 200,
                DateAndTimeOfOrder = DateTime.Parse("2010-02-15")
            };

            SellOrderRequest sellOrderRequest3 = new SellOrderRequest()
            {
                StockSymbol = "AMZN",
                OrderQuantity = 70,
                OrderPrice = 150,
                DateAndTimeOfOrder = DateTime.Parse("2013-06-10")
            };

            SellOrderResponse sellOrderResponse1 = _stocksService.CreateSellOrder(sellOrderRequest1);
            SellOrderResponse sellOrderResponse2 = _stocksService.CreateSellOrder(sellOrderRequest2);
            SellOrderResponse sellOrderResponse3 = _stocksService.CreateSellOrder(sellOrderRequest3);

            List<SellOrderResponse> sellOrders = _stocksService.GetAllSellOrders();

            Assert.True(sellOrderResponse1.OrderID != Guid.Empty);
            Assert.True(sellOrderResponse2.OrderID != Guid.Empty);
            Assert.True(sellOrderResponse3.OrderID != Guid.Empty);

            Assert.Contains(sellOrderResponse1, sellOrders);
            Assert.Contains(sellOrderResponse2, sellOrders);
            Assert.Contains(sellOrderResponse3, sellOrders);
        }
        #endregion
    }
}