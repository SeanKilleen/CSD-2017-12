namespace Excella.Vending
{
    public class PaymentProcessor : IPaymentProcessor
    {
        const int COST_OF_PRODUCT = 50;
        private readonly IPaymentDao _dao;

        public PaymentProcessor(IPaymentDao paymentDao)
        {
            _dao = paymentDao;
        }
        public bool IsPaymentMade()
        {
            return _dao.Retrieve() >= COST_OF_PRODUCT;
        }

        public void MakePayment(int payment)
        {
            var balance = _dao.Retrieve();
            _dao.Save(balance + payment);
        }

        public int ReturnChange()
        {
            var amountToReturn = _dao.Retrieve();
            _dao.Save(0);
            return amountToReturn;
        }

        public void ProcessPayment()
        {
            var balance = _dao.Retrieve();
            _dao.Save(balance - COST_OF_PRODUCT);
        }
    }
}
