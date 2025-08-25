using Checkout.Core.Interfaces;
using Checkout.Core.Models;

namespace Checkout.Core.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly Dictionary<string, ItemPrice> _stockList;
        private readonly List<IPromotion> _promotions;
        private readonly Dictionary<string, int> _basket;

        public CheckoutService(Dictionary<string, ItemPrice> stockList, List<IPromotion> promotions)
        {
            _stockList = stockList;
            _basket = new Dictionary<string, int>();
            _promotions = promotions;
        }

        public void Scan(string product)
        {
            // If the item doesnt exist in stock list throw it out
            if (!_stockList.ContainsKey(product))
            {
                throw new ArgumentException($"Item scanned is invalid: {product}");
            }

            // Lets set an entry up in the dictionary to save a double check here, so we can increase count on single line
            if (!_basket.TryGetValue(product, out int value))
            {
                value = 0;
                _basket[product] = value;
            }

            _basket[product] = ++value;
        }

        public int GetTotalPrice()
        {

            // Calculate total price without promotions
            int total = _basket.Sum(i => i.Value * _stockList[i.Key].UnitPrice);

            // Sum all discounts from promotions
            int totalDiscount = _promotions.Sum(p => p.CalculateDiscount(_basket, _stockList));

            return total - totalDiscount;
        }
    }
}
