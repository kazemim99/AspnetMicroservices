using Catalog.Api.Data;
using Catalog.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IClientSessionHandle _clientSessionHandle;
        private readonly IProductRepository _productRepository;

        public ProductController(IClientSessionHandle clientSessionHandle,
            IProductRepository productRepository)
        {
            _clientSessionHandle = clientSessionHandle;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           var products =  await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Creata([FromBody]Product product)
        {
             await _productRepository.InsertAsync(product);
            return Ok();
        }
    }
}
