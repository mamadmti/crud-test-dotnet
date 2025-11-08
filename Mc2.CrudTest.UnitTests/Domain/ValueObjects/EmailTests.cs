using FluentAssertions;
using Xunit;
using Mc2.CrudTest.Domain.ValueObjects;

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
        // Act
        var email = new Email(validEmail);

        // Assert
        email.Should().NotBeNull();
        email.Value.Should().Be(validEmail.ToLowerInvariant());
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
        // Act & Assert
        Action act = () => new Email(invalidEmail);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Email_ShouldBeEqual_WhenValuesAreSame()
    {
        // Arrange
        var email1 = new Email("test@example.com");
        var email2 = new Email("test@example.com");

        // Act & Assert
        email1.Should().Be(email2);
        (email1 == email2).Should().BeTrue();
    }

    [Fact]
    public void Email_ShouldBeCaseInsensitive()
    {
        // Arrange
        var email1 = new Email("Test@Example.COM");
        var email2 = new Email("test@example.com");

        // Act & Assert
        email1.Should().Be(email2);
        email1.Value.Should().Be("test@example.com");
    }
}

