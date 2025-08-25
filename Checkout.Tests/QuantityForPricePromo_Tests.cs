using Checkout.Core.Interfaces;
using Checkout.Core.Models;
using Checkout.Core.Promotions;

namespace Checkout.Tests
{
    public class QuantityForPricePromo_Tests
    {
        private readonly IPromotion _promotion;
        private readonly Dictionary<string, ItemPrice> _stockList;

        public QuantityForPricePromo_Tests()
        {
            _promotion = new QuantityForPricePromo("A", 3, 130);

            _stockList = new Dictionary<string, ItemPrice>
        {
            { "A", new ItemPrice("A", 50) }
        };
        }


        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 20)]
        [InlineData(4, 20)]
        [InlineData(6, 40)]
        public void CalculateDiscount_ShouldReturnCorrectDiscount(int quantity, int expectedDiscount)
        {
            var basket = new Dictionary<string, int>
            {
                { "A", quantity }
            };

            var discount = _promotion.CalculateDiscount(basket, _stockList);

            Assert.Equal(expectedDiscount, discount);
        }

        [Fact]
        public void CalculateDiscount_ShouldReturnZero_WhenSkuNotPresent()
        {
            var basket = new Dictionary<string, int>
            {
                { "B", 3 }
            };

            var discount = _promotion.CalculateDiscount(basket, _stockList);

            Assert.Equal(0, discount);
        }


    }
}