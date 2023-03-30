using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IStocksService
    {
        OrderResponse CreateBuyOrder(OrderRequest orderRequest);
        OrderResponse CreateSellOrder(OrderRequest orderRequest);
        List<OrderResponse> GetAllBuyOrders();
        List<OrderResponse> GetAllSellOrders();
    }
}
