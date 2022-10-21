using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
        public async Task<IActionResult> Favorite()
        {
            List<string> products = new List<string>();
            string script = System.IO.File.ReadAllText(@"Scripts/GetFavoriteProductsFromUserCart.sql");
            script = Regex.Replace(script, @"ToReplace", HttpContext.Session.GetInt32("username").ToString());
            using (var sqlConn = new NpgsqlConnection("Host=localhost;" +
                                                      "Port=5432;" +
                                                      "Database=Test;" +
                                                      "Username=postgres;" +
                                                      "Password=sitis"))
            {
                sqlConn.Open();
                NpgsqlCommand sqlCmd = new NpgsqlCommand(script, sqlConn);
                NpgsqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                        products.Add(reader.GetString(0));
                await reader.CloseAsync();
            }
            return View(products);
        }
        public async Task<IActionResult> AddToFavorite(int? id)
        {
            string script1 = System.IO.File.ReadAllText(@"Scripts/FavoriteProductsInsert.sql");
            script1 = Regex.Replace(script1, @"ToReplace", HttpContext.Session.GetInt32("username").ToString());
            string script2 = System.IO.File.ReadAllText(@"Scripts/ProductFavoriteProductsRELATIONInsert.sql");
            script2 = Regex.Replace(script2, @"FirstReplace", id.ToString());
            using (var sqlConn = new NpgsqlConnection(ApplicationContext.ConnectionString))
            {
                sqlConn.Open();
                NpgsqlCommand sqlCmd = new NpgsqlCommand(script1, sqlConn);
                sqlCmd.ExecuteNonQuery();
                Cart cart = (await db.Cart.FirstOrDefaultAsync(p =>
                    p.UserId == HttpContext.Session.GetInt32("username")))!;
                script2 = Regex.Replace(script2, @"SecondReplace", cart.Id.ToString());
                sqlCmd = new NpgsqlCommand(script2, sqlConn);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Main");
        }
        
    }
