using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

namespace WebMVC.Controllers;

public class UserController : Controller
{
    private ApplicationContext db;

    private static List<UserViewModel> users;
    //private static ApplicationContext db = new (options: new DbContextOptions<ApplicationContext>());
    

    public UserController(ApplicationContext context)
    {
        db = context;
        users= db.User.ToList();
    }
    public IActionResult UsersList()
    {
        return View(users);
    }
    public IActionResult Create()
    {
        var userVm = new UserViewModel();
        return View(userVm); 
    }

    public IActionResult CreateUser(UserViewModel userViewModel)
    {
        users.Add(userViewModel);
        
            UserViewModel newUser = userViewModel;
            db.User.AddRange(newUser);
            db.SaveChanges();
        
       return RedirectToAction(nameof(UsersList));
    }
    public IActionResult Delete()
    {
        return View();
    }
    public IActionResult Hello()
    {
        return View();
    }
}