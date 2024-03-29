using Retail.Discount.App.Console.Models;
using Retail.Discount.App.Console.Repositories.Interfaces;

namespace Retail.Discount.App.Console.Repositories.Providers;

/// <summary>
///  Represents a cart repository.
/// </summary>
public class CartRepository : ICartRepository
{
    /// <summary>
    ///  List of carts added.
    /// </summary>
    private readonly List<Cart> _carts = new();

    /// <summary>
    ///  Adds a cart.
    /// </summary>
    /// <param name="cart">new cart object to be added</param>
    public void AddCart(Cart cart)
    {
        _carts.Add(cart);
    }
    
    /// <summary>
    ///  Finds a user's cart by username.
    /// </summary>
    /// <param name="username">user's username</param>
    /// <returns>returns null if cart was not found otherwise cart object</returns>
    public Cart? FindCartByUsername(string username)
    {
        return _carts.Find(u => u.Username == username);
    }
}