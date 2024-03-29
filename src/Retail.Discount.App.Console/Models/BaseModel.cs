namespace Retail.Discount.App.Console.Models;

/// <summary>
///   Represents a cart item.
/// </summary>
public class BaseModel
{
    /// <summary>
    ///  Gets or sets the identifier.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString("N");

    /// <summary>
    ///  Gets or sets the created at date.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    
    /// <summary>
    ///  Gets or sets the created at date.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}