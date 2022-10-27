using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using arapi.Dtos.Product;
using ARAPI.Dtos.Product;
using ARAPI.Services.ProductService;
using Microsoft.AspNetCore.Mvc;


namespace ARAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
            
        }
        

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> GetProuducts()
        {
            return Ok(await _productService.GetAllProuducts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> GetSingle(int id)
        {
            return Ok(await _productService.GetProductById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> AddProduct(AddProductDto newProduct)
        {
            
            return Ok(await _productService.AddProduct(newProduct));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> UpdateProduct(UpdateProductDto updatedProduct)
        {
            var responese = await _productService.UpdateProduct(updatedProduct);
            if (responese.Data == null)
            {
                return NotFound(responese);
            }
            return Ok(responese);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> Delete(int id)
        {
            var responese = await _productService.DeleteProduct(id);
            if (responese.Data == null)
            {
                return NotFound(responese);
            }
            return Ok(responese);
        }
    }
}