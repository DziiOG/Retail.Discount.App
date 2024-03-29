namespace Retail.Discount.App.Console.Models;

/// <summary>
///   Represents a cart item.
/// </summary>
public class CartItem : BaseModel
{
    /// <summary>
    ///  Gets or sets whether the cart item is a grocery item or not.
    /// </summary>
    public bool IsGrocery { get; set; }
    
    /// <summary>
    ///  Gets or sets the price of the item.
    /// </summary>
    public double Price { get; set; }
    
    /// <summary>
    ///  Gets or sets the discounted price of the item.
    /// </summary>
    public double DiscountedPrice { get; set; }
    
    /// <summary>
    ///  Gets or sets the quantity of the item.
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    ///  Gets or sets the name of the item.
    /// </summary>
    public string ItemName { get; set; } = null!;
}