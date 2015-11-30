namespace DigiCoinService
{
    public interface IBroker
    {
        decimal GetCommission(int orderedCoinsAmount);
        decimal GetQuoteForTransaction(int orderedNumber);
        void TakeOrder(int orderedNumber);
        int ReportTransactedNumber();
    }
}