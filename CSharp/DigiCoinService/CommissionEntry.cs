namespace DigiCoinService
{
    public class CommissionEntry
    {
        public CommissionEntry(int amount, decimal commission)
        {
            Amount = amount;
            Commission = commission;
        }

        public int Amount { get; private set; }
        public decimal Commission { get; private set; }
    }
}