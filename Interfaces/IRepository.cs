using System.Linq.Expressions;
using WebMVC.Models;

namespace WebMVC.Interfaces;

public interface IRepository<TEntity> where TEntity :class
{
    ICollection<TEntity> Get();
    TEntity GetById(int id);
    void Delete(int id);
}