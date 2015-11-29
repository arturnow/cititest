namespace DigiCoinService
{
    public interface IBrokerageService
    {
        void RegisterBroker(IBroker broker);
        decimal PlaceOrder(int numberOfCoinsOrderd);
    }
}