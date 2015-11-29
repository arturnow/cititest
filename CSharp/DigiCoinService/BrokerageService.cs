using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCoinService
{
    public class BrokerageService : IBrokerageService
    {
        private readonly IList<IBroker> _registeredBrokers = new List<IBroker>();
        private bool _isServiceInProperState;

        public void RegisterBroker(IBroker broker)
        {
            if (broker == null)
            {
                throw new ArgumentNullException("broker");
            }

            if (_registeredBrokers.Count == 2)
            {
                throw new ApplicationException("Service constrained to 2 brokers");
            }
            _registeredBrokers.Add(broker);

            if (_registeredBrokers.Count == 2)
            {
                _isServiceInProperState = true;
            }
        }

        public decimal PlaceOrder(int numberOfCoinsOrderd)
        {
            if (!_isServiceInProperState)
            {
                throw new ApplicationException("Service out of order");
            }

            if (numberOfCoinsOrderd <= 0)
            {
                throw new ArgumentException("Less or equal 0", "numberOfCoinsOrderd");
            }

            var maxMoves = numberOfCoinsOrderd / 10;
            decimal minQuote = 0;
            int broker1Order = 0, broker2Order = 0;

            var i = maxMoves > 10 ? maxMoves - 10 : 0;


            for (; i <= (maxMoves > 10 ? 10 : maxMoves); i++)
            {
                var quote1 = _registeredBrokers[0].GetQuoteForTransactoin(i * 10);
                var quote2 = _registeredBrokers[1].GetQuoteForTransactoin((maxMoves - i) * 10);

                var total = quote1 + quote2;
                if (minQuote == -1)
                {
                    minQuote = total;
                    broker1Order = i * 10;
                    broker2Order = (maxMoves - i) * 10;
                }
                else if (total < minQuote)
                {
                    minQuote = total;
                    broker1Order = i * 10;
                    broker2Order = (maxMoves - i) * 10;
                }

            }
            _registeredBrokers[0].TakeOrder(broker1Order);
            _registeredBrokers[1].TakeOrder(broker2Order);

            return minQuote;
        }
    }
}
