namespace Retail.Discount.App.Console.Models;

/// <summary>
///   Represents a cart item.
/// </summary>
public class Cart : BaseModel
{
    /// <summary>
    ///  Gets or sets the user identifier.
    /// </summary>
    public string UserId { get; set; } = null!;
    
    /// <summary>
    ///  Gets or sets the user name.
    /// </summary>
    
    public string Username { get; set; } = null!;
    
    /// <summary>
    ///  Gets or sets whether the cart has been completed.
    /// </summary>
    
    public bool IsCompleted { get; set; }
    
    /// <summary>
    ///  Gets or sets the items in the cart.
    /// </summary>
    
    public List<CartItem> Items { get; set; } = new();
}