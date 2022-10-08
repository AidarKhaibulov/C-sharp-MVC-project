using Microsoft.EntityFrameworkCore;
using WebMVC;

var builder = WebApplication.CreateBuilder(args);

// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapGet("/", (ApplicationContext db) => db.User.ToList());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();