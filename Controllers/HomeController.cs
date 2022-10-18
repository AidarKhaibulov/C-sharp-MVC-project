using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

namespace WebMVC.Controllers;

    /// <summary>
    /// Controller for showing main application content such as products, navigation buttons for search
    /// products, filters, list of recently viewed products and recommendations
    /// </summary>
    public class HomeController : Controller
    {
        private ApplicationContext db;
        private static List<ProductViewModel> products;
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ApplicationContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            db = context;
            products= db.Product.ToList();
        }
        /// <summary>
        /// JWT registration form. DELETE
        /// </summary>
        public IActionResult Index()
        {
            //DELETE
            return View();
        }
        /// <summary>
        /// Represents main page with products list
        /// </summary>
        public async Task<IActionResult> Main()
        {
            return View(await db.Product.ToListAsync());
        }
        /// <summary>
        /// Represents favorite products page
        /// </summary>
        public async Task<IActionResult> Favorite()
        {
            // return View(await db.User.ToListAsync());
           return View(db.User.Include(u=>u.FavoriteProducts).ToList());
        }
    }