namespace Checkout.Core.Models
{
    public class ItemPrice
    {
        public string Sku { get; set; }
        public int UnitPrice { get; set; }

        public ItemPrice(string itemSku, int unitPrice)
        {
            Sku = itemSku;
            UnitPrice = unitPrice;
        }
    }
}
