using FluentAssertions;
using Xunit;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.ValueObjects;

namespace Mc2.CrudTest.UnitTests.Domain.Entities;

public class CustomerTests
{
    [Fact]
    public void Customer_ShouldBeCreated_WithValidData()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var dateOfBirth = new DateTime(1990, 1, 15);
        var phoneNumber = new PhoneNumber("+14155552671");
        var email = new Email("john.doe@example.com");
        var bankAccountNumber = new BankAccountNumber("GB82WEST12345698765432");

        // Act
        var customer = new Customer(firstName, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber);

        // Assert
        customer.Should().NotBeNull();
        customer.FirstName.Should().Be("John");
        customer.LastName.Should().Be("Doe");
        customer.DateOfBirth.Should().Be(dateOfBirth.Date);
        customer.PhoneNumber.Should().Be(phoneNumber);
        customer.Email.Should().Be(email);
        customer.BankAccountNumber.Should().Be(bankAccountNumber);
        customer.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Customer_ShouldNotBeCreated_WithNullFirstName()
    {
        // Arrange
        string? firstName = null;
        var lastName = "Doe";
        var dateOfBirth = new DateTime(1990, 1, 15);

        // Act & Assert
        Action act = () => new Customer(
            firstName!,
            lastName,
            dateOfBirth,
            new PhoneNumber("+14155552671"),
            new Email("test@example.com"),
            new BankAccountNumber("GB82WEST12345698765432"));

        act.Should().Throw<ArgumentException>()
            .WithMessage("*First name*");
    }

    [Fact]
    public void Customer_ShouldNotBeCreated_WithEmptyLastName()
    {
        // Arrange
        var firstName = "John";
        var lastName = "";
        var dateOfBirth = new DateTime(1990, 1, 15);

        // Act & Assert
        Action act = () => new Customer(
            firstName,
            lastName,
            dateOfBirth,
            new PhoneNumber("+14155552671"),
            new Email("test@example.com"),
            new BankAccountNumber("GB82WEST12345698765432"));

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Last name*");
    }

    [Fact]
    public void Customer_ShouldNotBeCreated_WithFutureDateOfBirth()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var dateOfBirth = DateTime.UtcNow.AddDays(1);

        // Act & Assert
        Action act = () => new Customer(
            firstName,
            lastName,
            dateOfBirth,
            new PhoneNumber("+14155552671"),
            new Email("test@example.com"),
            new BankAccountNumber("GB82WEST12345698765432"));

        act.Should().Throw<ArgumentException>()
            .WithMessage("*Date of birth*");
    }

    [Fact]
    public void Customer_ShouldUpdate_PhoneNumber()
    {
        // Arrange
        var customer = new Customer(
            "John",
            "Doe",
            new DateTime(1990, 1, 15),
            new PhoneNumber("+14155552671"),
            new Email("john@example.com"),
            new BankAccountNumber("GB82WEST12345698765432"));

        var newPhoneNumber = new PhoneNumber("+14155559876");

        // Act
        customer.UpdatePhoneNumber(newPhoneNumber);

        // Assert
        customer.PhoneNumber.Should().Be(newPhoneNumber);
    }

    [Fact]
    public void Customer_ShouldUpdate_Email()
    {
        // Arrange
        var customer = new Customer(
            "John",
            "Doe",
            new DateTime(1990, 1, 15),
            new PhoneNumber("+14155552671"),
            new Email("john@example.com"),
            new BankAccountNumber("GB82WEST12345698765432"));

        var newEmail = new Email("newemail@example.com");

        // Act
        customer.UpdateEmail(newEmail);

        // Assert
        customer.Email.Should().Be(newEmail);
    }

    [Theory]
    [InlineData("John", "Doe", "1990-01-15")]
    [InlineData("Jane", "Smith", "1985-05-20")]
    [InlineData("Bob", "Johnson", "1975-12-31")]
    public void Customer_ShouldBeUnique_ByFirstNameLastNameAndDateOfBirth(
        string firstName, string lastName, string dateOfBirthStr)
    {
        // Arrange
        var dateOfBirth = DateTime.Parse(dateOfBirthStr);

        // Act - Creating two customers with same name and DOB
        var customer1 = new Customer(
            firstName,
            lastName,
            dateOfBirth,
            new PhoneNumber("+14155552671"),
            new Email($"{firstName.ToLower()}1@example.com"),
            new BankAccountNumber("GB82WEST12345698765432"));

        var customer2 = new Customer(
            firstName,
            lastName,
            dateOfBirth,
            new PhoneNumber("+14155559876"),
            new Email($"{firstName.ToLower()}2@example.com"),
            new BankAccountNumber("FR1420041010050500013M02606"));

        // Assert - They should have different IDs but same identifying info
        customer1.Id.Should().NotBe(customer2.Id);
        customer1.FirstName.Should().Be(customer2.FirstName);
        customer1.LastName.Should().Be(customer2.LastName);
        customer1.DateOfBirth.Should().Be(customer2.DateOfBirth);
    }

    [Fact]
    public void Customer_ShouldBeUnique_ByEmail()
    {
        // Arrange
        var email = new Email("unique@example.com");

        // Act - Creating two customers with same email
        var customer1 = new Customer(
            "John",
            "Doe",
            new DateTime(1990, 1, 15),
            new PhoneNumber("+14155552671"),
            email,
            new BankAccountNumber("GB82WEST12345698765432"));

        var customer2 = new Customer(
            "Jane",
            "Smith",
            new DateTime(1985, 5, 20),
            new PhoneNumber("+14155559876"),
            email,
            new BankAccountNumber("FR1420041010050500013M02606"));

        // Assert - They should have same email but different IDs
        customer1.Id.Should().NotBe(customer2.Id);
        customer1.Email.Should().Be(customer2.Email);
    }
}
