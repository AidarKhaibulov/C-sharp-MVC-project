using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers;

public class UserController : Controller
{
    private static List<UserViewModel> dogs = new List<UserViewModel>();
    // GET
    public IActionResult UsersList()
    {
        return View(dogs);
    }
    public IActionResult Create()
    {
        var dogVm = new UserViewModel();
        return View(dogVm); 
    }

    public IActionResult CreateUser(UserViewModel dogViewModel)
    {
        dogs.Add(dogViewModel);
       return RedirectToAction(nameof(UsersList));
    }
    public IActionResult Hello()
    {
        return View();
    }
}