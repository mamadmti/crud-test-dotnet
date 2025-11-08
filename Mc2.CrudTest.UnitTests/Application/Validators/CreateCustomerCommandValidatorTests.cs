using FluentAssertions;
using Xunit;
using Mc2.CrudTest.Application.Commands;
using Mc2.CrudTest.Application.Validators;

namespace Mc2.CrudTest.UnitTests.Application.Validators;

public class CreateCustomerCommandValidatorTests
{
    private readonly CreateCustomerCommandValidator _validator;

    public CreateCustomerCommandValidatorTests()
    {
        _validator = new CreateCustomerCommandValidator();
    }

    [Fact]
    public void Validator_ShouldPass_WithValidData()
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = "+14155552671",
            Email = "john.doe@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validator_ShouldFail_WhenFirstNameIsInvalid(string? firstName)
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            FirstName = firstName!,
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = "+14155552671",
            Email = "john@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == "FirstName");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validator_ShouldFail_WhenLastNameIsInvalid(string? lastName)
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            FirstName = "John",
            LastName = lastName!,
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = "+14155552671",
            Email = "john@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == "LastName");
    }

    [Fact]
    public void Validator_ShouldFail_WhenDateOfBirthIsInFuture()
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.UtcNow.AddDays(1),
            PhoneNumber = "+14155552671",
            Email = "john@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == "DateOfBirth");
    }

    [Theory]
    [InlineData("")]
    public void Validator_ShouldFail_WithInvalidPhoneNumber(string phoneNumber)
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = phoneNumber,
            Email = "john@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == "PhoneNumber");
    }

    [Theory]
    [InlineData("not-an-email")]
    [InlineData("@example.com")]
    [InlineData("user@")]
    [InlineData("")]
    public void Validator_ShouldFail_WithInvalidEmail(string email)
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = "+14155552671",
            Email = email,
            BankAccountNumber = "GB82WEST12345698765432"
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == "Email");
    }

    [Theory]
    [InlineData("")]
    public void Validator_ShouldFail_WithInvalidBankAccountNumber(string bankAccountNumber)
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = "+14155552671",
            Email = "john@example.com",
            BankAccountNumber = bankAccountNumber
        };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x => x.PropertyName == "BankAccountNumber");
    }
}

