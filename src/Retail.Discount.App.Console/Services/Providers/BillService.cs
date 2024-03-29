using Retail.Discount.App.Console.Models;
using Retail.Discount.App.Console.Repositories.Interfaces;
using Retail.Discount.App.Console.Services.Interfaces;

namespace Retail.Discount.App.Console.Services.Providers;

public class BillService : IBillService
{

    private readonly ICartRepository _cartRepository;
    
    public BillService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }
    public double GetDiscountedAmount(User user, List<CartItem> items)
    {

    Cart cart = new Cart { UserId = user.Id, Username = user.Username, IsCompleted = false, UpdatedAt = DateTime.UtcNow, Items = items};

    double totalAmount = cart.Items.Sum(item => item.Price * item.Quantity);

    double discountedAmount = GetDiscountedAmount(totalAmount, items, user);

    System.Console.WriteLine("\nCart Details:");
    System.Console.WriteLine($"User: {cart.Username}");
    System.Console.WriteLine("Items:");
    foreach (var item in cart.Items)
    {
        System.Console.WriteLine($"- {item.ItemName}: ${item.Price} x {item.Quantity}");
    }
    System.Console.WriteLine($"Total Amount: ${totalAmount:F2}");
    System.Console.WriteLine($"Discounted Amount: ${discountedAmount:F2}");
    System.Console.WriteLine($"Final Amount to be Paid: ${(totalAmount - discountedAmount):F2}");
    
    cart.IsCompleted = true;
    _cartRepository.AddCart(cart);

    return discountedAmount;
    }

    private double GetDiscountedAmount(double totalAmount, List<CartItem> items, User user)
    {

        double discountPercentage = 0;

        if (items.Exists(item => !item.IsGrocery))
        {
            if (user.IsEmployee)
            {
                discountPercentage = Math.Max(discountPercentage, 0.3); 
            }
            else if (user.IsAffiliate)
            {
                discountPercentage = Math.Max(discountPercentage, 0.1); 
            }
            else if (IsLoyalCustomer(user))
            {
                discountPercentage = Math.Max(discountPercentage, 0.05); 
            }
        }

        int discountPer100Dollars = (int)(totalAmount / 100) * 5; 
        
        discountPercentage += discountPer100Dollars / totalAmount;

        double discountedAmount = totalAmount * (1 - discountPercentage);

        return discountedAmount;

    }

    private bool IsLoyalCustomer(User user)
    {
        return (DateTime.UtcNow - user.JoinDate).TotalDays >= (365 * 2);
    }
}