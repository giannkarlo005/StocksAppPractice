using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IGetStockOrdersService
    {
        List<OrderResponse> GetAllBuyOrders();
        List<OrderResponse> GetAllSellOrders();
    }
}
