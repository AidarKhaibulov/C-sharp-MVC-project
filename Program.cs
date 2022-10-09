using Microsoft.EntityFrameworkCore;
using WebMVC;
using WebMVC.Models;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapGet("/users", async (ApplicationContext db) => await db.User.ToListAsync());

app.MapGet("/users/{id:int}", async (int id, ApplicationContext db) =>
{
    // получаем пользователя по id
    UserViewModel? user = await db.User.FirstOrDefaultAsync(u => u.Id == id);
 
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
 
    // если пользователь найден, отправляем его
    return Results.Json(user);
});

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

app.MapPost("/api/users", async (UserViewModel user, ApplicationContext db) =>
{
    // добавляем пользователя в массив
    await db.User.AddAsync(user);
    await db.SaveChangesAsync();
    return user;
});
app.Run();