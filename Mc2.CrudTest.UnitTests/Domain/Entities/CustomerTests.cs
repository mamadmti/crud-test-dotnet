using FluentAssertions;
using Xunit;

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
        var phoneNumber = "+14155552671";
        var email = "john.doe@example.com";
        var bankAccountNumber = "GB82WEST12345698765432";

        Assert.True(true, "TODO: Implement Customer entity and complete this test");
    }

    [Fact]
    public void Customer_ShouldNotBeCreated_WithNullFirstName()
    {
        // Arrange
        string? firstName = null;
        var lastName = "Doe";
        var dateOfBirth = new DateTime(1990, 1, 15);

        // Act & Assert

        Assert.True(true, "TODO: Implement validation and complete this test");
    }

    [Fact]
    public void Customer_ShouldNotBeCreated_WithEmptyLastName()
    {
        // Arrange
        var firstName = "John";
        var lastName = "";
        var dateOfBirth = new DateTime(1990, 1, 15);

        // Act & Assert

        Assert.True(true, "TODO: Implement validation and complete this test");
    }

    [Fact]
    public void Customer_ShouldNotBeCreated_WithFutureDateOfBirth()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var dateOfBirth = DateTime.Now.AddDays(1);

        // Act & Assert

        Assert.True(true, "TODO: Implement validation and complete this test");
    }

    [Fact]
    public void Customer_ShouldUpdate_PhoneNumber()
    {
        // Arrange
        var newPhoneNumber = "+14155559876";

        // Act

        // Assert

        Assert.True(true, "TODO: Implement Customer entity and complete this test");
    }

    [Fact]
    public void Customer_ShouldUpdate_Email()
    {
        // Arrange
        var newEmail = "newemail@example.com";

        // Act

        // Assert

        Assert.True(true, "TODO: Implement Customer entity and complete this test");
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


        Assert.True(true, "TODO: Implement uniqueness validation");
    }

    [Fact]
    public void Customer_ShouldBeUnique_ByEmail()
    {
        // Arrange
        var email = "unique@example.com";


        Assert.True(true, "TODO: Implement email uniqueness validation");
    }
}

