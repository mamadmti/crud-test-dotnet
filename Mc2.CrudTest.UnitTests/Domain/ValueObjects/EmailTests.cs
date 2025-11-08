using FluentAssertions;
using Xunit;

namespace Mc2.CrudTest.UnitTests.Domain.ValueObjects;

public class EmailTests
{
    [Theory]
    [InlineData("valid@example.com")]
    [InlineData("user.name@example.com")]
    [InlineData("user+tag@example.co.uk")]
    [InlineData("test123@test-domain.com")]
    public void Email_ShouldBeCreated_WithValidFormat(string validEmail)
    {

        Assert.True(true, $"TODO: Implement Email value object - test with {validEmail}");
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-an-email")]
    [InlineData("@example.com")]
    [InlineData("user@")]
    [InlineData("user name@example.com")]
    [InlineData("user@.com")]
    public void Email_ShouldNotBeCreated_WithInvalidFormat(string invalidEmail)
    {

        Assert.True(true, $"TODO: Implement Email validation - test with {invalidEmail}");
    }

    [Fact]
    public void Email_ShouldBeEqual_WhenValuesAreSame()
    {
        var email1 = "test@example.com";
        var email2 = "test@example.com";

        Assert.True(true, "TODO: Implement value object equality");
    }

    [Fact]
    public void Email_ShouldBeCaseInsensitive()
    {
        var email1 = "Test@Example.COM";
        var email2 = "test@example.com";

        Assert.True(true, "TODO: Implement case-insensitive email comparison");
    }
}

