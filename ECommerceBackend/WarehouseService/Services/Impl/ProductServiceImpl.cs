using System.Net;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MongoDB.Driver.Linq;
using WarehouseService.Data;
using WarehouseService.Models;
using WarehouseService.Repositories;

namespace WarehouseService.Services.Impl
{
    public class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public ProductServiceImpl(IProductRepository productRepository, IMapper mapper, Cloudinary cloudinary)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public async Task<CreateProductModel> CreateProductAsync (CreateProductModel model)
        {
            var product = _mapper.Map<Product>(model);
            product.AvailableQuantity = model.Quantity;
            await _productRepository.CreateProductAsync(product);
            return _mapper.Map<CreateProductModel>(product);
        }

        public async Task<GetProductModel> GetProductByCategoryId(string categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            if (products == null || !products.Any())
            {
                throw new Exception("No products found for the given category ID.");
            }
            var result = _mapper.Map<List<GetProductModel>>(products);
            return result.FirstOrDefault();
        }

        public async Task<GetProductModel> GetProductById(string productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }
            var result = _mapper.Map<GetProductModel>(product);
            return result;
        }

        public async Task<GetProductModel> GetProductByInventoryId(string inventoryId)
        {
            var products = await _productRepository.GetProductsByInventoryIdAsync(inventoryId);
            if (products == null || !products.Any())
            {
                throw new Exception("No products found for the given inventory ID.");
            }
            var result = _mapper.Map<List<GetProductModel>>(products);
            return result.FirstOrDefault();
        }

        public async Task<List<GetProductModel>> SearchProductByKeyword(string keyword)
        {
            var product = await _productRepository.GetProductByKeywordAsync(keyword);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }
            var result = _mapper.Map<List<GetProductModel>>(product);
            return result;
        }

        public async Task<List<GetProductModel>> GetProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            if (products == null || !products.Any())
            {
                throw new Exception("No products found.");
            }
            var result = _mapper.Map<List<GetProductModel>>(products);
            return result;
        }

        public async Task<List<IFormFile>> UpdateImage(UpdateImageModel model)
        {
            var product = await _productRepository.GetProductByIdAsync(model.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            List<string> imageUrls = new List<string>();
            foreach (var file in model.ProductImage)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "product_images",
                    Transformation = new Transformation(),
                    PublicId = $"Image{model.ProductId}"
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                imageUrls.Add(uploadResult.SecureUrl.ToString());
            }
            await _productRepository.UploadImageAsync(model.ProductId, imageUrls);
            return model.ProductImage;
        }

        public async Task<UpdateProductModel> UpdateProductAsync (string productId, UpdateProductModel model)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(productId);
            if (existingProduct == null)
            {
                throw new Exception("Product not found.");
            }

            existingProduct.ProductName = model.ProductName;
            existingProduct.ProductDescription = model.ProductDescription;
            existingProduct.ProductPrice = model.ProductPrice;

            await _productRepository.UpdateProductAsync(existingProduct);

            return _mapper.Map<UpdateProductModel>(existingProduct);
        }
    }
}
