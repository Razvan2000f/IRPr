using IRPr.Models;
using IRPr.Repositories.Interfaces;
using IRPr.Services.Interfaces;

namespace IRPr.Services
{
    public class ProductService: IProductService
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
            product.CategoryID = 1;
            ICollection<Image> images = _imageService.AddImage(imageFiles);
            product.images = images;

            _repositoryWrapper.productRepository.Create(product);
            _repositoryWrapper.Save();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> products=_repositoryWrapper.productRepository.FindAll().ToList();
            return products;
        }
    }
}
