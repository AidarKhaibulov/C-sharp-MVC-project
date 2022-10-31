namespace WebMVC.Models;

public class Categories
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SubCategory { get; set; }
    public List<ProductViewModel> Product { get; set; }
}