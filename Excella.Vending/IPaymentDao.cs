namespace Excella.Vending
{
    public interface IPaymentDao
    {
        void Save(int amountToSave);
        int Retrieve();
    }
}