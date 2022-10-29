using WebMVC.Controllers;
using WebMVC.Models;

namespace WebMVC.Interfaces;

public interface ICartRepository
{
    List<ProductViewModel> GetProducts(HomeController.CartType sourceCartType,int userId);
    int GetProductsCount(HomeController.CartType targetCartType, int? userId);
    int GetFirstProductIdInCart(HomeController.CartType sourceCartType,int userId);
    List<ProductViewModel> FilterProducts(int minValue=0, int maxValue=Int32.MaxValue,string categoryName="");
    
    void AddProductToCart(HomeController.CartType targetCartType, string productId, int? userId);
    
    void DeleteProductFromCart(HomeController.CartType targetCartType,int productId, int UserId);
}