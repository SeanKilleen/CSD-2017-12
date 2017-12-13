namespace Excella.Vending
{
    public interface IPaymentProcessor
    {
        bool IsPaymentMade();
        void MakePayment(int payment);
        int ReturnChange();
        void ProcessPayment();
    }
}