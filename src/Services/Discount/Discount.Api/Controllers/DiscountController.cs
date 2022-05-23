using Discount.Api.Entities;
using Discount.Api.Exceptions;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly ILogger<DiscountController> _logger;
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(ILogger<DiscountController> logger, IDiscountRepository discountRepository)
        {
            _logger = logger;
            _discountRepository = discountRepository;
        }

        [HttpPost(Name = "CreateDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] Coupon coupon)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _discountRepository.Create(coupon);
            return Ok();

        }

        [HttpPut(Name = "UpdateDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] Coupon coupon)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _discountRepository.Update(coupon);
                return Ok();

            }
            catch (CouponNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{productname}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] string productname)
        {
            try
            {
                var coupont = await _discountRepository.Get(productname);
                return Ok(coupont);
            }
            catch (CouponNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet(Name = "GetAllDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public async Task<IActionResult> GetAll()
        {
            var coupons = await _discountRepository.GetAll();
            return Ok(coupons);
        }

        [HttpDelete(Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] string productName)
        {
            try
            {
                await _discountRepository.Delete(productName);
                return Ok();
            }
            catch (CouponNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
          
        }
    }
}
