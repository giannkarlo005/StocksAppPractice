using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface ICreateStockOrdersService
    {
        OrderResponse CreateBuyOrder(OrderRequest orderRequest);
        OrderResponse CreateSellOrder(OrderRequest orderRequest);
    }
}
