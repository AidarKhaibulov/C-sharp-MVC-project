using WebMVC.Models;

namespace WebMVC.Services;
/// <summary>
/// Contains DB connection and Login check method
/// </summary>
public class AccountServiceImplementation:AccountService
{
    private ApplicationContext db;
    private static List<UserViewModel> accounts;

    public AccountServiceImplementation(ApplicationContext context)
    {
        db = context;
        accounts= db.User.ToList();
    }
/// <summary>
/// Checks if there is such a user in data base
/// </summary>
/// <returns>User with specified username and password</returns>
    public UserViewModel Login(string username, string password)
    {
        return accounts.SingleOrDefault(account => account.Username == username && account.Password == password);
    }
}