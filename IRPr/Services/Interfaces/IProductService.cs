using IRPr.Models;

namespace IRPr.Services.Interfaces
{
    public interface IProductService
    {
        void AddProduct(Product product, ICollection<IFormFile> imageFiles);
        public Product GetProductById(int id);
        public dynamic GetAllCategories();
        IEnumerable<Product> GetAllProducts();
        List<CartItem> GetCartItems(string userID);
        void AddCartItem(CartItem newCartItem);
        void DeleteShoppingCart(string userID);
    }
}
