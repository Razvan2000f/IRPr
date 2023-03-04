using IRPr.Models;
using IRPr.Repositories.Interfaces;

namespace IRPr.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ShopContext shopContext) : base(shopContext)
        {
        }

        public Product GetProductById(int id)
        {
           List<Product> products= FindByCondition(product => product.ID == id).ToList();
            return products[0];
        }
    }
}
