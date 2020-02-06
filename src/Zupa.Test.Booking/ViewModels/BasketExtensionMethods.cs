using System;
using System.Linq;

namespace Zupa.Test.Booking.ViewModels
{
    public static class BasketExtensionMethods
    {
        public static Models.Order ToOrderModel(this Models.Basket basket)
        {
            return new Models.Order
            {
                ID = Guid.NewGuid(),
                GrossTotal = basket.GetGrossTotal(),
                NetTotal = basket.Items.Sum(item => item.NetPrice),
                TaxTotal = basket.Items.Sum(item => item.NetPrice * item.TaxRate),
                Items = basket.Items.ToOrderItemModels()
            };
        }

        public static Basket ToBasketViewModel(this Models.Basket basket)
        {
            return new Basket
            {
                Items = basket.Items.ToBasketItemViewModels(),
                GrossTotal = basket.GetGrossTotal()
            };
        }

        private static double GetGrossTotal(this Models.Basket basket)
        {
            var netSubTotal = basket.Items.Sum(item => item.GrossPrice);

            if (basket.Discount != null)
            {
                switch (basket.Discount.Type)
                {
                    case Models.DiscountType.Percentage:
                        return netSubTotal * (1 - basket.Discount.Amount);
                    case Models.DiscountType.FlatAmount:
                        return netSubTotal - basket.Discount.Amount;
                }
            }

            return netSubTotal;
        }
    }
}
