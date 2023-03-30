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
           OrderRequest orderRequest = null;

            Func<Task> action = async () =>
            {
                _stocksService.CreateBuyOrder(orderRequest);
            };
            action.Should().ThrowAsync<ArgumentNullException>();
        }

        //When supplied BuyOrderQuantity is 0
        [Fact]
        public void BuyOrder_OrderQuantityIs0()
        {
            OrderRequest buyOrderRequest = _fixture.Build<OrderRequest>()
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
            OrderRequest buyOrderRequest = _fixture.Build<OrderRequest>()
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
            OrderRequest buyOrderRequest = _fixture.Build<OrderRequest>()
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
            OrderRequest buyOrderRequest = _fixture.Build<OrderRequest>()
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
            OrderRequest buyOrderRequest = _fixture.Build<OrderRequest>()
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
            OrderRequest orderRequest = _fixture.Create<OrderRequest>();
            OrderResponse orderResponseExpected = _fixture.Create<OrderResponse>();

            _stocksServiceMock.Setup(mock => mock.CreateBuyOrder(It.IsAny<OrderRequest>()))
                              .Returns(orderResponseExpected);

            OrderResponse buyOrderResponseActual = _stocksService.CreateBuyOrder(orderRequest);
            buyOrderResponseActual.Should().BeEquivalentTo(orderResponseExpected);
        }
        #endregion

        #region GetAllBuyOrders
        //If invoked by default, returned list should be empty
        [Fact]
        public void GetAllBuyOrders_DefaultResponse()
        {
            _stocksServiceMock.Setup(mock => mock.GetAllBuyOrders())
                              .Returns(new List<OrderResponse>());

            List<OrderResponse> buyOrders = _stocksService.GetAllBuyOrders();

            buyOrders.Should().BeEmpty();
        }

        //When supplied SellOrderQuantity is 0
        [Fact]
        public void GetAllBuyOrders_AddFewBuyOrders()
        {
            OrderRequest orderRequest1 = _fixture.Build<OrderRequest>()
                                              .With(request => request.OrderQuantity, 100)
                                              .With(request => request.OrderPrice, 100)
                                              .With(request => request.DateAndTimeOfOrder, DateTime.Parse("2010-01-01"))
                                              .Create();

            OrderRequest orderRequest2 = _fixture.Build<OrderRequest>()
                                              .With(request => request.OrderQuantity, 50)
                                              .With(request => request.OrderPrice, 200)
                                              .With(request => request.DateAndTimeOfOrder, DateTime.Parse("2010-02-15"))
                                              .Create();

            OrderRequest orderRequest3 = _fixture.Build<OrderRequest>()
                                              .With(request => request.OrderQuantity, 70)
                                              .With(request => request.OrderPrice, 150)
                                              .With(request => request.DateAndTimeOfOrder, DateTime.Parse("2013-06-10"))
                                              .Create();


            OrderResponse buyOrderResponseExpected = _fixture.Create<OrderResponse>();


            _stocksServiceMock.Setup(mock => mock.CreateBuyOrder(It.IsAny<OrderRequest>()))
                              .Returns(buyOrderResponseExpected);

            OrderResponse orderResponse1 = _stocksService.CreateBuyOrder(orderRequest1);
            OrderResponse orderResponse2 = _stocksService.CreateBuyOrder(orderRequest2);
            OrderResponse orderResponse3 = _stocksService.CreateBuyOrder(orderRequest3);

            _stocksServiceMock.Setup(mock => mock.GetAllBuyOrders())
                              .Returns(new List<OrderResponse>()
                              {
                                  orderResponse1,
                                  orderResponse2,
                                  orderResponse3,
                              });

            List<OrderResponse> buyOrders = _stocksService.GetAllBuyOrders();

            orderResponse1.OrderID.Should().NotBe(Guid.Empty);
            orderResponse2.OrderID.Should().NotBe(Guid.Empty);
            orderResponse3.OrderID.Should().NotBe(Guid.Empty);

            buyOrders.Should().Contain(orderResponse1);
            buyOrders.Should().Contain(orderResponse2);
            buyOrders.Should().Contain(orderResponse3);
        }
        #endregion

        #region SellOrder
        //When supplied SellOrderRequest is null
        [Fact]
        public void SellOrder_NullOrderRequest()
        {
            OrderRequest orderRequest = null;

            Func<Task> action = async () =>
            {
                _stocksService.CreateSellOrder(orderRequest);
            };
            action.Should().ThrowAsync<ArgumentNullException>();
        }

        //When supplied SellOrderQuantity is 0
        [Fact]
        public void SellOrder_OrderQuantityIs0()
        {
            OrderRequest orderRequest = new OrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 0,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Now
            };

            Func<Task> action = async () =>
            {
                _stocksService.CreateSellOrder(orderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
        }

        //When SellOrderQuantity is greater than 10000
        [Fact]
        public void SellOrder_OrderQuantityIsGreaterThan10000()
        {
            OrderRequest orderRequest = new OrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 10001,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Now
            };

            Func<Task> action = async () =>
            {
                _stocksService.CreateSellOrder(orderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
        }

        //When SellOrderPrice is greater than 10000
        [Fact]
        public void SellOrder_OrderPriceIsGreaterThan10000()
        {
            OrderRequest orderRequest = new OrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 100,
                OrderPrice = 10001,
                DateAndTimeOfOrder = DateTime.Now
            };

            Func<Task> action = async () =>
            {
                _stocksService.CreateSellOrder(orderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
        }

        //When StockSymbol is null
        [Fact]
        public void SellOrder_StockSymbolIsNull()
        {
            OrderRequest orderRequest = new OrderRequest()
            {
                StockSymbol = null,
                OrderQuantity = 100,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Now
            };

            Func<Task> action = async () =>
            {
                _stocksService.CreateSellOrder(orderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
        }

        //When supplied DateAndTimeOfOrder is same or later than 2000-01-01
        [Fact]
        public void SellOrder_DateAndTimeOfOrderIsSameOrLaterThan20010101()
        {
            OrderRequest orderRequest = new OrderRequest()
            {
                StockSymbol = "MSFT",
                OrderQuantity = 100,
                OrderPrice = 100,
                DateAndTimeOfOrder = DateTime.Parse("1999-12-31")
            };

            Func<Task> action = async () =>
            {
                _stocksService.CreateSellOrder(orderRequest);
            };
            action.Should().ThrowAsync<ArgumentException>();
        }

        //When All values supplied are valid
        [Fact]
        public void SellOrder_AllValuesAreValid()
        {
            OrderRequest sellOrderRequest = _fixture.Create<OrderRequest>();
            OrderResponse sellOrderResponseExpected = _fixture.Create<OrderResponse>();

            _stocksServiceMock.Setup(mock => mock.CreateSellOrder(It.IsAny<OrderRequest>()))
                              .Returns(sellOrderResponseExpected);

            OrderResponse sellOrderResponseActual = _stocksService.CreateSellOrder(sellOrderRequest);
            sellOrderResponseActual.Should().BeEquivalentTo(sellOrderResponseExpected);
        }
        #endregion

        #region GetAllSellOrders
        //If invoked by default, returned list should be empty
        [Fact]
        public void GetAllSellOrders_DefaultResponse()
        {
            _stocksServiceMock.Setup(mock => mock.GetAllSellOrders())
                  .Returns(new List<OrderResponse>());

            List<OrderResponse> sellOrders = _stocksService.GetAllSellOrders();

            sellOrders.Should().BeEmpty();
        }

        //When supplied SellOrderQuantity is 0
        [Fact]
        public void GetAllSellOrders_AddFewBuyOrders()
        {
            OrderRequest orderRequest1 = _fixture.Build<OrderRequest>()
                                              .With(request => request.OrderQuantity, 100)
                                              .With(request => request.OrderPrice, 100)
                                              .With(request => request.DateAndTimeOfOrder, DateTime.Parse("2010-01-01"))
                                              .Create();

            OrderRequest orderRequest2 = _fixture.Build<OrderRequest>()
                                              .With(request => request.OrderQuantity, 50)
                                              .With(request => request.OrderPrice, 200)
                                              .With(request => request.DateAndTimeOfOrder, DateTime.Parse("2010-02-15"))
                                              .Create();

            OrderRequest orderRequest3 = _fixture.Build<OrderRequest>()
                                              .With(request => request.OrderQuantity, 70)
                                              .With(request => request.OrderPrice, 150)
                                              .With(request => request.DateAndTimeOfOrder, DateTime.Parse("2013-06-10"))
                                              .Create();


            OrderResponse sellOrderResponseExpected = _fixture.Create<OrderResponse>();

            _stocksServiceMock.Setup(mock => mock.CreateSellOrder(It.IsAny<OrderRequest>()))
                              .Returns(sellOrderResponseExpected);

            OrderResponse sellOrderResponse1 = _stocksService.CreateSellOrder(orderRequest1);
            OrderResponse sellOrderResponse2 = _stocksService.CreateSellOrder(orderRequest2);
            OrderResponse sellOrderResponse3 = _stocksService.CreateSellOrder(orderRequest3);

            _stocksServiceMock.Setup(mock => mock.GetAllSellOrders())
                              .Returns(new List<OrderResponse>()
                              {
                                  sellOrderResponse1,
                                  sellOrderResponse2,
                                  sellOrderResponse3,
                              });

            List<OrderResponse> sellOrders = _stocksService.GetAllSellOrders();

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