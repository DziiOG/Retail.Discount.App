using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Retail.Discount.App.Console.Repositories.Interfaces;
using Retail.Discount.App.Console.Repositories.Providers;
using Retail.Discount.App.Console.Services.InteractionImpls;
using Retail.Discount.App.Console.Services.Interfaces;
using Retail.Discount.App.Console.Services.Providers;

namespace Retail.Discount.App.Console;

internal class Program
{
    async static Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IConsoleCommunicatorService, ConsoleCommunicatorService>();
                services.AddSingleton<ICartRepository, CartRepository>();
                services.AddSingleton<IUserRepository, UserRepository>();
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IBillService, BillService>();
                
            })
            .Build();

        using (var serviceScope = host.Services.CreateScope())
        {
            var serviceProvider = serviceScope.ServiceProvider;

            var userService = serviceProvider.GetRequiredService<IUserService>();
            var billService = serviceProvider.GetRequiredService<IBillService>();
            var consoleCommunicatorService = serviceProvider.GetRequiredService<IConsoleCommunicatorService>();

            System.Console.WriteLine("Welcome to Retail Discount Application!");

            bool continueRunning = true;
            while (continueRunning)
            {
                System.Console.WriteLine("\nOptions:");
                System.Console.WriteLine("1. Add a user");
                System.Console.WriteLine("2. Calculate Bill Discount on Items");
                System.Console.WriteLine("3. Exit");

                System.Console.Write("Enter your choice: ");
                string? choice = System.Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        userService.AddUser(consoleCommunicatorService.GetUsernameInput(), consoleCommunicatorService.GetYesNoInput("Is user an employee? (y/n): ") == "y", consoleCommunicatorService.GetYesNoInput("Is user an affiliate? (y/n): ") == "y", consoleCommunicatorService.GetJoinDateInput());
                        break;
                    case "2":
                        billService.GetDiscountedAmount(consoleCommunicatorService.GetUserByUsername(), consoleCommunicatorService.GetCartItems());
                        break;
                    case "3":
                        continueRunning = false;
                        break;
                    default:
                        System.Console.WriteLine("Invalid choice. Please enter a valid option (1, 2, or 3).");
                        break;
                }
            }
        }

        await host.RunAsync();
    }
}