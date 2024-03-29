using System.Globalization;
using System.Text.RegularExpressions;
using Retail.Discount.App.Console.Models;
using Retail.Discount.App.Console.Repositories.Interfaces;
using Retail.Discount.App.Console.Services.Interfaces;

namespace Retail.Discount.App.Console.Services.InteractionImpls;

public class ConsoleCommunicatorService : IConsoleCommunicatorService
{
    private readonly IUserRepository _userRepository;
    
    public ConsoleCommunicatorService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public DateTime GetJoinDateInput()
    {
        var isIncorrect = true;
        var result = DateTime.MinValue;
        
        while (isIncorrect)
        {
            System.Console.Write("Enter join date (yyyy-MM-dd): ");
            
            var input = System.Console.ReadLine();

            if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                isIncorrect = false;
                result = date;
                continue;
            }
            
            System.Console.WriteLine("Invalid input. Please enter a valid date.");
        }

        return result;
    }
    public string GetUsernameInput()
    {
        var isIncorrect = true;
        var result = string.Empty;
        while (isIncorrect)
        {
            System.Console.Write("Enter username: ");
            
            var input = System.Console.ReadLine();
       
            var usernameRegex = new Regex("^[a-z0-9._]{1,30}$");
            
            if (!usernameRegex.IsMatch(input ?? string.Empty))
            {
                System.Console.WriteLine("Invalid username. It should only contain lowercase letters, numbers, periods, and underscores, and be 1-30 characters long.");
                continue;
            }

            var existingUser = _userRepository.FindUser(input?.ToLower() ?? string.Empty);
            
            if (existingUser is not null)
            {
                System.Console.WriteLine("Invalid input.");
                continue;
            }
            result = input ?? string.Empty;
            isIncorrect = false;
        }

        return result;
    }
    public string GetYesNoInput(string prompt)
    {
        var isIncorrect = true;
        var result = string.Empty;
        while (isIncorrect)
        {
            System.Console.Write(prompt);
            var input = System.Console.ReadLine()?.ToLower();
            switch (input)
            {
                case "y":
                case "n":
                    isIncorrect = false;
                    result = input;
                    break;
                default:
                    System.Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                    break;
            }
        }
        return result;
    }

    public List<CartItem> GetCartItems()
    { 
        List<CartItem> items = new List<CartItem>();
    bool continueAddingItems = true;
    
    while (continueAddingItems)
    {
        // Get item details
        System.Console.Write("Enter item name: ");
        string? itemName = System.Console.ReadLine();
        
        while (string.IsNullOrWhiteSpace(itemName))
        {
            System.Console.WriteLine("Invalid name. Please enter valid item name.");
            System.Console.Write("Enter item name: "); 
        }

        System.Console.Write("Enter item price: ");
        
        double itemPrice;
        
        while (!double.TryParse(System.Console.ReadLine(), out itemPrice) || itemPrice < 0)
        {
            System.Console.WriteLine("Invalid price. Price must be a positive number.");
            System.Console.Write("Enter item price: ");
        }

        System.Console.Write("Enter item quantity: ");
        int itemQuantity;
        
        while (!int.TryParse(System.Console.ReadLine(), out itemQuantity) || itemQuantity < 0)
        {
            System.Console.WriteLine("Invalid quantity. Quantity must be a positive integer.");
            System.Console.Write("Enter item quantity: ");
        }

        bool isGrocery = GetYesNoInput("Is it a grocery item? (y/n): ") == "y";
        
        var existingItem = items.Find(i =>i.ItemName.ToLower().Equals(itemName.ToLower(), StringComparison.OrdinalIgnoreCase));
        
        if (existingItem != null)
        {
            existingItem.Quantity += itemQuantity; 
        }
        else
        {
           
            items.Add(new () { ItemName = itemName, Price = itemPrice, Quantity = itemQuantity, IsGrocery = isGrocery });
        }

        continueAddingItems = GetYesNoInput("Do you want to add more items? (y/n): ") == "y";
    }
    return items;
    }

    public User GetUserByUsername()
    {
        User? user = null;
        System.Console.Write("Enter user's name: ");
    
        while (user is null)
        {
            var userName = System.Console.ReadLine();
            user = _userRepository.FindUser(userName ?? string.Empty);
            if (user is null)
            {
                System.Console.WriteLine("User not found. Please enter a valid username.");
            }
        }
        return user;
    }
}