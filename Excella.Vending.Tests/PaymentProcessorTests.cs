using Moq;
using NUnit.Framework;

namespace Excella.Vending.Tests
{
    public class PaymentProcessorTests
    {
        private PaymentProcessor _processor;
        private Mock<IPaymentDao> _mockDao;

        [SetUp]
        public void Setup()
        {
            _mockDao = new Mock<IPaymentDao>();
            _processor = new PaymentProcessor(
                _mockDao.Object);
        }

        [Test]
        public void IsPaymentMade_NoPayment_ExpectFalse()
        {
            var paymentMade = _processor.IsPaymentMade();

            Assert.That(paymentMade, Is.False);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(49)]
        public void IsPaymentMade_WithInsufficientPayment_ExpectTrue(int paymentToTest)
        {
            _processor.MakePayment(paymentToTest);

            var paymentMade = _processor.IsPaymentMade();

            Assert.That(paymentMade, Is.False);
        }

        [Test]
        public void IsPaymentMade_WithSufficientPayment_ExpectTrue()
        {
            _mockDao.Setup(x => x.Retrieve())
                .Returns(50);

            var paymentMade = _processor.IsPaymentMade();

            Assert.That(paymentMade, Is.True);
        }

        [Test]
        public void ReturnChange_NoPayment_NoChangeReturned()
        {
            var change = _processor.ReturnChange();

            Assert.That(change, Is.EqualTo(0));
        }

        [Test]
        public void ReturnChange_PaymentMade_ReturnsPayment()
        {
            _mockDao.Setup(x => x.Retrieve()).Returns(30);

            var change = _processor.ReturnChange();

            Assert.That(change, Is.EqualTo(30));
        }

        [Test]
        public void ReturnChange_AlreadyReturned_Expect0()
        {
            var paymentAmount = 30;
            _processor.MakePayment(paymentAmount);
            _processor.ReturnChange();

            var secondAttempt = _processor.ReturnChange();

            Assert.That(secondAttempt, Is.EqualTo(0));
        }

        [Test]
        public void ProcessPayment_WithMoney_SavesDecreasedTotal()
        {
            _mockDao.Setup(x => x.Retrieve()).Returns(75);

            _processor.ProcessPayment();

            _mockDao.Verify(x=>x.Save(25));
        }        
    }
}
