using System;
using System.Collections.Generic;
using System.Threading;
using DigiCoinService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigiCoinServiceTEsts
{
    [TestClass]
    public class BrokerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CallConstructor_WithNullCommissionArray_ThrowsArgumentNullException()
        {
            //Arrange
            //Act
            //Assert
            var broker = new DigiCoinBroker(null, 1);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Equal or less then 0")]
        public void CallConstructor_WithQuoteLessThen0_ThrowsArgumentException()
        {
            //Arrange
            //Act
            //Assert
            var broker = new DigiCoinBroker(new List<CommissionEntry>(), -1);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Equal or less then 0")]
        public void CallConstructor_WithCommissionLessThen0_ThrowsArgumentException()
        {
            //Arrange
            //Act
            //Assert
            var broker = new DigiCoinBroker(-1, 1);

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BrokerWithCommissionListOfNull_ThrowsException()
        {
            //Arrange
            var broker = new DigiCoinBroker(new List<CommissionEntry> { null, null }, 1);

            //Act
            //Assert
            broker.GetCommission(10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Equal or less then 0")]
        public void OrderLessThen0_ThrowsException()
        {
            //Arrange
            var broker = new DigiCoinBroker(new List<CommissionEntry> { null, null }, 1);

            //Act
            //Assert
            broker.GetCommission(-10);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Equal or less then 100")]
        public void OrderMoreThen100_ThrowsException()
        {
            //Arrange
            var broker = new DigiCoinBroker(new List<CommissionEntry> { null, null }, 1);

            //Act
            //Assert
            broker.GetCommission(110);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Must be multiplication of 10")]
        public void OrderNot10Multiplication_ThrowsException()
        {
            //Arrange
            var broker = new DigiCoinBroker(new List<CommissionEntry> { null, null }, 1);

            //Act
            //Assert
            broker.GetCommission(9);
        }

        [TestMethod]
        public void CommissionSetToSomeNumber_ReturnsTheCommission()
        {
            //arrange
            const decimal expected = 0.03m;
            var broker = new DigiCoinBroker(expected, 1.44m);
            //act
            var result = broker.GetCommission(20);
            //asset
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CommissionSetArray1_ReturnsTheCommission()
        {
            //arrange
            const decimal expected = 0.03m;
            var commissionTable = new List<CommissionEntry>
            {
                new CommissionEntry(20, expected),
                new CommissionEntry(60, 1.34m),
                new CommissionEntry(100, 1.35m),
            };
            var broker = new DigiCoinBroker(commissionTable, 1.44m);
            //act
            var result = broker.GetCommission(20);
            //asset
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CommissionSetArray1_ReturnsTheCommission2()
        {
            //arrange
            const decimal expected = 0.03m;
            var commissionTable = new List<CommissionEntry>
            {
                new CommissionEntry(20, 1.3m),
                new CommissionEntry(60, expected),
                new CommissionEntry(100, 1.35m),
            };
            var broker = new DigiCoinBroker(commissionTable, 1.44m);
            //act
            var result = broker.GetCommission(50);
            //asset
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void CommissionSetArray1_ReturnsTheCommission3()
        {
            //arrange
            const decimal expected = 0.03m;
            var commissionTable = new List<CommissionEntry>
            {
                new CommissionEntry(20, 1.3m),
                new CommissionEntry(100, 1.35m),
                new CommissionEntry(60, expected),
            };
            var broker = new DigiCoinBroker(commissionTable, 1.44m);
            //act
            var result = broker.GetCommission(50);
            //asset
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BrokerWithSomeSettings_GetQuote_ReturnsProperQuote()
        {
            //Arrange
            const decimal expected = 62.58m;
            var broker = new DigiCoinBroker(0.05m, 1.49m);
            //Act
            var result = broker.GetQuoteForTransactoin(40);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BrokererTransacted2Orderd_With40And50Coins_Reports90()
        {
            //Arrange
            const int expected = 90;
            var broker = new DigiCoinBroker(0.05m, 1.49m);
            //Act
            broker.TakeOrder(40);
            broker.TakeOrder(50);

            var result = broker.ReportTransactedNumber();

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}
