using Retail.Discount.App.Console.Models;

namespace Retail.Discount.App.Console.Services.Interfaces;

public interface IUserService
{
    void AddUser(string username, bool isEmployee, bool isAffiliate, DateTime joinDate);
}