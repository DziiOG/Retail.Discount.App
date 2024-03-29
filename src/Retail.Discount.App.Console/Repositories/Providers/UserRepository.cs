using Retail.Discount.App.Console.Models;
using Retail.Discount.App.Console.Repositories.Interfaces;

namespace Retail.Discount.App.Console.Repositories.Providers;

/// <summary>
///  Represents a user repository.
/// </summary>
public class UserRepository : IUserRepository
{

    /// <summary>
    ///  List of users added.
    /// </summary>
    private readonly List<User> _users = new();

    /// <summary>
    ///  Adds a user.
    /// </summary>
    /// <param name="user">new user object to be added</param>
    public void AddUser(User user)
    {
        _users.Add(user);
    }

    /// <summary>
    ///  Finds a user by username.
    /// </summary>
    /// <param name="username">user's username</param>
    /// <returns>returns null if user was not found otherwise user object</returns>
    public User? FindUser(string username)
    {
        return _users.Find(u => u.Username == username);
    }

    /// <summary>
    ///  Deletes a user by username.
    /// </summary>
    /// <param name="username">user's username</param>
    /// <returns> returns true if user is deleted successfully otherwise false </returns>
    public bool DeleteUser(string username)
    {
        var user = _users.Find(u => u.Id == username);
        return user != null && _users.Remove(user);
    }
}