using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IRPr.Models
{
	public class ShopContext : IdentityDbContext<IdentityUser>
	{
		public ShopContext(DbContextOptions<ShopContext> options)
			: base(options)
		{ }

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
	}
}
