namespace WebMVC.Models;

/// <summary>
/// Model for user's favorite products
/// </summary>
public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; } 
    public UserViewModel User { get; set; }
    public ICollection<ProductCartRelationViewModel> Products { get; set; }
}