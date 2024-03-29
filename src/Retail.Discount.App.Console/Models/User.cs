namespace Retail.Discount.App.Console.Models;

/// <summary>
///  Represents a user.
/// </summary>
public class User: BaseModel
{
    /// <summary>
    ///  Gets or sets the user name.
    /// </summary>
    public string Username { get; set; } = null!;
    
    /// <summary>
    ///  Gets or sets whether the user is an employee.
    /// </summary>
    public bool IsEmployee { get; set; }
    
    /// <summary>
    ///  Gets or sets whether the user is an affiliate.
    /// </summary>
    public bool IsAffiliate { get; set; }
    
    /// <summary>
    ///  Gets or sets whether the user is a customer.
    /// </summary>
    public DateTime JoinDate { get; set; }
}