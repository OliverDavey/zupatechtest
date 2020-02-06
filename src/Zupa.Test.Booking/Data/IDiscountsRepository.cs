using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Data
{
    public interface IDiscountsRepository
    {
        Task<DiscountValidationResult> ValidateAsync(string promoCode);
        Task ExpireDiscountAsync(string promoCode);
    }
}
