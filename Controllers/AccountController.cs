using Microsoft.AspNetCore.Mvc;
using WebMVC.Interfaces;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers;
/// <summary>
/// User's accounts controller. Performs LogIn, Registration logic. Provides account's information, allows edit and delete data.
/// </summary>
public class AccountController:Controller
{
    private readonly IRepository<UserViewModel> _usersRepository;
    private readonly AccountService _accountService;

    public AccountController(IRepository<UserViewModel> usersRepository,AccountService _accountService)
    {
        this._accountService = _accountService;
        _usersRepository = usersRepository;
    }
    public IActionResult Login()
    {
        return PartialView("Login");
    }
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var account = _accountService.Login(username, password);
        if (account != null)
        {
            HttpContext.Session.SetInt32("username",account.Id);
            return Redirect("Home/Main");
        }
        ViewBag.msg = "Неправильно введен логин или пароль";
        return View("Login");
    }
    public IActionResult Welcome()
    {
        return View(_usersRepository.GetById((int) HttpContext.Session.GetInt32("username")));
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
        _usersRepository.Add(userViewModel);
        if(HttpContext.Session.Keys.Contains("username"))
            return Redirect("/Home/Main");
        return Redirect("/");
    }
    public async Task<IActionResult> UsersList()
    {
        if(HttpContext.Session.Keys.Contains("username"))
            return View( _usersRepository.Get());
        //TODO: create custom authorize filter and use it instead checking HttpContext.Session.Keys.Contains("username") all the time
        return NotFound();
    }
    public async Task<IActionResult> Edit(int? userId)
    {
        if(userId!=null)
        {
            UserViewModel? user = _usersRepository.GetById((int) userId);
            if (user != null) return View(user);
        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Edit(UserViewModel user)
    {
        Console.WriteLine(user.Id);
        _usersRepository.Update(user);
        return RedirectToAction("UsersList");
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int? userId)
    {
       _usersRepository.Delete((int) userId);
        return RedirectToAction("UsersList");
    }
}