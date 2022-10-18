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

    public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
    {
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