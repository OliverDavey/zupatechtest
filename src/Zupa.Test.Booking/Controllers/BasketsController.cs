using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zupa.Test.Booking.Data;
using Zupa.Test.Booking.ViewModels;

namespace Zupa.Test.Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketsRepository _basketsRepository;
        private readonly IDiscountsRepository _discountsRepository;

        public BasketsController(
            IBasketsRepository basketsRepository,
            IDiscountsRepository discountsRepository)
        {
            _basketsRepository = basketsRepository;
            _discountsRepository = discountsRepository;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Basket>> AddToBasket([FromBody]BasketItem basketItem)
        {
            var item = basketItem.ToBasketItemModel();
            var basket = await _basketsRepository.AddToBasketAsync(item);

            return basket.ToBasketViewModel();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Basket>> GetBasket()
        {
            var basket = await _basketsRepository.ReadAsync();
            return basket.ToBasketViewModel();
        }

        [HttpPost("promo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AddDiscountResult>> AddDiscount([FromBody]Discount request)
        {
            var discount = await _discountsRepository.ValidateAsync(request.PromoCode);

            if (!discount.IsValid)
            {
                return new AddDiscountResult
                {
                    Success = false,
                    ErrorMessage = discount.ErrorMessage
                };
            }

            var result = await _basketsRepository.AddDiscountAsync(discount.Discount);

            if (!result.Success)
            {
                return new AddDiscountResult
                {
                    Success = false,
                    ErrorMessage = "Promo code already applied to basket"
                };
            }
            else
            {
                await _discountsRepository.ExpireDiscountAsync(request.PromoCode);
                return new AddDiscountResult
                {
                    Success = true
                };
            }
        }
    }
}