using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCoinService
{
    public class DigiCoinBroker : IBroker
    {
        private readonly IEnumerable<CommissionEntry> _commission;
        private readonly decimal _quote;
        private int _coinsTransaced =0;

        private DigiCoinBroker(decimal quote)
        {
            if (quote <= 0)
            {
                throw new ArgumentException("Equal or less then 0", "quote");
            }
            _quote = quote;
        }

        public DigiCoinBroker(decimal commission, decimal quote) : this(quote)
        {
            if(commission <= 0)
            {
                throw new ArgumentException("Equal or less then 0", "commission");
            }
            _commission = new List<CommissionEntry>{ new CommissionEntry(100, commission)};
        }

        public DigiCoinBroker(IEnumerable<CommissionEntry> commission, decimal quote) : this(quote)
        {
            if (commission == null)
            {
                throw new ArgumentNullException("commission");
            }

            _commission = commission;
        }

        public decimal GetCommission(int orderedCoinsAmount)
        {
            if (orderedCoinsAmount < 0)
            {
                throw new ArgumentException("Equal or less then 0", "orderedCoinsAmount");
            }
            if (orderedCoinsAmount > 100)
            {
                throw new ArgumentException("Equal or less then 100", "orderedCoinsAmount");
            }

            if (orderedCoinsAmount%10 != 0)
            {
                throw new ArgumentException("Must be multiplication of 10", "orderedCoinsAmount");
            }

            decimal commissionValue = -1;
            var commission = _commission.OrderBy(entry => entry.Amount).FirstOrDefault(entry => orderedCoinsAmount <= entry.Amount);

            if (commission != null)
            {
                commissionValue = commission.Commission;
            }

            return commissionValue;
        }

        public decimal GetQuoteForTransaction(int orderedNumber)
        {
            var commisionForOrder = GetCommission(orderedNumber);
            return (orderedNumber*_quote)*(1 + commisionForOrder);
        }

        public void TakeOrder(int orderedNumber)
        {
            _coinsTransaced += orderedNumber;
        }

        public int ReportTransactedNumber()
        {
            return _coinsTransaced;
        }
    }
}
