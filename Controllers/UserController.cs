using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

namespace WebMVC.Controllers;
//[Authorize]
//[ApiController]
//[Route("[controller]")]
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
    public async Task<IActionResult> UsersList()
    {
        return View(await db.User.ToListAsync());
    }
    public IActionResult Create()
    {
        return View(); 
    }
[HttpPost]
    public async Task<IActionResult> Create(UserViewModel userViewModel)
    {
        db.User.Add(userViewModel);
        await db.SaveChangesAsync();
        return RedirectToAction("UsersList");
    }
    //delete data
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
    //edit data
    public async Task<IActionResult> Edit(int? id)
    {
        if(id!=null)
        {
            UserViewModel? user = await db.User.FirstOrDefaultAsync(p=>p.Id==id);
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
    public IActionResult Hello()
    {
        return View();
    }
}