using System.Linq.Expressions;
using WebMVC.Models;

namespace WebMVC.Interfaces;

public interface IRepository<TEntity> where TEntity :class
{
    void Add(TEntity entity);
    ICollection<TEntity> Get();
    TEntity GetById(int id);
    void Delete(int id);
    void Update(TEntity entity);
}