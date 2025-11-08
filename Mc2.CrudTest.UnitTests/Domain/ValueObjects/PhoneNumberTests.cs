using FluentAssertions;
using Xunit;
using Mc2.CrudTest.Domain.ValueObjects;

namespace Mc2.CrudTest.UnitTests.Domain.ValueObjects;

public class PhoneNumberTests
{
    [Theory]
    [InlineData("+14155552671")]  // US mobile
    [InlineData("+447911123456")] // UK mobile
    [InlineData("+33612345678")]  // France mobile
    [InlineData("+61412345678")]  // Australia mobile
    public void PhoneNumber_ShouldBeCreated_WithValidMobileNumber(string validMobileNumber)
    {
        // Act
        var phoneNumber = new PhoneNumber(validMobileNumber);

        // Assert
        phoneNumber.Should().NotBeNull();
        phoneNumber.Value.Should().Be(validMobileNumber);
    }

    [Theory]
    [InlineData("+1234")]  // Too short
    [InlineData("1234567890")]  // Missing country code
    [InlineData("abc123")]  // Invalid characters
    [InlineData("+442071234567")]  // UK landline (not mobile)
    public void PhoneNumber_ShouldNotBeCreated_WithInvalidNumber(string invalidNumber)
    {
        // Act & Assert
        Action act = () => new PhoneNumber(invalidNumber);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void PhoneNumber_ShouldReject_LandlineNumbers()
    {
        // Arrange
        var landlineNumber = "+442071234567"; // UK landline

        // Act & Assert
        Action act = () => new PhoneNumber(landlineNumber);
        act.Should().Throw<ArgumentException>()
            .WithMessage("*Only mobile phone numbers are allowed*");
    }

    [Fact]
    public void PhoneNumber_ShouldStore_InMinimalSpaceFormat()
    {
        // Arrange & Act
        var phoneNumber = new PhoneNumber("+14155552671");

        // Assert
        phoneNumber.Value.Should().Be("+14155552671"); // E.164 format is minimal
        phoneNumber.Value.Length.Should().Be(12); // Compact format
    }

    [Fact]
    public void PhoneNumber_ShouldNormalize_ToE164Format()
    {
        // Arrange
        var phoneWithSpaces = "+1 415 555 2671";
        var expectedNormalized = "+14155552671";

        // Act
        var phoneNumber = new PhoneNumber(phoneWithSpaces);

        // Assert
        phoneNumber.Value.Should().Be(expectedNormalized);
    }
}

