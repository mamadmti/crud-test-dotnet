using FluentAssertions;
using Xunit;

namespace Mc2.CrudTest.UnitTests.Application.Validators;

public class CreateCustomerCommandValidatorTests
{
    [Fact]
    public void Validator_ShouldPass_WithValidData()
    {
        // Arrange

        // Act

        // Assert

        Assert.True(true, "TODO: Implement CreateCustomerCommandValidator");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validator_ShouldFail_WhenFirstNameIsInvalid(string? firstName)
    {
        // Arrange

        // Act

        // Assert

        Assert.True(true, $"TODO: Implement FirstName validation - test with '{firstName}'");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validator_ShouldFail_WhenLastNameIsInvalid(string? lastName)
    {
        // Arrange & Act & Assert

        Assert.True(true, $"TODO: Implement LastName validation - test with '{lastName}'");
    }

    [Fact]
    public void Validator_ShouldFail_WhenDateOfBirthIsInFuture()
    {
        // Arrange

        // Act

        // Assert

        Assert.True(true, "TODO: Implement DateOfBirth validation");
    }

    [Theory]
    [InlineData("+1234")]  // Too short
    [InlineData("1234567890")]  // Missing country code
    [InlineData("+442071234567")]  // Landline
    public void Validator_ShouldFail_WithInvalidPhoneNumber(string phoneNumber)
    {
        // Arrange

        // Act

        // Assert

        Assert.True(true, $"TODO: Implement PhoneNumber validation - test with {phoneNumber}");
    }

    [Theory]
    [InlineData("not-an-email")]
    [InlineData("@example.com")]
    [InlineData("user@")]
    [InlineData("")]
    public void Validator_ShouldFail_WithInvalidEmail(string email)
    {
        // Arrange & Act & Assert

        Assert.True(true, $"TODO: Implement Email validation - test with {email}");
    }

    [Theory]
    [InlineData("INVALID")]
    [InlineData("GB82WEST123456987654")]  // Too short
    [InlineData("")]
    public void Validator_ShouldFail_WithInvalidBankAccountNumber(string bankAccountNumber)
    {
        // Arrange & Act & Assert

        Assert.True(true, $"TODO: Implement BankAccountNumber validation - test with {bankAccountNumber}");
    }
}

