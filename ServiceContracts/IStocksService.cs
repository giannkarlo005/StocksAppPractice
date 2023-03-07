using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface IStocksService
    {
        BuyOrderResponse CreateBuyOrder(BuyOrderRequest buyOrderRequest);
        SellOrderResponse CreateSellOrder(SellOrderRequest sellOrderRequest);
        List<BuyOrderResponse> GetAllBuyOrders();
        List<SellOrderResponse> GetAllSellOrders();
    }
}
