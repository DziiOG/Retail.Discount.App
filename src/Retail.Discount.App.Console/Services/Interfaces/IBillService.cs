using Retail.Discount.App.Console.Models;

namespace Retail.Discount.App.Console.Services.Interfaces;

public interface IBillService
{
    double GetDiscountedAmount(User user, List<CartItem> items);
}