using IRPr.Models;
using IRPr.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

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

		public IActionResult Index()
		{
            List<Category> categories = _productService.GetAllCategories();
            ViewData["Categories"] = categories;

            IEnumerable<Product> products = _productService.GetAllProducts();
			return View(products);
		}

		public IActionResult Privacy()
		{
			return View();
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
        public IActionResult Create([FromForm] Product product, ICollection<IFormFile> imageFiles)
		{
			_productService.AddProduct(product, imageFiles);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}