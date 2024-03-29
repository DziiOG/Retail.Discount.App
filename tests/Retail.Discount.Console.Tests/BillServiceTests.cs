using NSubstitute;
using Retail.Discount.App.Console.Models;
using Retail.Discount.App.Console.Repositories.Interfaces;
using Retail.Discount.App.Console.Services.Providers;

namespace Retail.Discount.Console.Tests;

public class BillServiceTests
{
    [Fact]
    public void GetDiscountedAmount_NoDiscount_ReturnsTotalAmount()
    {
        // Arrange
        var cartRepository = Substitute.For<ICartRepository>();
        var billService = new BillService(cartRepository);
        var user = new User() { IsEmployee = false, IsAffiliate = false, JoinDate = DateTime.UtcNow };
        var items = new List<CartItem>
        {
            new CartItem { Price = 10, Quantity = 2, IsGrocery = true }
        };
        var totalAmount = 20.0;

        // Act
        var discountedAmount = billService.GetDiscountedAmount(user, items);

        // Assert
        Assert.Equal(totalAmount, discountedAmount);
    }

    [Fact]
    public void GetDiscountedAmount_EmployeeDiscount_ReturnsDiscountedAmount()
    {
        // Arrange
        var cartRepository = Substitute.For<ICartRepository>();
        var billService = new BillService(cartRepository);
        var user = new User { IsEmployee = true, IsAffiliate = false, JoinDate = DateTime.UtcNow };
        var items = new List<CartItem>
        {
            new CartItem { Price = 10, Quantity = 2, IsGrocery = false }
        };
        // Total amount = $20, Employee discount = 30%
        var expectedDiscountedAmount = 14.0;

        // Act
        var discountedAmount = billService.GetDiscountedAmount(user, items);

        // Assert
        Assert.Equal(expectedDiscountedAmount, discountedAmount);
    }

    [Fact]
    public void GetDiscountedAmount_AffiliateDiscount_ReturnsDiscountedAmount()
    {
        // Arrange
        var cartRepository = Substitute.For<ICartRepository>();
        var billService = new BillService(cartRepository);
        var user = new User { IsEmployee = false, IsAffiliate = true, JoinDate = DateTime.UtcNow };
        var items = new List<CartItem>
        {
            new CartItem { Price = 10, Quantity = 2, IsGrocery = false }
        };
        // Total amount = $20, Affiliate discount = 10%
        var expectedDiscountedAmount = 18.0;

        // Act
        var discountedAmount = billService.GetDiscountedAmount(user, items);

        // Assert
        Assert.Equal(expectedDiscountedAmount, discountedAmount);
    }

    [Fact]
    public void GetDiscountedAmount_LoyalCustomerDiscount_ReturnsDiscountedAmount()
    {
        // Arrange
        var cartRepository = Substitute.For<ICartRepository>();
        var billService = new BillService(cartRepository);
        var user = new User { IsEmployee = false, IsAffiliate = false, JoinDate = DateTime.UtcNow.AddYears(-3) };
        var items = new List<CartItem>
        {
            new CartItem { Price = 10, Quantity = 2, IsGrocery = false }
        };
        // Total amount = $20, Loyal customer discount = 5%
        var expectedDiscountedAmount = 19.0;

        // Act
        var discountedAmount = billService.GetDiscountedAmount(user, items);

        // Assert
        Assert.Equal(expectedDiscountedAmount, discountedAmount);
    }

    [Fact]
    public void GetDiscountedAmount_EmployeeAndLoyalCustomerDiscount_ReturnsDiscountedAmountWithMaximumDiscount()
    {
        // Arrange
        var cartRepository = Substitute.For<ICartRepository>();
        var billService = new BillService(cartRepository);
        var user = new User { IsEmployee = true, IsAffiliate = false, JoinDate = DateTime.UtcNow.AddYears(-3) };
        var items = new List<CartItem>
        {
            new CartItem { Price = 10, Quantity = 2, IsGrocery = false }
        };
        // Total amount = $20, Employee discount = 30%
        var expectedDiscountedAmount = 14.0;

        // Act
        var discountedAmount = billService.GetDiscountedAmount(user, items);

        // Assert
        Assert.Equal(expectedDiscountedAmount, discountedAmount);
    }

    [Fact]
    public void GetDiscountedAmount_MultipleDiscounts_ReturnsDiscountedAmountWithMaximumDiscount()
    {
        // Arrange
        var cartRepository = Substitute.For<ICartRepository>();
        var billService = new BillService(cartRepository);
        var user = new User { IsEmployee = true, IsAffiliate = true, JoinDate = DateTime.UtcNow.AddYears(-3) };
        var items = new List<CartItem>
        {
            new CartItem { Price = 10, Quantity = 2, IsGrocery = false }
        };
        // Total amount = $20, Employee discount = 30%, Affiliate discount = 10%, Loyal customer discount = 5%
        // Employee discount should take precedence over other discounts
        var expectedDiscountedAmount = 14.0;

        // Act
        var discountedAmount = billService.GetDiscountedAmount(user, items);

        // Assert
        Assert.Equal(expectedDiscountedAmount, discountedAmount);
    }

    [Fact]
    public void GetDiscountedAmount_NoDiscountForGroceries_ReturnsTotalAmount()
    {
        // Arrange
        var cartRepository = Substitute.For<ICartRepository>();
        var billService = new BillService(cartRepository);
        var user = new User { IsEmployee = true, IsAffiliate = true, JoinDate = DateTime.UtcNow.AddYears(-3) };
        var items = new List<CartItem>
        {
            new CartItem { Price = 10, Quantity = 2, IsGrocery = true }
        };
        // Total amount = $20, No discount for groceries
        var expectedDiscountedAmount = 20.0;

        // Act
        var discountedAmount = billService.GetDiscountedAmount(user, items);

        // Assert
        Assert.Equal(expectedDiscountedAmount, discountedAmount);
    }

}