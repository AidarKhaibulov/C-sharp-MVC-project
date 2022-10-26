using WebMVC.Controllers;
using WebMVC.Models;

namespace WebMVC.Interfaces;

public interface ICartRepository<TEntity> where TEntity :class
{
    List<ProductViewModel> GetProducts(HomeController.CartType sourceCartType,int userId);
    void AddProductToCart(HomeController.CartType targetCartType, string productId, int? userId);
    TEntity GetProductById(int id);
    /// <summary>
    /// Delete specified product from specified users cart
    /// </summary>
    /// <param name="productId">Product which need to delete from cart</param>
    /// <param name="UserId">From this user cart product will be deleted</param>
    void DeleteProductFromCart(int productId, int UserId);
}