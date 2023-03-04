using IRPr.Models;
using IRPr.Repositories.Interfaces;

namespace IRPr.Repositories
{
    public class CartItemRepository : RepositoryBase<CartItem>, ICartItemRepository
    {
        public CartItemRepository(ShopContext shopContext) : base(shopContext)
        {
        }
    }
}
