using IRPr.Models;
using IRPr.Repositories.Interfaces;

namespace IRPr.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ShopContext _shopContext;
        private IProductRepository? _productRepository;
        private IImageRepository? _imageRepository;
        private ICategoryRepository? _categoryRepository;
        private ICartItemRepository? _cartItemRepository;

        public IProductRepository productRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_shopContext);
                }

                return _productRepository;
            }
        }

        public IImageRepository ImageRepository
        {
            get
            {
                if (_imageRepository == null)
                {
                    _imageRepository = new ImageRepository(_shopContext);
                }

                return _imageRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_shopContext);
                }

                return _categoryRepository;
            }
        }

        public ICartItemRepository CartItemRepository
        {
            get
            {
                if (_cartItemRepository == null)
                {
                    _cartItemRepository = new CartItemRepository(_shopContext);
                }

                return _cartItemRepository;
            }
        }


        public RepositoryWrapper(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public void Save()
        {
            _shopContext.SaveChanges();
        }
    }
}
