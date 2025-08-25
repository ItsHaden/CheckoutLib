namespace Checkout.Core.Models
{
    public class ItemPriceRule
    {
        public int UnitPrice { get; set; }
        public int? PromoQuantity { get; set; }
        public int? PromoPrice { get; set; }

        public ItemPriceRule(int unitPrice, int? promoAmount = null, int? promoPrice = null)
        {
            UnitPrice = unitPrice;
            PromoQuantity = promoAmount;
            PromoPrice = promoPrice;
        }
    }
}
