namespace Excella.Vending
{
    public class PaymentDao : IPaymentDao
    {
        private int _amount;

        public void Save(int amountToSave)
        {
            _amount = amountToSave;
        }

        public int Retrieve()
        {
            return _amount;
        }
    }
}