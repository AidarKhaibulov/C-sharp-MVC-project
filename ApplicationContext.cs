using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

namespace WebMVC;

public class ApplicationContext:IdentityDbContext<IdentityUser>
{
    public DbSet<UserViewModel> User => Set<UserViewModel>();

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