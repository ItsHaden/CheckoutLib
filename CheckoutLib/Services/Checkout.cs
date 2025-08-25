using Checkout.Core.Interfaces;

namespace Checkout.Core.Services
{
    public class Checkout : ICheckout
    {
        public Checkout() { }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void Scan(string item)
        {
            throw new NotImplementedException();
        }
    }
}
