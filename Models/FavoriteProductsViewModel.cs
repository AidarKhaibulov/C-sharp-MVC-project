using System.ComponentModel.DataAnnotations.Schema;

namespace WebMVC.Models;

/// <summary>
/// Model for user's favorite products
/// </summary>
public class FavoriteProductsViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }     //foreign key
    public UserViewModel User { get; set; }
    //public List<ProductViewModel> Products { get; set; }
    public string Products { get; set; }
}