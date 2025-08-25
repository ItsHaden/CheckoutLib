using Checkout.Core.Models;

namespace Checkout.Core.Interfaces
{
    public interface IPromotion
    {
        int CalculateDiscount(Dictionary<string, int> items, Dictionary<string, ItemPrice> priceRules);
    }
}
