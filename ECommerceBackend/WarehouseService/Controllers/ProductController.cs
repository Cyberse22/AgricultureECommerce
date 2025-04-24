using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WarehouseService.Data;
using WarehouseService.Data.MongoDB;
using WarehouseService.Models;
using WarehouseService.Services;

namespace WarehouseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetProductById/{productId}")]
        public async Task<IActionResult> GetProductById(string productId)
        {
            try
            {
                var product = await _productService.GetProductById(productId);
                if (product == null)
                {
                    return NotFound("Product not found.");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("SearchProduct/{keyword}")]
        public async Task<IActionResult> SearchProductByKeyword(string keyword)
        {
            try
            {
                var product = await _productService.SearchProductByKeyword(keyword);
                if (product == null)
                {
                    return NotFound("Product not found.");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetProductByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetProductByCategoryId(string categoryId)
        {
            try
            {
                var product = await _productService.GetProductByCategoryId(categoryId);
                if (product == null)
                {
                    return NotFound("Product not found.");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetProductByInventoryId/{inventoryId}")]
        public async Task<IActionResult> GetProductByInventoryId(string inventoryId)
        {
            try
            {
                var product = await _productService.GetProductByInventoryId(inventoryId);
                if (product == null)
                {
                    return NotFound("Product not found.");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid product data.");
                }
                var createdProduct = await _productService.CreateProductAsync(model);
                return CreatedAtAction(nameof(GetProductById), new { productName = createdProduct.ProductName }, createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("UpdateImage")]
        public async Task<IActionResult> UpdateImage([FromForm] UpdateImageModel model)
        {
            try
            {
                if (model == null || model.ProductImage == null)
                {
                    return BadRequest("Invalid image data.");
                }
                var updatedImages = await _productService.UpdateImage(model);
                return Ok(updatedImages);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("UpdateProduct/{productId}")]
        public async Task<IActionResult> UpdateProduct(string productId, [FromBody] UpdateProductModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid product data.");
                }
                var product = await _productService.GetProductById(productId);
                if (product == null)
                {
                    return NotFound("Product not found.");
                }
                var updatedProduct = await _productService.UpdateProductAsync(productId, model);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
