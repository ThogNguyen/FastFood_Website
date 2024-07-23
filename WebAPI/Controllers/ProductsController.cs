using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models.ProductModel;
using System.Reflection.Metadata;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(Guid id)
        {
            var products = await _productService.GetProductByIdAsync(id);
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductForCreate productDto)
        {
            var product = await _productService.CreateProductAsync(productDto);
            return Ok(product);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductForUpdate productDto, Guid id)
        {
            var product = await _productService.UpdateProductAsync(productDto, id);
            return Ok(product);
        }


    }
}
