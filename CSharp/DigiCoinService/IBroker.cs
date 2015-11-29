namespace DigiCoinService
{
    public interface IBroker
    {
        decimal GetCommission(int orderedCoinsAmount);
        decimal GetQuoteForTransactoin(int orderedNumber);
        void TakeOrder(int orderedNumber);
        int ReportTransactedNumber();
    }
}