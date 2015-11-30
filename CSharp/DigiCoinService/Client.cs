using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiCoinService
{
    public enum OrderType
    {
        Buy = 1,
        Sell = -1
    }

    public class Client : IClient
    {
        private class OrderRecord
        {
            private readonly int _number;
            private readonly decimal _value;
            private readonly OrderType _type;

            public OrderRecord(int number, decimal value, OrderType type)
            {
                _number = number;
                _value = value;
                _type = type;
            }

            public int Number
            {
                get { return _number; }
            }

            public decimal Value
            {
                get { return _value; }
            }

            public OrderType Type
            {
                get { return _type; }
            }
        }

        readonly IList<OrderRecord> _orderRecords = new List<OrderRecord>(); 

        private readonly IBrokerageService _brokerageService;

        public Client(IBrokerageService brokerageService)
        {
            if (brokerageService == null)
            {
                throw new ArgumentNullException("brokerageService");
            }
            _brokerageService = brokerageService;
        }


        public decimal MakerOrder(int orderedCoins, OrderType type)
        {
            var transactionPrice =_brokerageService.PlaceOrder(orderedCoins);

            _orderRecords.Add(new OrderRecord(orderedCoins, transactionPrice, type));

            return transactionPrice;
        }

        public decimal GetOrderNetValue()
        {
            decimal avg = 0, orderSum = 0;
            foreach (var orderRecord in _orderRecords)
            {
                avg += orderRecord.Value/orderRecord.Number;
                orderSum += orderRecord.Number*(int) orderRecord.Type;

            }

            var netValue = orderSum*(avg/_orderRecords.Count);

            return Math.Round(netValue, 3);
        }
    }
}
