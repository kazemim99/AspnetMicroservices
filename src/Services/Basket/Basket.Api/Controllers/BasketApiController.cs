using Basket.Api.Entities;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketApiController : ControllerBase
    {
      private readonly  IBasketRepository _basketRepository;
        private readonly IDiscountGrpcService _discountGrpcService;
        public BasketApiController(IBasketRepository basketRepository, IDiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
        }
        // GET: api/<ValuesController>/admin 
        [HttpGet("{username}",Name ="GetBasket")]
        public async Task<ShoppingCart> Get([FromRoute]string username)
        {
            return await _basketRepository.GetBasket(username);
        }


        // PUT api/<ValuesController>/admin
        [HttpPut(Name ="UpdateBasket")]
        public async Task<ShoppingCart> Put([FromBody] ShoppingCart value)
        {
            foreach (var item in value.ShoppingCartItems)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            return await _basketRepository.UpdateBasket(value);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{username}",Name ="DeleteBasket")]
        public async Task Delete([FromRoute]string username)
        {
             await _basketRepository.DeleteBasket(username);

        }
    }
}
