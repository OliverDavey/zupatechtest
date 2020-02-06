using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Data
{
    internal class InMemoryDiscountsRepository : IDiscountsRepository
    {
        private List<Discount> _discounts;

        public InMemoryDiscountsRepository()
        {
            _discounts = new List<Discount>
            {
                new Discount { PromoCode = "discount10", Type = DiscountType.Percentage, Amount = 0.1 },
                new Discount { PromoCode = "discount50", Type = DiscountType.Percentage, Amount = 0.5 },
            };
        }

        public Task<DiscountValidationResult> ValidateAsync(string promoCode)
        {
            var discount = _discounts.FirstOrDefault(disc => disc.PromoCode == promoCode);
            var isValid = discount != null;

            var result = new DiscountValidationResult
            {
                Discount = discount,
                IsValid = isValid,
                ErrorMessage = isValid ? string.Empty : "Invalid promo code supplied"
            };

            return Task.FromResult(result);
        }

        public Task ExpireDiscountAsync(string promoCode)
        {
            var discount = _discounts.FirstOrDefault(disc => disc.PromoCode == promoCode);

            _discounts.Remove(discount);

            return Task.CompletedTask;
        }
    }
}
