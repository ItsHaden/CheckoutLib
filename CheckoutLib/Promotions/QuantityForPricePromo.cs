using Checkout.Core.Interfaces;
using Checkout.Core.Models;

namespace Checkout.Core.Promotions
{
    public class QuantityForPricePromo : IPromotion
    {
        public string _sku { get; set; }
        public int _promoQuantity { get; set; }
        public int _promoPrice { get; set; }

        public QuantityForPricePromo(string sku, int quantity, int price)
        {
            _sku = sku;
            _promoQuantity = quantity;
            _promoPrice = price;
        }

        public int CalculateDiscount(Dictionary<string, int> items, Dictionary<string, ItemPrice> priceRules)
        {
            if (!items.TryGetValue(_sku, out var itemQuantity)) return 0;

            int unitPrice = priceRules[_sku].UnitPrice;

            int numberOfPromos = itemQuantity / _promoQuantity;
            int overage = itemQuantity % _promoQuantity;

            int normalPrice = itemQuantity * unitPrice;
            int promoPrice = (numberOfPromos * _promoPrice) + (overage * unitPrice);

            return normalPrice - promoPrice;
        }
    }
}
