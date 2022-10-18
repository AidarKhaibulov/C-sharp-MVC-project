namespace WebMVC.Models;

/// <summary>
/// Model for user's favorite products
/// </summary>
public class FavoriteProductsViewModel
{
    public int UserId { get; set; }
    public List<ProductViewModel> Products { get; set; }
}