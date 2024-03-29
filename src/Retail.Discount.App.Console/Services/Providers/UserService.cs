using Retail.Discount.App.Console.Models;
using Retail.Discount.App.Console.Repositories.Interfaces;
using Retail.Discount.App.Console.Services.Interfaces;
namespace Retail.Discount.App.Console.Services.Providers;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public void AddUser(string username, bool isEmployee, bool isAffiliate, DateTime joinDate)
    {
        var user = new User
        {
            Username = username,
            IsEmployee = isEmployee,
            IsAffiliate = isAffiliate,
            JoinDate = joinDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _userRepository.AddUser(user);
        System.Console.WriteLine("User added successfully.");
        System.Console.WriteLine(user.Username);
    }
    
}