using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zupa.Test.Booking.Models
{
    public class Discount
    {
        public string PromoCode { get; set; }

        public DiscountType Type { get; set; }

        public double Amount { get; set; }

        // Possible extensions: Start Date and Expiry Date
    }
}
