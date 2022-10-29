namespace WebMVC.Models;

public class Categories
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public List<ProductViewModel> Product { get; set; }
}