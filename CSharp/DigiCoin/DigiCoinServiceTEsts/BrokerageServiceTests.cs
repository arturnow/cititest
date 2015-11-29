using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigiCoinService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
