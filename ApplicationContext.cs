using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

namespace WebMVC;
/// <summary>
/// Data base context
/// </summary>
public class ApplicationContext:IdentityDbContext<IdentityUser>
{
    public static string ConnectionString
    {
        get
        {
            return "Host=localhost;" +
                   "Port=5432;" +
                   "Database=Test;" +
                   "Username=postgres;" +
                   "Password=sitis";
        }
    }

    public DbSet<UserViewModel> User => Set<UserViewModel>();
    public DbSet<ProductViewModel> Product => Set<ProductViewModel>(); 
    public DbSet<Cart> Cart => Set<Cart>();
    public DbSet<ProductCartRelationViewModel> ProductCartRelation => Set<ProductCartRelationViewModel>();

    public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ProductCartRelationViewModel>().HasKey(i => new { i.FavoriteProductsId, i.ProductId });
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString);
    }
}