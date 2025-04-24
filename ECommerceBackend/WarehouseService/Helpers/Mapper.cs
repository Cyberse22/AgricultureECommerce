using AutoMapper;
using WarehouseService.Data;
using WarehouseService.Models;

namespace WarehouseService.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Product, CreateProductModel>().ReverseMap();
            CreateMap<Product, GetProductModel>().ReverseMap();
            CreateMap<Product, UpdateProductModel>().ReverseMap();
            CreateMap<Product, UpdateImageModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Category, UpdateCategoryModel>().ReverseMap();
            CreateMap<Inventory, InventoryModel>().ReverseMap();
            CreateMap<InventoryTransaction, TransactionModel>().ReverseMap();
            CreateMap<ProductTransactionModel, ProductTransaction>()
                .ForMember(dest => dest.ProductName, opt => opt.Ignore());
            CreateMap<ProductTransaction, ProductTransactionModel>();
        }
    }
}
