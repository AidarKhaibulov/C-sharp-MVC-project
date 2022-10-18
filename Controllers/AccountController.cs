﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers;
/// <summary>
/// User's accounts controller. Performs LogIn, Registration logic. Provides account's information, allows edit and delete data.
/// </summary>

public class AccountController:Controller
{
    private ApplicationContext db;
    private static List<UserViewModel> users;
    //private static ApplicationContext db = new (options: new DbContextOptions<ApplicationContext>());
    
    private AccountService accountService;

    public AccountController(ApplicationContext context,AccountService _accountService)
    {
        accountService = _accountService; 
        db = context;
        users= db.User.ToList();
    }
    /// <summary>
    /// Login page
    /// </summary>
    public IActionResult Login()
    {
        return PartialView("Login");
    }
    /// <summary>
    /// Login and redirection to main page
    /// </summary>
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var account = accountService.Login(username, password);
        if (account != null)
        {
            HttpContext.Session.SetString("username",username);
            return Redirect("/Home/Main");
        }
        ViewBag.msg = "Неправильно введен логин или пароль";
        return View("Login");
    }
    /// <summary>
    /// Main account page, contains information about current session and LogOut button
    /// </summary>
    public IActionResult Welcome()
    {
        ViewBag.username = HttpContext.Session.GetString("username");
        return View();
    }
    /// <summary>
    /// LogOut current session
    /// </summary>
    /// <returns>Returns LogIn page</returns>
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("username");
        return RedirectToAction("Login");
    }
    /// <summary>
    /// New account registration page
    /// </summary>
    public IActionResult Register()
    {
        return View();
    }
    /// <summary>
    /// Registration new account and redirect to main page
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Register(UserViewModel userViewModel)
    {
        db.User.Add(userViewModel);
        await db.SaveChangesAsync();
        //If registration performs via existing session then redirect to Main page.
        if(HttpContext.Session.Keys.Contains("username"))
            return Redirect("/Home/Main");
        //If registration performs without existing session then redirect to Login page.
        return Redirect("/");
    }
    /// <summary>
    /// Page contains list of all users with Edit and Delete options
    /// </summary>
    public async Task<IActionResult> UsersList()
    {
        if(HttpContext.Session.Keys.Contains("username"))
            return View(await db.User.ToListAsync());
        //TODO: create custom authorize filter and use it instead checking HttpContext.Session.Keys.Contains("username") all the time
        return NotFound();
    }
    /// <summary>
    /// Edit user page, gets id from UsersList page and opens page for editing information
    /// </summary>
    public async Task<IActionResult> Edit(int? id)
    {
        if(id!=null)
        {
            UserViewModel? user = await db.User.FirstOrDefaultAsync(p=>p.Id==id);
            if (user != null) return View(user);
        }
        return NotFound();
    }
    /// <summary>
    /// Method for update new users information 
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Edit(UserViewModel user)
    {
        db.User.Update(user);
        await db.SaveChangesAsync();
        return RedirectToAction("UsersList");
    }
    /// <summary>
    /// Deleting user method, gets id from UsersList page and delete selected user
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id != null)
        {
            UserViewModel? user = await db.User.FirstOrDefaultAsync(p => p.Id == id);
            if (user != null)
            {
                db.User.Remove(user);
                await db.SaveChangesAsync();
                return RedirectToAction("UsersList");
            }
        }
        return NotFound();
    }
}