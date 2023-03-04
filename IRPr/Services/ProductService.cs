using IRPr.Models;
using IRPr.Repositories.Interfaces;
using IRPr.Services.Interfaces;

namespace IRPr.Services
{
    public class ProductService : IProductService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IImageService _imageService;

        public ProductService(IRepositoryWrapper repositoryWrapper, IImageService imageService)
        {
            _repositoryWrapper = repositoryWrapper;
            _imageService = imageService;
        }

        public void AddProduct(Product product, ICollection<IFormFile> imageFiles)
        {
            foreach (IFormFile imageFile in imageFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                string extension = Path.GetExtension(imageFile.FileName);
                product.MainPhotoName = fileName = fileName + DateTime.Now.ToString("_yyMMddHHmmss") + extension;
                break;
            }

            ICollection<Image> images = _imageService.AddImage(imageFiles);
            product.images = images;

            _repositoryWrapper.productRepository.Create(product);
            _repositoryWrapper.Save();
        }

        public Product GetProductById(int id)
        {
            Product product = _repositoryWrapper.productRepository.GetProductById(id);
            return product;
        }

        public dynamic GetAllCategories()
        {
            IEnumerable<Category> categories = _repositoryWrapper.CategoryRepository.FindAll().ToList();
            return categories;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> products = _repositoryWrapper.productRepository.FindAll().ToList();
            return products;
        }

        public List<CartItem> GetCartItems(string userID)
        {
            List<CartItem> cartItems = _repositoryWrapper.CartItemRepository.FindByCondition(cartItem => cartItem.UserID == userID).ToList();
            return cartItems;
        }

        public void AddCartItem(CartItem newCartItem)
        {
            _repositoryWrapper.CartItemRepository.Create(newCartItem);
            _repositoryWrapper.Save();
        }

        public void DeleteShoppingCart(string userID)
        {
            List<CartItem> items = GetCartItems(userID);
            foreach (var item in items)
            {
                _repositoryWrapper.CartItemRepository.Delete(item);
            }
            _repositoryWrapper.Save();
        }
    }
}
