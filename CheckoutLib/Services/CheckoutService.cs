using Checkout.Core.Interfaces;
using Checkout.Core.Models;

namespace Checkout.Core.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly Dictionary<string, ItemPriceRule> _stockList;
        private readonly Dictionary<string, int> _basket;

        public CheckoutService(Dictionary<string, ItemPriceRule> stockList)
        {
            _stockList = stockList;
            _basket = new Dictionary<string, int>();
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
            int totalPrice = 0;

            // Loop through each group of items and handle promo
            // We want to look up an item against the price rule object, if we find a match, apply the rule
            // If no rule found then simply do a calculation for item and quantity
            foreach (var (sku, quantity) in _basket)
            {
                var stockRecord = _stockList[sku];

                //Lets check if there's any promo for it
                if (stockRecord.PromoQuantity.HasValue && stockRecord.PromoPrice.HasValue)
                {
                    // Find if there are multiples for promo (we may have multiple amounts eligible)
                    var promoCount = quantity / stockRecord.PromoQuantity.Value;

                    // Modulus - Find out the remainder after promos have been applied
                    var overage = quantity % stockRecord.PromoQuantity.Value;

                    var totalItemPrice = (promoCount * stockRecord.PromoPrice.Value) + (overage * stockRecord.UnitPrice);
                    totalPrice += totalItemPrice;
                }
                else
                {
                    totalPrice += quantity * stockRecord.UnitPrice;
                }
            }

            return totalPrice;
        }
    }
}
