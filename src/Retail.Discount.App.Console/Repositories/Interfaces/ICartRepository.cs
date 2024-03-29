using Retail.Discount.App.Console.Models;

namespace Retail.Discount.App.Console.Repositories.Interfaces;

/// <summary>
///  Represents a cart repository interface.
/// </summary>
public interface ICartRepository
{
    /// <summary>
    ///  Adds a cart.
    /// </summary>
    /// <param name="cart">new cart object to be added</param>
    void AddCart(Cart cart);

    /// <summary>
    ///  Finds a user's cart by username.
    /// </summary>
    /// <param name="username">user's username</param>
    /// <returns>returns null if cart was not found otherwise cart object</returns>
    Cart? FindCartByUsername(string username);
}