using IRPr.Models;
using IRPr.Repositories.Interfaces;
using IRPr.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace IRPr.Services
{
    public class ProductService : IProductService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IImageService _imageService;
        private IWebHostEnvironment _webHostEnvironment;

        public ProductService(IRepositoryWrapper repositoryWrapper, IImageService imageService, IWebHostEnvironment webHostEnvironment)
        {
            _repositoryWrapper = repositoryWrapper;
            _imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddProduct(Product product, ICollection<IFormFile> imageFiles, IFormFile docuFile)
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

            string wwwRootPath = _webHostEnvironment.WebRootPath;

            string docuName = Path.GetFileNameWithoutExtension(docuFile.FileName);
            string docuExtension = Path.GetExtension(docuFile.FileName);
            product.DocumentName = docuName = docuName + DateTime.Now.ToString("_yyMMddHHmmss") + docuExtension;
            string path = Path.Combine(wwwRootPath + "/documents", docuName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                docuFile.CopyTo(fileStream);
            }

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
