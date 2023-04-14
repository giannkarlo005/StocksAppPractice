using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoFixture;
using FluentAssertions;
using Moq;

using StocksAppAssignment.Core.DTO;
using StocksAppAssignment.Core.ServiceContracts;

namespace StockAppTest
{
    public class GetStockServiceTest
    {
        private readonly ICreateStockOrdersService _createStocksService;
        private readonly IGetStockOrdersService _getStocksService;

        private readonly Mock<ICreateStockOrdersService> _createStocksServiceMock;
        private readonly Mock<IGetStockOrdersService> _getStocksServiceMock;

        private readonly Fixture _fixture;

        public GetStockServiceTest()
        {
            _fixture = new Fixture();

            _createStocksServiceMock = new Mock<ICreateStockOrdersService>();
            _createStocksService = _createStocksServiceMock.Object;

            _getStocksServiceMock = new Mock<IGetStockOrdersService>();
            _getStocksService = _getStocksServiceMock.Object;
        }

        #region GetAllBuyOrders
        //If invoked by default, returned list should be empty
        [Fact]
        public async void GetAllBuyOrders_DefaultResponse()
        {
            _getStocksServiceMock.Setup(mock => mock.GetAllBuyOrders())
                                 .ReturnsAsync(new List<OrderResponse>());

            List<OrderResponse> buyOrders = await _getStocksService.GetAllBuyOrders();

            buyOrders.Should().BeEmpty();
        }

        //When supplied SellOrderQuantity is 0
        [Fact]
        public async void GetAllBuyOrders_AddFewBuyOrders()
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


            _createStocksServiceMock.Setup(mock => mock.CreateBuyOrder(It.IsAny<OrderRequest>()))
                                    .ReturnsAsync(buyOrderResponseExpected);

            OrderResponse orderResponse1 = await _createStocksService.CreateBuyOrder(orderRequest1);
            OrderResponse orderResponse2 = await _createStocksService.CreateBuyOrder(orderRequest2);
            OrderResponse orderResponse3 = await _createStocksService.CreateBuyOrder(orderRequest3);

            _getStocksServiceMock.Setup(mock => mock.GetAllBuyOrders())
                                 .ReturnsAsync(new List<OrderResponse>()
                                 {
                                     orderResponse1,
                                     orderResponse2,
                                     orderResponse3,
                                 });

            List<OrderResponse> buyOrders = await _getStocksService.GetAllBuyOrders();

            orderResponse1.OrderID.Should().NotBe(Guid.Empty);
            orderResponse2.OrderID.Should().NotBe(Guid.Empty);
            orderResponse3.OrderID.Should().NotBe(Guid.Empty);

            buyOrders.Should().Contain(orderResponse1);
            buyOrders.Should().Contain(orderResponse2);
            buyOrders.Should().Contain(orderResponse3);
        }
        #endregion

        #region GetAllSellOrders
        //If invoked by default, returned list should be empty
        [Fact]
        public async void GetAllSellOrders_DefaultResponse()
        {
            _getStocksServiceMock.Setup(mock => mock.GetAllSellOrders())
                                 .ReturnsAsync(new List<OrderResponse>());

            List<OrderResponse> sellOrders = await _getStocksService.GetAllSellOrders();

            sellOrders.Should().BeEmpty();
        }

        //When supplied SellOrderQuantity is 0
        [Fact]
        public async void GetAllSellOrders_AddFewBuyOrders()
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

            _createStocksServiceMock.Setup(mock => mock.CreateSellOrder(It.IsAny<OrderRequest>()))
                                    .ReturnsAsync(sellOrderResponseExpected);

            OrderResponse? sellOrderResponse1 = await _createStocksService.CreateSellOrder(orderRequest1);
            OrderResponse? sellOrderResponse2 = await _createStocksService.CreateSellOrder(orderRequest2);
            OrderResponse? sellOrderResponse3 = await _createStocksService.CreateSellOrder(orderRequest3);

            _getStocksServiceMock.Setup(mock => mock.GetAllSellOrders())
                                 .ReturnsAsync(new List<OrderResponse>()
                                 {
                                     sellOrderResponse1,
                                     sellOrderResponse2,
                                     sellOrderResponse3,
                                 });

            List<OrderResponse> sellOrders = await _getStocksService.GetAllSellOrders();

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
