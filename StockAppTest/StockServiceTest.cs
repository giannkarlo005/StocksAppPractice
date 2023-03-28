using Moq;
using AutoFixture;
using FluentAssertions;

using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace StockAppTest
{
    public class StockServiceTest
    {
        private readonly IStocksService _stocksService;

        private readonly Mock<IStocksService> _stocksServiceMock;

        private readonly Fixture _fixture;

        public StockServiceTest()
        {
            _fixture = new Fixture();

            _stocksServiceMock = new Mock<IStocksService>();
            _stocksService = _stocksServiceMock.Object;
        }

        #region BuyOrder
        //When supplied BuyOrderRequest is null
        [Fact]
        public void BuyOrder_NullOrderRequest()
        {
            BuyOrderRequest buyOrderRequest = null;

            Func<Task> action = async () =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            };
            action.Should().ThrowAsync<ArgumentNullException>();
        }

        //When supplied BuyOrderQuantity is 0
        [Fact]
        public void BuyOrder_OrderQuantityIs0()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                              .With(request => request.OrderQuantity, 0)
                                              .With(request => request.OrderPrice, 100)
                                              .Create();

            Func<Task> action = async () =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
        }

        //When BuyOrderQuantity is greater than 10000
        [Fact]
        public void BuyOrder_OrderQuantityIsGreaterThan10000()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                              .With(request => request.OrderQuantity, 10001)
                                              .With(request => request.OrderPrice, 100)
                                              .Create();

            Func<Task> action = async () =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
        }

        //When BuyOrderPrice is greater than 10000
        [Fact]
        public void BuyOrder_OrderPriceIsGreaterThan10000()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                              .With(request => request.OrderQuantity, 100)
                                              .With(request => request.OrderPrice, 10001)
                                              .Create();

            Func<Task> action = async () =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
        }

        //When StockSymbol is null
        [Fact]
        public void BuyOrder_StockSymbolIsNull()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                  .With(request => request.StockSymbol, null as string)
                                  .With(request => request.OrderQuantity, 100)
                                  .With(request => request.OrderPrice, 100)
                                  .Create();

            Func<Task> action = async () =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
        }

        //When supplied DateAndTimeOfOrder is same or later than 2000-01-01
        [Fact]
        public void BuyOrder_DateAndTimeOfOrderIsSameOrLaterThan20010101()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                                              .With(request => request.StockSymbol, "MSFT")
                                              .With(request => request.OrderQuantity, 100)
                                              .With(request => request.OrderPrice, 100)
                                              .With(request => request.DateAndTimeOfOrder, DateTime.Parse("1999-12-31"))
                                              .Create();
            Func<Task> action = async () =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            action.Should().ThrowAsync<ArgumentException>();
        }

        //When All values supplied are valid
        [Fact]
        public void BuyOrder_AllValuesAreValid()
        {
            BuyOrderRequest buyOrderRequest = _fixture.Create<BuyOrderRequest>();
            BuyOrderResponse buyOrderResponseExpected = _fixture.Create<BuyOrderResponse>();

            _stocksServiceMock.Setup(mock => mock.CreateBuyOrder(It.IsAny<BuyOrderRequest>()))
                              .Returns(buyOrderResponseExpected);

            BuyOrderResponse buyOrderResponseActual = _stocksService.CreateBuyOrder(buyOrderRequest);
            buyOrderResponseActual.Should().BeEquivalentTo(buyOrderResponseExpected);
        }
        #endregion

        #region GetAllBuyOrders
        //If invoked by default, returned list should be empty
        [Fact]
        public void GetAllBuyOrders_DefaultResponse()
        {
            _stocksServiceMock.Setup(mock => mock.GetAllBuyOrders())
                              .Returns(new List<BuyOrderResponse>());

            List<BuyOrderResponse> buyOrders = _stocksService.GetAllBuyOrders();

            buyOrders.Should().BeEmpty();
        }

        //When supplied SellOrderQuantity is 0
        [Fact]
        public void GetAllBuyOrders_AddFewBuyOrders()
        {
            BuyOrderRequest buyOrderRequest1 = _fixture.Build<BuyOrderRequest>()
                                              .With(request => request.OrderQuantity, 100)
                                              .With(request => request.OrderPrice, 100)
                                              .With(request => request.DateAndTimeOfOrder, DateTime.Parse("2010-01-01"))
                                              .Create();

            BuyOrderRequest buyOrderRequest2 = _fixture.Build<BuyOrderRequest>()
                                              .With(request => request.OrderQuantity, 50)
                                              .With(request => request.OrderPrice, 200)
                                              .With(request => request.DateAndTimeOfOrder, DateTime.Parse("2010-02-15"))
                                              .Create();

            BuyOrderRequest buyOrderRequest3 = _fixture.Build<BuyOrderRequest>()
                                              .With(request => request.OrderQuantity, 70)
                                              .With(request => request.OrderPrice, 150)
                                              .With(request => request.DateAndTimeOfOrder, DateTime.Parse("2013-06-10"))
                                              .Create();


            BuyOrderResponse buyOrderResponseExpected = _fixture.Create<BuyOrderResponse>();


            _stocksServiceMock.Setup(mock => mock.CreateBuyOrder(It.IsAny<BuyOrderRequest>()))
                              .Returns(buyOrderResponseExpected);

            BuyOrderResponse buyOrderResponse1 = _stocksService.CreateBuyOrder(buyOrderRequest1);
            BuyOrderResponse buyOrderResponse2 = _stocksService.CreateBuyOrder(buyOrderRequest2);
            BuyOrderResponse buyOrderResponse3 = _stocksService.CreateBuyOrder(buyOrderRequest3);

            _stocksServiceMock.Setup(mock => mock.GetAllBuyOrders())
                              .Returns(new List<BuyOrderResponse>()
                              {
                                  buyOrderResponse1,
                                  buyOrderResponse2,
                                  buyOrderResponse3,
                              });

            List<BuyOrderResponse> buyOrders = _stocksService.GetAllBuyOrders();

            buyOrderResponse1.OrderID.Should().NotBe(Guid.Empty);
            buyOrderResponse2.OrderID.Should().NotBe(Guid.Empty);
            buyOrderResponse3.OrderID.Should().NotBe(Guid.Empty);

            buyOrders.Should().Contain(buyOrderResponse1);
            buyOrders.Should().Contain(buyOrderResponse2);
            buyOrders.Should().Contain(buyOrderResponse3);
        }
        #endregion

        #region SellOrder
        //When supplied SellOrderRequest is null
        [Fact]
        public void SellOrder_NullOrderRequest()
        {
            SellOrderRequest sellOrderRequest = null;

            Func<Task> action = async () =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            };
            action.Should().ThrowAsync<ArgumentNullException>();
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

            Func<Task> action = async () =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
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

            Func<Task> action = async () =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
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

            Func<Task> action = async () =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
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

            Func<Task> action = async () =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
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

            Func<Task> action = async () =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
        }

        //When All values supplied are valid
        [Fact]
        public void SellOrder_AllValuesAreValid()
        {
            SellOrderRequest sellOrderRequest = _fixture.Create<SellOrderRequest>();
            SellOrderResponse sellOrderResponseExpected = _fixture.Create<SellOrderResponse>();

            _stocksServiceMock.Setup(mock => mock.CreateSellOrder(It.IsAny<SellOrderRequest>()))
                              .Returns(sellOrderResponseExpected);

            SellOrderResponse sellOrderResponseActual = _stocksService.CreateSellOrder(sellOrderRequest);
            sellOrderResponseActual.Should().BeEquivalentTo(sellOrderResponseExpected);
        }
        #endregion

        #region GetAllSellOrders
        //If invoked by default, returned list should be empty
        [Fact]
        public void GetAllSellOrders_DefaultResponse()
        {
            _stocksServiceMock.Setup(mock => mock.GetAllSellOrders())
                  .Returns(new List<SellOrderResponse>());

            List<SellOrderResponse> sellOrders = _stocksService.GetAllSellOrders();

            sellOrders.Should().BeEmpty();
        }

        //When supplied SellOrderQuantity is 0
        [Fact]
        public void GetAllSellOrders_AddFewBuyOrders()
        {
            SellOrderRequest sellOrderRequest1 = _fixture.Build<SellOrderRequest>()
                                              .With(request => request.OrderQuantity, 100)
                                              .With(request => request.OrderPrice, 100)
                                              .With(request => request.DateAndTimeOfOrder, DateTime.Parse("2010-01-01"))
                                              .Create();

            SellOrderRequest sellOrderRequest2 = _fixture.Build<SellOrderRequest>()
                                              .With(request => request.OrderQuantity, 50)
                                              .With(request => request.OrderPrice, 200)
                                              .With(request => request.DateAndTimeOfOrder, DateTime.Parse("2010-02-15"))
                                              .Create();

            SellOrderRequest sellOrderRequest3 = _fixture.Build<SellOrderRequest>()
                                              .With(request => request.OrderQuantity, 70)
                                              .With(request => request.OrderPrice, 150)
                                              .With(request => request.DateAndTimeOfOrder, DateTime.Parse("2013-06-10"))
                                              .Create();


            SellOrderResponse sellOrderResponseExpected = _fixture.Create<SellOrderResponse>();

            _stocksServiceMock.Setup(mock => mock.CreateSellOrder(It.IsAny<SellOrderRequest>()))
                              .Returns(sellOrderResponseExpected);

            SellOrderResponse sellOrderResponse1 = _stocksService.CreateSellOrder(sellOrderRequest1);
            SellOrderResponse sellOrderResponse2 = _stocksService.CreateSellOrder(sellOrderRequest2);
            SellOrderResponse sellOrderResponse3 = _stocksService.CreateSellOrder(sellOrderRequest3);

            _stocksServiceMock.Setup(mock => mock.GetAllSellOrders())
                              .Returns(new List<SellOrderResponse>()
                              {
                                  sellOrderResponse1,
                                  sellOrderResponse2,
                                  sellOrderResponse3,
                              });

            List<SellOrderResponse> sellOrders = _stocksService.GetAllSellOrders();

            sellOrderResponse1.OrderID.Should().NotBe(Guid.Empty);
            sellOrderResponse2.OrderID.Should().NotBe(Guid.Empty);
            sellOrderResponse3.OrderID.Should().NotBe(Guid.Empty);

            sellOrders.Should().Contain(sellOrderResponse1);
            sellOrders.Should().Contain(sellOrderResponse2);
            sellOrders.Should().Contain(sellOrderResponse3);

        }
        #endregion
    }
}