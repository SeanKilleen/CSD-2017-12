using NUnit.Framework;

namespace Excella.Vending.Tests
{
    public class VendingMachineTests
    {
        private VendingMachine _vendingMachine; 

        [SetUp]
        public void Setup()
        {
            _vendingMachine 
                = new VendingMachine(new PaymentProcessor(new PaymentDao()));
        }

        [Test]
        public void BuyProduct_WhenNoMoney_ExpectNoProduct()
        {
            var product = _vendingMachine.BuyProduct();

            Assert.That(product, Is.Null);
        }

        [Test]
        public void BuyProduct_WhenSufficientMoney_ExpectProduct()
        {
            _vendingMachine.InsertQuarter();
            _vendingMachine.InsertQuarter();

            var product = _vendingMachine.BuyProduct();

            Assert.That(product, Is.Not.Null);
        }

        [Test]
        public void BuyProduct_WhenInsufficientMoney_ExpectNoProduct()
        {
            _vendingMachine.InsertQuarter();

            var product = _vendingMachine.BuyProduct();

            Assert.That(product, Is.Null);
        }

        [Test]
        public void ReleaseChange_WhenNoMoney_Expect0Change()
        {
            _vendingMachine.ReleaseChange();
            var change = _vendingMachine.GetChangeFromDispenser();
            Assert.That(change, Is.EqualTo(0));
        }

        [Test]
        public void ReleaseChange_WhenMoney_ExpectMoneyBack()
        {
            _vendingMachine.InsertQuarter();

            _vendingMachine.ReleaseChange();
            var change = _vendingMachine.GetChangeFromDispenser();

            Assert.That(change, Is.EqualTo(25));
        }

        [Test]
        public void ReleaseChange_WhenMoneyAlreadyReleased_ExpectZeroChange()
        {
            _vendingMachine.InsertQuarter();
            _vendingMachine.ReleaseChange();

            _vendingMachine.ReleaseChange();
            var secondChangeAttempt = _vendingMachine.GetChangeFromDispenser();

            Assert.That(secondChangeAttempt, Is.EqualTo(0));
        }

        [Test]
        public void BuyProduct_WithMoreThanEnoughMoney_ExpectProduct()
        {
            _vendingMachine.InsertQuarter();
            _vendingMachine.InsertQuarter();
            _vendingMachine.InsertQuarter();

            var product = _vendingMachine.BuyProduct();

            Assert.That(product, Is.Not.Null);
        }

        [Test]
        public void BuyProduct_WithMoreThanEnoughMoney_ExpectChange()
        {
            _vendingMachine.InsertQuarter();
            _vendingMachine.InsertQuarter();
            _vendingMachine.InsertQuarter();
            _vendingMachine.BuyProduct();

            var change = _vendingMachine.GetChangeFromDispenser();

            Assert.That(change, Is.EqualTo(25));
        }

        [Test]
        public void GetChangeFromDispenser_GetChangeTwoTimes_ExpectSecondTimeToBeZero()
        {
            _vendingMachine.InsertQuarter();
            _vendingMachine.ReleaseChange();
            _vendingMachine.GetChangeFromDispenser();

            var secondChangeAttempt = _vendingMachine.GetChangeFromDispenser();

            Assert.That(secondChangeAttempt, Is.EqualTo(0));
        }

        [Test]
        public void GetChangeFromDispenser_WhenNoMoney_ExpectNothing()
        {
            var change = _vendingMachine.GetChangeFromDispenser();

            Assert.That(change, Is.EqualTo(0));
        }

        [Test]
        public void GetChangeFromDispenser_WhenMoney_ExpectNothing()
        {
            _vendingMachine.InsertQuarter();
            var change = _vendingMachine.GetChangeFromDispenser();

            Assert.That(change, Is.EqualTo(0));
        }

        [Test]
        public void GetChangeFromDispenser_WhenBuyingProductWithExtraMoney_ExpectMoneyBack()
        {
            _vendingMachine.InsertQuarter();
            _vendingMachine.InsertQuarter();
            _vendingMachine.InsertQuarter();
            _vendingMachine.BuyProduct();

            var change = _vendingMachine.GetChangeFromDispenser();

            Assert.That(change, Is.EqualTo(25));
        }

        [Test]
        public void GetChangeFromDispenser_WhenBuyingWithExactMoney_NoChange()
        {
            _vendingMachine.InsertQuarter();
            _vendingMachine.InsertQuarter();
            _vendingMachine.BuyProduct();

            var change = _vendingMachine.GetChangeFromDispenser();

            Assert.That(change, Is.EqualTo(0));
        }

        [Test]
        public void GetChangeFromDispenser_WhenBuyingWithInsufficientMoney_NoChange()
        {
            _vendingMachine.InsertQuarter();
            _vendingMachine.BuyProduct();

            var change = _vendingMachine.GetChangeFromDispenser();

            Assert.That(change, Is.EqualTo(0));
        }

        [Test]
        public void GetChangeFromDispenser_WhenCoinsInsertedAndReleaseChange_ExpectChange()
        {
            _vendingMachine.InsertQuarter();
            _vendingMachine.InsertQuarter();
            _vendingMachine.InsertQuarter();
            _vendingMachine.ReleaseChange();

            var change = _vendingMachine.GetChangeFromDispenser();

            Assert.That(change, Is.EqualTo(75));

        }
    }
}
