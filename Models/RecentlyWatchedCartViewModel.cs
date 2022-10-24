namespace WebMVC.Models;

public class RecentlyWatchedCartViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }     //foreign key
    public UserViewModel User { get; set; }
    public ICollection<ProductRecentlyWatchedRelationViewModel> Products { get; set; }
}