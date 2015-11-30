using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigiCoinService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DigiCoinServiceTEsts
{
    [TestClass]
    public class BrokerageServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisterNullBroker_ThrowsArgumentNullException()
        {
            //arrange
            var service = new BrokerageService();
            //act
            //assert
            service.RegisterBroker(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void RegisterThirdBroker_ThrowsApplicationException()
        {
            //arrange
            var service = new BrokerageService();
            //act
            //assert
            service.RegisterBroker(new DigiCoinBroker(1m, 1m));
            service.RegisterBroker(new DigiCoinBroker(1m, 1m));
            service.RegisterBroker(new DigiCoinBroker(1m, 1m));
        }

        [TestMethod]
        public void PlaceOrder_CallGetQuoteFroTransaction_AtLeast1OnBothBroker()
        {
            //arrange
            var service = new BrokerageService();

            var broker1Mock = new Mock<IBroker>();
            var broker2Mock = new Mock<IBroker>();

            broker1Mock.Setup(b => b.GetQuoteForTransaction(It.IsAny<int>())).Returns(10);
            broker2Mock.Setup(b => b.GetQuoteForTransaction(It.IsAny<int>())).Returns(20);

            service.RegisterBroker(broker1Mock.Object);
            service.RegisterBroker(broker2Mock.Object);

            //act
            service.PlaceOrder(10);
            //assert;
            broker1Mock.Verify(b => b.GetQuoteForTransaction(It.IsAny<int>()), Times.AtLeast(1));
            broker2Mock.Verify(b => b.GetQuoteForTransaction(It.IsAny<int>()), Times.AtLeast(1));
        }

        [TestMethod]
        public void PlaceOrder_CallTakeOrder_AtLeast1OnBothBroker()
        {
            //arrange
            var service = new BrokerageService();

            var broker1Mock = new Mock<IBroker>();
            var broker2Mock = new Mock<IBroker>();

            broker1Mock.Setup(b => b.GetQuoteForTransaction(It.IsAny<int>())).Returns(10);
            broker2Mock.Setup(b => b.GetQuoteForTransaction(It.IsAny<int>())).Returns(20);

            service.RegisterBroker(broker1Mock.Object);
            service.RegisterBroker(broker2Mock.Object);

            //act
            service.PlaceOrder(10);
            //assert;
            broker1Mock.Verify(b => b.TakeOrder(It.IsAny<int>()), Times.AtLeast(1));
            broker2Mock.Verify(b => b.TakeOrder(It.IsAny<int>()), Times.AtLeast(1));
        }
    }
}
