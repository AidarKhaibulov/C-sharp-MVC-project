using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Controllers;

    /// <summary>
    /// Controller for showing main application content such as products, navigation buttons for search
    /// products, filters, list of recently viewed products and recommendations
    /// </summary>

    public class HomeController : Controller
    {
        private readonly IRepository<ProductViewModel> _productsRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ApplicationContext _context;

        public enum CartType
        {
            FavoriteProducts,
            RecentlyWatched
        }

        public HomeController(
            IRepository<ProductViewModel> productsRepository,
            ICartRepository cartRepository,
            ApplicationContext context
            )
        {
            _productsRepository = productsRepository;   
            _cartRepository = cartRepository;
            _context=context;
        }
        
        

        /// <summary>
        /// Represents main page with products list
        /// </summary>
        public async Task<IActionResult> Main()
        {
            return View(_productsRepository.Get());
        }
        public async Task<IActionResult> Filter()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Main(string name,int minPrice, int maxPrice,string categoryName)
        {
            if (categoryName == null)
                return View(_productsRepository.Get());
            return View(_cartRepository.FilterProducts(name, minPrice, maxPrice, categoryName));
            //return NotFound("ЭТОТ ЗАПРОС СОДЕРЖИТ ПОСЛЕДОВАТЕЛЬНОСТЬ ДВОЙНОГО ПРЕОБРАЗОВАНИЯ СИМВОЛОВ!");

        }
        
        public async Task<IActionResult> Delete(int userId)
        {
            _cartRepository.DeleteProductFromCart(CartType.FavoriteProducts,userId,(int) HttpContext.Session.GetInt32("username"));
            return RedirectToAction("Main");
        }
        public async Task<IActionResult> AddToFavorite(string userId)
        {
            _cartRepository.AddProductToCart(CartType.FavoriteProducts,userId,HttpContext.Session.GetInt32("username"));
            return RedirectToAction("Main");
        }
        public async Task<IActionResult> ProductInfo(string userId)
        {
            int currentUserId = (int) HttpContext.Session.GetInt32("username");
            int numberOfRowsInRecentlyCart =
                _cartRepository.GetProductsCount(CartType.RecentlyWatched, currentUserId);
            userId = Regex.Match(userId, @"\d+").ToString();
            _cartRepository.AddProductToCart(CartType.RecentlyWatched,userId,currentUserId);
            ProductViewModel product = ( _context.Product.FirstOrDefault(x =>
                x.Id == Convert.ToInt32(userId)))!;
            if (numberOfRowsInRecentlyCart > 3)
            {
                int productToDeleteId = _cartRepository.GetFirstProductIdInCart(CartType.RecentlyWatched,
                    currentUserId);
                _cartRepository.DeleteProductFromCart(CartType.RecentlyWatched,productToDeleteId,currentUserId);
            }

            /*List<ProductViewModel> result = _cartRepository.GetProducts(CartType.RecentlyWatched,
                (int) HttpContext.Session.GetInt32("username"));
            result.Insert(0,product);*/
            return View(product);
        }
        public async Task<IActionResult> Favorite()
        {
            return View(_cartRepository.GetProducts(CartType.FavoriteProducts,
                (int) HttpContext.Session.GetInt32("username")));
        }
        public async Task<IActionResult> RecentlyWatched()
        {
            return View(_cartRepository.GetProducts(CartType.RecentlyWatched,
                (int) HttpContext.Session.GetInt32("username")));
        }
        
    }
