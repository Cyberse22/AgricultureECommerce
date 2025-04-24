using System.Runtime.CompilerServices;

namespace WarehouseService.Models
{
    public class CategoryModel
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryParent { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryImage { get; set; }
    }
    public class UpdateCategoryModel
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryParent { get; set; }
        public string CategoryDescription { get; set; }
    }
    public class UploadCategoryImage
    {
        public string CategoryId { get; set; }
        public IFormFile CategoryImage { get; set; }
    }
}
