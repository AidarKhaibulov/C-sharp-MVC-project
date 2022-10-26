using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Repository;

public class UserRepository: IRepository <UserViewModel>
{
    private ApplicationContext _context;
    private DbSet<UserViewModel> _dbSet;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
        _dbSet = context.Set<UserViewModel>();
    }

    public ICollection<UserViewModel> Get()
    {
        return  _context.User.OrderBy(x => x.Id).ToList();
    }

    public UserViewModel GetById(int id)
    {
        return _context.User.FirstOrDefault(x => x.Id == id)!;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}