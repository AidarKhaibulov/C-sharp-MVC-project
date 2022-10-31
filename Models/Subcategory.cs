namespace WebMVC.Models;

public class Subcategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Categories> Categories { get; set; }
}