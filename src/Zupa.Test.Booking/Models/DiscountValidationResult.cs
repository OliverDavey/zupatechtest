using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zupa.Test.Booking.Models
{
    public class DiscountValidationResult
    {
        public Discount Discount { get; set; }

        public bool IsValid { get; set; }

        public string ErrorMessage { get; set; }
    }
}
