using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Api.Exceptions
{
    public class CouponNotFoundException:Exception
    {
        public CouponNotFoundException(string productName): base($"Coupon for {productName} was not found.")
        {

        }
    }
}
