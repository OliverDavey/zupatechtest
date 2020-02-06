using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Data
{
    internal class InMemoryBasketsRepository : IBasketsRepository
    {
        private Basket _basket;

        public InMemoryBasketsRepository()
        {
            _basket = new Basket();
        }

        public Task<Basket> ReadAsync()
        {
            return Task.FromResult(_basket);
        }

        public Task ResetBasketAsync()
        {
            return Task.FromResult(_basket = new Basket());
        }

        public Task<Basket> AddToBasketAsync(BasketItem item)
        {
            var items = _basket.Items.ToList();
            items.Add(item);
            _basket.Items = items;

            return Task.FromResult(_basket);
        }

        public Task<AddDiscountResult> AddDiscountAsync(Discount discount)
        {
            if (_basket.Discount != null)
            {
                return Task.FromResult(new AddDiscountResult
                {
                    Success = false,
                    ErrorMessage = "Basket already has a promo code"
                });
            }

            _basket.Discount = discount;

            return Task.FromResult(new AddDiscountResult
            {
                Success = true
            });
        }
    }
}
