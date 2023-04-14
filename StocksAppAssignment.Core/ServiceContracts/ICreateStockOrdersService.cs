using StocksAppAssignment.Core.DTO;

namespace StocksAppAssignment.Core.ServiceContracts
{
    public interface ICreateStockOrdersService
    {
        Task<OrderResponse> CreateBuyOrder(OrderRequest? orderRequest);
        Task<OrderResponse> CreateSellOrder(OrderRequest? orderRequest);
    }
}
