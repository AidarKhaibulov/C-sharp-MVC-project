namespace WebMVC.Models;
/// <summary>
/// Model for users
/// </summary>
public class UserViewModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}