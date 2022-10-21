namespace WebMVC.Models;

public class ProductFavoriteProductsRELATIONViewModel
{
    public int ProductId { get; set; }
    public ProductViewModel Product { get; set; }
    public int FavoriteProductsId { get; set; }
    public FavoriteProductsViewModel FavoriteProducts { get; set; }
}