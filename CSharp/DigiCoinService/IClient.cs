namespace DigiCoinService
{
    public interface IClient
    {
        decimal MakerOrder(int orderedCoins, OrderType type);
        decimal GetOrderNetValue();
    }
}