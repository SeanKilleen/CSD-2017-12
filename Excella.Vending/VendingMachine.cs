namespace Excella.Vending
{
    public class VendingMachine
    {
        private readonly IPaymentProcessor _paymentProcessor;
        private int _dispenserAmount;

        public VendingMachine(IPaymentProcessor processor)
        {
            _paymentProcessor = processor;
        }

        public Product BuyProduct()
        {
            if (_paymentProcessor.IsPaymentMade())
            {
                _paymentProcessor.ProcessPayment();
                this.ReleaseChange();
                return new Product();
            }
            return null;
        }

        public void InsertQuarter()
        {
            _paymentProcessor.MakePayment(25);
        }

        public void ReleaseChange()
        {
            _dispenserAmount = _paymentProcessor.ReturnChange();
        }

        public int GetChangeFromDispenser()
        {
            var amountToReturn = _dispenserAmount;
            _dispenserAmount = 0;
            return amountToReturn;
        }
    }

    public class Product { }

}
