using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Repository;

public class UserRepository: IRepository <UserViewModel>
{
    private ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public void Add(UserViewModel entity)
    {
        _context.User.Add(entity);
        _context.SaveChangesAsync();
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
        /*Cart cart = _context.Cart.FirstOrDefault(x => x.UserId == id)!;
        RecentlyWatchedCartViewModel recentlyWatchedCart = _context.RecentlyWatchedCart.FirstOrDefault(x => x.UserId == id)!;
        if (cart != null)
        {
            List<ProductCartRelationViewModel> relation1 = _context.ProductCartRelation.Where(x
                => x.FavoriteProductsId == cart.Id).ToList();
            _context.ProductCartRelation.RemoveRange(relation1);
            _context.Cart.Remove(cart);
        }
        if (recentlyWatchedCart != null)
        {
            List<ProductRecentlyWatchedRelationViewModel> relation2 = _context.ProductRecentlyWatchedRelation.Where(x
                => x.RecentlyWatchedCartId == recentlyWatchedCart.Id).ToList();  
            _context.ProductRecentlyWatchedRelation.RemoveRange(relation2);
            _context.RecentlyWatchedCart.Remove(recentlyWatchedCart);
        }
        _context.User.Remove(_context.User.FirstOrDefault(x => x.Id == id)!);
        _context.SaveChanges();*/
        _context.User.Remove(_context.User.FirstOrDefault(x => x.Id == id)!);
        _context.SaveChanges();
    }

    public void Update(UserViewModel entity)
    {
        _context.User.Update(entity); 
        _context.SaveChangesAsync();
    }
}