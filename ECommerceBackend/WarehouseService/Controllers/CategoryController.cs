using AutoMapper;
using WarehouseService.Data;
using WarehouseService.Repositories;
using Microsoft.AspNetCore.Mvc;
using WarehouseService.Models;
using WarehouseService.Services;

namespace WarehouseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(string categoryId)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(categoryId);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("name/{categoryName}")]
        public async Task<IActionResult> GetCategoryByName(string categoryName)
        {
            try
            {
                var category = await _categoryService.GetCategoryByNameAsync(categoryName);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] UpdateCategoryModel model)
        {
            try
            {
                var category = await _categoryService.CreateCategoryAsync(model);
                return CreatedAtAction(nameof(GetCategoryById), new { categoryId = category.CategoryId }, category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryModel model)
        {
            try
            {
                var category = await _categoryService.UpdateCategoryAsync(model);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadCategoryImage([FromForm] UploadCategoryImage model)
        {
            try
            {
                var result = await _categoryService.UploadCategoryImageAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("parent/{categoryParent}")]
        public async Task<IActionResult> GetCategoryByCategoryParent(string categoryParent)
        {
            try
            {
                var categories = await _categoryService.GetCategoryByCategoryParentAsync(categoryParent);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}