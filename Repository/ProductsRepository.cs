using System.Linq.Expressions;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Repository;

public class ProductsRepository:IRepository<ProductViewModel>
{
    private ApplicationContext _context;

    public ProductsRepository(ApplicationContext context)
    {
        _context = context;  
    }

    public void Add(ProductViewModel entity)
    {
        throw new NotImplementedException();
    }

    public ICollection<ProductViewModel> Get()
    {
        return  _context.Product.OrderBy(x => x.Id).ToList();
    }

    public ProductViewModel GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(ProductViewModel entity)
    {
        throw new NotImplementedException();
    }
}