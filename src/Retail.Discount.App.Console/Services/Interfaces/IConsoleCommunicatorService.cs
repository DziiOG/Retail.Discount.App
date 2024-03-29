using Retail.Discount.App.Console.Models;

namespace Retail.Discount.App.Console.Services.Interfaces;

public interface IConsoleCommunicatorService
{
    public DateTime GetJoinDateInput();
    public string GetUsernameInput();
    public string GetYesNoInput(string prompt);
    public List<CartItem> GetCartItems();
    public User GetUserByUsername();
}