using NSubstitute;
using Retail.Discount.App.Console.Models;
using Retail.Discount.App.Console.Repositories.Interfaces;
using Retail.Discount.App.Console.Services.Providers;

namespace Retail.Discount.Console.Tests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_ValidInput_UserAddedSuccessfully()
    {
        // Arrange
        var userRepository = Substitute.For<IUserRepository>();
        
        userRepository.AddUser(Arg.Any<User>());
        
        userRepository.FindUser(Arg.Any<string>()).Returns(new User(){Username = "name"});
        var userService = new UserService(userRepository);

        // Act
        userService.AddUser("name", false, false, DateTime.Parse("1998-05-31"));

        // Assert
        userRepository.Received(1).AddUser(Arg.Any<User>());
    }
    
    
}