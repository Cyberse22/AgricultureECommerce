using System.Net;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using WarehouseService.Data;
using WarehouseService.Models;
using WarehouseService.Repositories;

namespace WarehouseService.Services.Impl;

public class CategoryServiceImpl : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly Cloudinary _cloudinary;

    public CategoryServiceImpl(ICategoryRepository categoryRepository, IMapper mapper, Cloudinary cloudinary)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _cloudinary = cloudinary;
    }

    public async Task<UpdateCategoryModel> CreateCategoryAsync(UpdateCategoryModel model)
    {
        var category = _mapper.Map<Category>(model);
        await _categoryRepository.CreateCategoryAsync(category);
        return _mapper.Map<UpdateCategoryModel>(category);
    }

    public async Task<List<CategoryModel>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();
        if (categories == null || !categories.Any())
        {
            throw new Exception("No categories found.");
        }
        var result = _mapper.Map<List<CategoryModel>>(categories);
        return result;
    }

    public async Task <List<CategoryModel>> GetCategoryByCategoryParentAsync(string categoryParent)
    {
        var categories = await _categoryRepository.GetCategoryByCategoryParentAsync(categoryParent);
        if (categories == null)
        {
            throw new Exception("No categories found.");
        }
        var result = _mapper.Map<List<CategoryModel>>(categories);
        return result;
    }

    public async Task<CategoryModel> GetCategoryByIdAsync(string categoryId)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
        if (category == null)
        {
            throw new Exception("Category not found.");
        }
        var result = _mapper.Map<CategoryModel>(category);
        return result;
    }

    public async Task<CategoryModel> GetCategoryByNameAsync(string categoryName)
    {
        var category = await _categoryRepository.GetCategoryByNameAsync(categoryName);
        if (category == null)
        {
            throw new Exception("Category not found.");
        }
        var result = _mapper.Map<CategoryModel>(category);
        return result;
    }

    public async Task<UpdateCategoryModel> UpdateCategoryAsync(UpdateCategoryModel category)
    {
        var existingCategory = _categoryRepository.GetCategoryByIdAsync(category.CategoryId);
        if (existingCategory == null)
        {
            throw new Exception("Category not found.");
        }
        var updatedCategory = _mapper.Map<Category>(category);
        await _categoryRepository.UpdateCategoryAsync(updatedCategory);
        return _mapper.Map<UpdateCategoryModel>(updatedCategory);
    }

    public async Task<string> UploadCategoryImageAsync(UploadCategoryImage model)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(model.CategoryId);
        if (category == null)
        {
            throw new Exception("Category not found.");
        }

        var file = model.CategoryImage;
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            Folder = "category_images",
            Transformation = new Transformation(),
            PublicId = $"Category_{model.CategoryId}"
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        if (uploadResult.StatusCode != HttpStatusCode.OK)
            throw new Exception("Upload failed");

        if (category.CategoryImages == null)
        {
            category.CategoryImages = new List<string>();
        }

        category.CategoryImages.Add(uploadResult.SecureUrl.ToString());
        await _categoryRepository.UpdateCategoryAsync(category);
        return uploadResult.SecureUrl.ToString();
    }
}