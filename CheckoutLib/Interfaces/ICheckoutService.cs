namespace Checkout.Core.Interfaces
{
    public interface ICheckoutService
    {
        void Scan(string item);
        int GetTotalPrice();
    }
}
