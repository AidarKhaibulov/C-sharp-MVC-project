using Microsoft.EntityFrameworkCore;

namespace WebMVC.Models;

public class ProductCartRelationViewModel
{
    public int ProductId { get; set; }
    public ProductViewModel Product { get; set; }
    public int FavoriteProductsId { get; set; }
    public Cart FavoriteProducts { get; set; }
}