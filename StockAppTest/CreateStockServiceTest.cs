using AutoFixture;
using FluentAssertions;
using Moq;

using StocksAppAssignment.Core.DTO;
using StocksAppAssignment.Core.ServiceContracts;

namespace StockAppTest
{
    public class CreateStockServiceTest
    {
        private readonly ICreateStockOrdersService _stocksService;
        private readonly Mock<ICreateStockOrdersService> _stocksServiceMock;
        private readonly Fixture _fixture;

        public CreateStockServiceTest()
        {
            _fixture = new Fixture();

            _stocksServiceMock = new Mock<ICreateStockOrdersService>();
            _stocksService = _stocksServiceMock.Object;
        }

        #region BuyOrder
        //When supplied BuyOrderRequest is null
        [Fact]
        public void BuyOrder_NullOrderRequest()
        {
            OrderRequest? orderRequest = null;

            Func<Task> action = () =>
            {
                _stocksService.CreateBuyOrder(orderRequest);
                return Task.CompletedTask;
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

            Func<Task> action = () =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
                return Task.CompletedTask;
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

            Func<Task> action = () =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
                return Task.CompletedTask;
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

            Func<Task> action = () =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
                return Task.CompletedTask;
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

            Func<Task> action = () =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
                return Task.CompletedTask;
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
            Func<Task> action = () =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
                return Task.CompletedTask;
            };

            action.Should().ThrowAsync<ArgumentException>();
        }

        //When All values supplied are valid
        [Fact]
        public async void BuyOrder_AllValuesAreValid()
        {
            OrderRequest orderRequest = _fixture.Create<OrderRequest>();
            OrderResponse orderResponseExpected = _fixture.Create<OrderResponse>();

            _stocksServiceMock.Setup(mock => mock.CreateBuyOrder(It.IsAny<OrderRequest>()))
                              .ReturnsAsync(orderResponseExpected);

            OrderResponse buyOrderResponseActual = await _stocksService.CreateBuyOrder(orderRequest);
            buyOrderResponseActual.Should().BeEquivalentTo(orderResponseExpected);
        }
        #endregion

        #region SellOrder
        //When supplied SellOrderRequest is null
        [Fact]
        public void SellOrder_NullOrderRequest()
        {
            OrderRequest? orderRequest = null;

            Func<Task> action = () =>
            {
                _stocksService.CreateSellOrder(orderRequest);
                return Task.CompletedTask;
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

            Func<Task> action = () =>
            {
                _stocksService.CreateSellOrder(orderRequest);
                return Task.CompletedTask;
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

            Func<Task> action = () =>
            {
                _stocksService.CreateSellOrder(orderRequest);
                return Task.CompletedTask;
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

            Func<Task> action = () =>
            {
                _stocksService.CreateSellOrder(orderRequest);
                return Task.CompletedTask;
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

            Func<Task> action = () =>
            {
                _stocksService.CreateSellOrder(orderRequest);
                return Task.CompletedTask;
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

            Func<Task> action = () =>
            {
                _stocksService.CreateSellOrder(orderRequest);
                return Task.CompletedTask;
            };
            action.Should().ThrowAsync<ArgumentException>();
        }

        //When All values supplied are valid
        [Fact]
        public async void SellOrder_AllValuesAreValid()
        {
            OrderRequest sellOrderRequest = _fixture.Create<OrderRequest>();
            OrderResponse sellOrderResponseExpected = _fixture.Create<OrderResponse>();

            _stocksServiceMock.Setup(mock => mock.CreateSellOrder(It.IsAny<OrderRequest>()))
                              .ReturnsAsync(sellOrderResponseExpected);

            OrderResponse sellOrderResponseActual = await _stocksService.CreateSellOrder(sellOrderRequest);
            sellOrderResponseActual.Should().BeEquivalentTo(sellOrderResponseExpected);
        }
        #endregion
    }
}
