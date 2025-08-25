namespace Checkout.Tests
{
    public class ICheckout_Tests
    {

        private ICheckout CreateCheckoutInstance()
        {
            var productList = new Dictionary<string, ItemPriceRule>();
            {
                { "A", new ItemPriceRule(50, 3, 130) },
                { "B", new ItemPriceRule(30, 2, 45) },
                { "C", new ItemPriceRule(20) },
                { "D", new ItemPriceRule(15) }
            }

            return new Checkout(productList);
        }

        [Theory]
        [InlineData("A", 50)]
        [InlineData("B", 30)]
        [InlineData("C", 20)]
        [InlineData("D", 15)]
        public void ScanSingleItem_AddedToTotal(string item, int itemPrice)
        {
            var checkout = CreateCheckoutInstance();
            checkout.Scan(item);
            var totalPrice = checkout.GetTotalPrice();
            Assert.Equal(itemPrice, totalPrice);
        }

        [Fact]
        public void ScanNoItems_TotalShouldBeZero()
        {
            var checkout = CreateCheckoutInstance();
            var total = checkout.GetTotalPrice();
            Assert.Equal(0, total);
        }

        [Fact]
        public void ScanMultipleItems_TotalPriceCorrect()
        {
            var checkout = CreateCheckoutInstance();
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("D");

            var total = checkout.GetTotalPrice();
            var expectedTotal = 50 + 30 + 50 + 15;

            Assert.Equal(expectedTotal, total);
        }

        [Fact]
        public void ScanMultipleSameItem_ApplyOffer()
        {
            var checkout = CreateCheckoutInstance();
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            var total = checkout.GetTotalPrice();

            Assert.Equal(130, total);
        }

        [Fact]
        public void ScanMultipleDifferentItems_ApplyMutipleOffers()
        {
            var checkout = CreateCheckoutInstance();
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("B");

            var total = checkout.GetTotalPrice();
            var expectedTotal = 130 + 45;

            Assert.Equal(expectedTotal, total);
        }

        [Fact]
        public void ScanMultipleDifferentItems_ApplyOffersAndSingles()
        {
            var checkout = CreateCheckoutInstance();
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("B");
            checkout.Scan("C");

            var total = checkout.GetTotalPrice();
            var expectedTotal = 130 + 45 + 20;

            Assert.Equal(expectedTotal, total);
        }

        [Fact]
        public void ScanSameItemOverDiscount_ApplyOffer()
        {
            var checkout = CreateCheckoutInstance();
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            var total = checkout.GetTotalPrice();
            var expectedTotal = 130 + 50;

            Assert.Equal(expectedTotal, total);
        }

        [Fact]
        public void ScanMultipleItemsRandomOrder_TotalShouldBeCorrect()
        {
            var checkout = CreateCheckoutInstance();
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("C");

            var total = checkout.GetTotalPrice();
            var expectedTotal = 130 + 45 + 20;
            Assert.Equal(expectedTotal, total);
        }

        [Fact]
        public void ScanInvalidItem_ThrowException()
        {
            var checkout = CreateCheckoutInstance();

            Assert.Throws<ArgumentException>(() => checkout.Scan("INVALID_ITEM"));
        }
    }
}