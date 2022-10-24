namespace WebMVC.Models;

public class ProductRecentlyWatchedRelationViewModel
{
    public int ProductId { get; set; }
    public ProductViewModel Product { get; set; }
    public int RecentlyWatchedCartId { get; set; }
    public RecentlyWatchedCartViewModel RecentlyWatchedCart { get; set; }
}