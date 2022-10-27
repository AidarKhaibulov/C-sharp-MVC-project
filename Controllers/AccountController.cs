using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers;
/// <summary>
/// User's accounts controller. Performs LogIn, Registration logic. Provides account's information, allows edit and delete data.
/// </summary>
/*[Route("api/[controller]")]
[ApiController]*/
public class AccountController:Controller
{
    private ApplicationContext db;
    private static List<UserViewModel> users;

    private AccountService accountService;

    public AccountController(ApplicationContext context,AccountService _accountService)
    {
        accountService = _accountService; 
        db = context;
        users= db.User.ToList();
    }
    public IActionResult Login()
    {
        return PartialView("Login");
    }
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var account = accountService.Login(username, password);
        if (account != null)
        {
            HttpContext.Session.SetInt32("username",account.Id);
            return Redirect("Home/Main");
        }
        ViewBag.msg = "Неправильно введен логин или пароль";
        return View("Login");
    }
    /// <summary>
    /// Main account page, contains information about current session and LogOut button
    /// </summary>
    public IActionResult Welcome()
    {
        ViewBag.username = HttpContext.Session.GetInt32("username");
        return View();
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("username");
        return RedirectToAction("Login");
    }
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(UserViewModel userViewModel)
    {
        db.User.Add(userViewModel);
        await db.SaveChangesAsync();
        if(HttpContext.Session.Keys.Contains("username"))
            return Redirect("/Home/Main");
        return Redirect("/");
    }
    public async Task<IActionResult> UsersList()
    {
        if(HttpContext.Session.Keys.Contains("username"))
            return View(await db.User.ToListAsync());
        //TODO: create custom authorize filter and use it instead checking HttpContext.Session.Keys.Contains("username") all the time
        return NotFound();
    }
    public async Task<IActionResult> Edit(int? productId)
    {
        if(productId!=null)
        {
            UserViewModel? user = await db.User.FirstOrDefaultAsync(p=>p.Id==productId);
            if (user != null) return View(user);
        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Edit(UserViewModel user)
    {
        db.User.Update(user);
        await db.SaveChangesAsync();
        return RedirectToAction("UsersList");
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int? productId)
    {
        Cart cart = await db.Cart.FirstOrDefaultAsync(x => x.UserId == productId);
       
        List<ProductCartRelationViewModel> relation = db.ProductCartRelation.Where(x
            => x.FavoriteProductsId == cart.Id).ToList();
        db.ProductCartRelation.RemoveRange(relation);
        db.Cart.Remove(cart);
        db.User.Remove(db.User.FirstOrDefault(x => x.Id == productId));
        await db.SaveChangesAsync();
        return RedirectToAction("UsersList");
    }
    
}