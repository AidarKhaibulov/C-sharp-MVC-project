using System.Data.Entity;
using System.Text.RegularExpressions;
using Npgsql;
using WebMVC.Controllers;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Repository;

public class CartRepository:ICartRepository<Cart>
{
    private ApplicationContext _context;
    public CartRepository(ApplicationContext context)
    {
        _context = context;
    }
    public List<ProductViewModel> GetProducts(HomeController.CartType sourceCartType,int userId)
    {
        string scriptPath = null;
        switch (sourceCartType)
        {
            case HomeController.CartType.FavoriteProducts:
                scriptPath = "GetFavoriteProductsFromUserCart.sql";
                break;
            case HomeController.CartType.RecentlyWatched:
                scriptPath = "GetProductsFromRecentlyWatchedCart.sql";
                break;
        }
        List<ProductViewModel> products = new List<ProductViewModel>();
        string script = File.ReadAllText(@"Scripts/"+scriptPath);
        script = Regex.Replace(script, @"ToReplace", userId.ToString());
        using (var sqlConn = new NpgsqlConnection(ApplicationContext.ConnectionString))
        {
            sqlConn.Open();
            NpgsqlCommand sqlCmd = new NpgsqlCommand(script, sqlConn);
            NpgsqlDataReader reader = sqlCmd.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                    products.Add(_context.Product.FirstOrDefault(x=>x.Id==reader.GetInt32(0)));
            reader.CloseAsync();
        }
        return products;
    }

    public void AddProductToCart(HomeController.CartType targetCartType, string productId, int? userId)
    {
        switch (targetCartType)
        {
            case HomeController.CartType.FavoriteProducts:
                string script1 = File.ReadAllText(@"Scripts/FavoriteProductsInsert.sql");
                script1 = Regex.Replace(script1, @"ToReplace", userId.ToString());
                string script2 = File.ReadAllText(@"Scripts/ProductFavoriteProductsRELATIONInsert.sql");
                script2 = Regex.Replace(script2, @"FirstReplace", productId);
                using (var sqlConn = new NpgsqlConnection(ApplicationContext.ConnectionString))
                {
                    sqlConn.Open();
                    NpgsqlCommand sqlCmd = new NpgsqlCommand(script1, sqlConn);
                    sqlCmd.ExecuteNonQuery();
                    Cart cart = _context.Cart.FirstOrDefault(p =>
                        p.UserId == userId)!;
                    script2 = Regex.Replace(script2, @"SecondReplace", cart.Id.ToString());
                    sqlCmd = new NpgsqlCommand(script2, sqlConn);
                    sqlCmd.ExecuteNonQuery();
                    //get element's count in recently watched cart
                    script1=System.IO.File.ReadAllText(@"Scripts/GetProductsCountInCart.sql");
                    script1 = Regex.Replace(script1, @"ToReplace", userId.ToString());
                    sqlCmd = new NpgsqlCommand(script1, sqlConn);
                    NpgsqlDataReader reader = sqlCmd.ExecuteReader();
                    reader.Read();
                    Console.WriteLine(reader.GetValue(0).ToString());
                    reader.CloseAsync();
                }
                break;
            case HomeController.CartType.RecentlyWatched:
                script1 = File.ReadAllText(@"Scripts/RecentlyCartInsert.sql");
                script1 = Regex.Replace(script1, @"ToReplace", userId.ToString());
                script2 = File.ReadAllText(@"Scripts/ProductRecentlyWatchedRELATIONInsert.sql");
                script2 = Regex.Replace(script2, @"FirstReplace", productId);
                using (var sqlConn = new NpgsqlConnection(ApplicationContext.ConnectionString))
                {
                    sqlConn.Open();
                    NpgsqlCommand sqlCmd = new NpgsqlCommand(script1, sqlConn);
                    sqlCmd.ExecuteNonQuery();
                    RecentlyWatchedCartViewModel recentlyWatchedCart = _context.RecentlyWatchedCart.FirstOrDefault(p =>
                        p.UserId == userId)!;
                    script2 = Regex.Replace(script2, @"SecondReplace", recentlyWatchedCart.Id.ToString());
                    sqlCmd = new NpgsqlCommand(script2, sqlConn);
                    sqlCmd.ExecuteNonQuery();
                }
                break;
        }
    }

    public Cart GetProductById(int id)
    {
        throw new NotImplementedException();
    }

    public void DeleteProductFromCart(int productId,int UserId)
    {
        Cart cart =  _context.Cart.FirstOrDefault(x => x.UserId == UserId)!;
        ProductCartRelationViewModel relation =
            _context.ProductCartRelation.FirstOrDefault(x => x.FavoriteProductsId == cart.Id && x.ProductId==productId);
        if (relation != null) _context.ProductCartRelation.RemoveRange(relation);
        _context.SaveChangesAsync();
    }
}