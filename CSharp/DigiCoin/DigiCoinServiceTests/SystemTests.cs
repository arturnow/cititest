using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DigiCoinService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigiCoinServiceTEsts
{
    [TestClass]
    public class SystemTests
    {

        private IBroker _broker1;
        private IBroker _broker2;

        private IClient _clientA;
        private IClient _clientB;
        private IClient _clientC;

        private IBrokerageService _service;

        [TestInitialize]
        public void Init()
        {


            _broker1 = new DigiCoinBroker(0.05m, 1.49m);

            var broker2Commission = new List<CommissionEntry>
            {
                new CommissionEntry(40, 0.03m),
                new CommissionEntry(80, 0.025m),
                new CommissionEntry(100, 0.02m),
            };

            _broker2 = new DigiCoinBroker(broker2Commission, 1.52m);

            _service = new BrokerageService();
            _service.RegisterBroker(_broker1);
            _service.RegisterBroker(_broker2);

            _clientA = new Client(_service);
            _clientB = new Client(_service);
            _clientC = new Client(_service);

        }

        [TestMethod]
        [Description("ClientA buys 10 for 15.645")]
        public void ClientA_Buys_Test1()
        {
            //arrange
            const decimal expected = 15.645m;
            //act
            var result = _clientA.MakerOrder(10, OrderType.Buy);
            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [Description("ClientB buys 40 for 62.58")]
        public void ClientB_Buys_Test2()
        {
            //arrange
            const decimal expected = 62.58m;
            //act
            var result = _clientB.MakerOrder(40, OrderType.Buy);
            //assert
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [Description("ClientA buys 50 for 77.9")]
        public void ClientA_Buys_For_Test3()
        {
            //arrange
            const decimal expected = 77.9m;
            //act
            var result = _clientA.MakerOrder(50, OrderType.Buy);
            //assert
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [Description("ClientB buys 100 for 155.04")]
        public void ClientB_Buys_For_Test4()
        {
            //arrange
            const decimal expected = 155.04m;
            //act
            var result = _clientB.MakerOrder(100, OrderType.Buy);
            //assert
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [Description("ClientB sells 80 for 124.64")]
        public void ClientB_Sells_Test5()
        {
            //arrange
            const decimal expected = 124.64m;
            //act
            var result = _clientB.MakerOrder(80, OrderType.Sell);
            //assert
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [Description("ClientC sells 70 for 109.06")]
        public void ClientC_Sells_Test6()
        {
            //arrange
            const decimal expected = 109.06m;
            //act
            var result = _clientC.MakerOrder(70, OrderType.Sell);
            //assert
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [Description("ClientA buys 130 for 201.975")]
        public void ClientA_Buys_Test7()
        {
            //arrange
            const decimal expected = 201.975m;
            //act
            var result = _clientA.MakerOrder(130, OrderType.Buy);
            //assert
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [Description("ClientB sells 60 for 93.48")]
        public void ClientB_Sells_Test8()
        {
            //arrange
            const decimal expected = 93.48m;
            //act
            var result = _clientB.MakerOrder(60, OrderType.Sell);
            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [Description("Clients net positions ClientA 296.156, ClientB 0, ClientC -109.06")]
        public void ClientsReportsProperNetValues()
        {

            this.ClientA_Buys_Test1();
            this.ClientB_Buys_Test2();
            this.ClientA_Buys_For_Test3();
            this.ClientB_Buys_For_Test4();
            this.ClientB_Sells_Test5();
            this.ClientC_Sells_Test6();
            this.ClientA_Buys_Test7();
            this.ClientB_Sells_Test8();
            /*
            _clientA.MakerOrder(10, OrderType.Buy);
            _clientB.MakerOrder(40, OrderType.Buy);
            _clientA.MakerOrder(50, OrderType.Buy);
            _clientB.MakerOrder(100, OrderType.Buy);
            _clientB.MakerOrder(80, OrderType.Sell);
            _clientC.MakerOrder(70, OrderType.Sell);
            _clientA.MakerOrder(130, OrderType.Buy);
            _clientB.MakerOrder(60, OrderType.Sell);
            */
            //assert
            Assert.AreEqual(_clientA.GetOrderNetValue(), 296.156m);
            Assert.AreEqual(_clientB.GetOrderNetValue(), 0m);
            Assert.AreEqual(_clientC.GetOrderNetValue(), -109.06m);
        }


        [TestMethod]
        [Description("Digicoins transacted by Brokers: Broker1 80, Broker2 460")]
        public void BrokerStatus()
        {

            this.ClientA_Buys_Test1();
            this.ClientB_Buys_Test2();
            this.ClientA_Buys_For_Test3();
            this.ClientB_Buys_For_Test4();
            this.ClientB_Sells_Test5();
            this.ClientC_Sells_Test6();
            this.ClientA_Buys_Test7();
            this.ClientB_Sells_Test8();
            /*
            _clientA.MakerOrder(10, OrderType.Buy);
            _clientB.MakerOrder(40, OrderType.Buy);
            _clientA.MakerOrder(50, OrderType.Buy);
            _clientB.MakerOrder(100, OrderType.Buy);
            _clientB.MakerOrder(80, OrderType.Sell);
            _clientC.MakerOrder(70, OrderType.Sell);
            _clientA.MakerOrder(130, OrderType.Buy);
            _clientB.MakerOrder(60, OrderType.Sell);
            */
            //assert
            Assert.AreEqual(_broker1.ReportTransactedNumber(), 80);
            Assert.AreEqual(_broker2.ReportTransactedNumber(), 460);
        }
    }
}
