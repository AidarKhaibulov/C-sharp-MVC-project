using WebMVC.Models;

namespace WebMVC.Services;
/// <summary>
/// Interface-service for LogIn
/// </summary>
public interface AccountService
{
    public UserViewModel Login(string username,  string password);
    
}