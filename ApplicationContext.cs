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
    public DbSet<UserViewModel> User => Set<UserViewModel>();
    public DbSet<ProductViewModel> Product => Set<ProductViewModel>(); 
    public DbSet<FavoriteProductsViewModel> FavoriteProducts => Set<FavoriteProductsViewModel>();
    public DbSet<ProductFavoriteProductsRELATIONViewModel> ProductFavoriteProducts => Set<ProductFavoriteProductsRELATIONViewModel>();

    public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ProductFavoriteProductsRELATIONViewModel>().HasKey(i => new { i.FavoriteProductsId, i.ProductId });
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;" +
                                 "Port=5432;" +
                                 "Database=Test;" +
                                 "Username=postgres;" +
                                 "Password=sitis");
    }
}