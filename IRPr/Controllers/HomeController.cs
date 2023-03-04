using IRPr.Models;
using IRPr.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
			IEnumerable<Product> products = _productService.GetAllProducts();
			return View(products);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Create()
		{

            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Product product, ICollection<IFormFile> imageFiles)
		{
			_productService.AddProduct(product, imageFiles);
            return RedirectToAction("Feed");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}