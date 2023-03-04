using IRPr.Models;
using IRPr.Repositories.Interfaces;

namespace IRPr.Repositories
{
    public class CategoryRepository: RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ShopContext shopContext) : base(shopContext)
        {
        }
    }
}
