using Retail.Discount.App.Console.Models;

namespace Retail.Discount.App.Console.Repositories.Interfaces;

/// <summary>
///  Represents a user repository interface.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    ///  Adds a user.
    /// </summary>
    /// <param name="user">new user object to be added</param>
    void AddUser(User user);
    
    /// <summary>
    ///  Finds a user by username.
    /// </summary>
    /// <param name="username">user's username</param>
    /// <returns>returns null if user was not found otherwise user object</returns>
    User? FindUser(string username);
    
    /// <summary>
    ///  Deletes a user by username.
    /// </summary>
    /// <param name="username">user's username</param>
    /// <returns> returns true if user is deleted successfully otherwise false </returns>
    bool DeleteUser(string username);
}