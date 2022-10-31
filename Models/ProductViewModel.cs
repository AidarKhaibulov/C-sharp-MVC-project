using System.Net.Mime;
using Npgsql.Internal.TypeHandlers.NumericHandlers;

namespace WebMVC.Models;
/// <summary>
/// Model for products
/// </summary>
public class ProductViewModel
{
    public int Id { get; set; }
    public int? CategoryId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string? Size { get; set; }
    public string Platform { get; set; }
    public string Image { get; set; }
    public ICollection<ProductCartRelationViewModel> Cart { get; set; }
    public Categories? Category { get; set; }
}