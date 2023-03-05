using IRPr.Models;
using IRPr.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Claims;

namespace IRPr.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
		private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, IProductService productService)
		{
			_logger = logger;
			_userManager = userManager;
			_productService = productService;
		}

		[Authorize]
		public IActionResult Index()
		{
            List<Category> categories = _productService.GetAllCategories();
            ViewData["Categories"] = categories;

            IEnumerable<Product> products = _productService.GetAllProducts();
			return View(products);
		}

		public IActionResult AddToCart(int id)
		{
			//Product product=_productService.GetProductById(id);

			string userID= User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<CartItem> cartItemsForUser = _productService.GetCartItems(userID);
			CartItem newCartItem = new CartItem()
			{
				UserID = userID,
				ProductID = id,
				Quantity = 1
			};
			_productService.AddCartItem(newCartItem);

            return RedirectToAction("Index");
		}

		public IActionResult Cart()
		{
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

			List<Product> products=new List<Product>(); 

            List<CartItem> cartItemsForUser = _productService.GetCartItems(userID);
			foreach(CartItem cartItem in cartItemsForUser)
			{
                Product product = _productService.GetProductById(cartItem.ProductID);
				products.Add(product);
			}

            return View(products);
		}

		public IActionResult Create()
		{
			List<Category> categories = _productService.GetAllCategories();
            List<SelectListItem> items = categories.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.Name.ToString(),
                    Value = a.categoryID.ToString(),
                    Selected = false
                };
            });
			ViewData["Categories"]= items;
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Product product, ICollection<IFormFile> imageFiles, IFormFile docuFile)
		{
			_productService.AddProduct(product, imageFiles, docuFile);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Checkout()
		{
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _productService.DeleteShoppingCart(userID);

			return RedirectToAction("Cart");
		}

        public FileResult Download(int id)
        {
            Product product = _productService.GetProductById(id);

            byte[] fileBytes = System.IO.File.ReadAllBytes(@"wwwroot/documents/"+product.DocumentName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, product.DocumentName);
        }
    }
}