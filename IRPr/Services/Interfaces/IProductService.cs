using IRPr.Models;

namespace IRPr.Services.Interfaces
{
    public interface IProductService
    {
        void AddProduct(Product product, ICollection<IFormFile> imageFiles);
        dynamic GetAllCategories();
        IEnumerable<Product> GetAllProducts();
    }
}
