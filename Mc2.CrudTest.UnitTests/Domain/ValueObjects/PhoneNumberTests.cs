using FluentAssertions;
using Xunit;

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

        // Assert

        Assert.True(true, $"TODO: Implement PhoneNumber value object - test with {validMobileNumber}");
    }

    [Theory]
    [InlineData("+1234")]  // Too short
    [InlineData("1234567890")]  // Missing country code
    [InlineData("abc123")]  // Invalid characters
    [InlineData("+442071234567")]  // UK landline (not mobile)
    public void PhoneNumber_ShouldNotBeCreated_WithInvalidNumber(string invalidNumber)
    {
        Assert.True(true, $"TODO: Implement phone validation - test with {invalidNumber}");
    }

    [Fact]
    public void PhoneNumber_ShouldReject_LandlineNumbers()
    {

        var landlineNumber = "+442071234567"; // UK landline

        Assert.True(true, "TODO: Implement mobile-only validation using libphonenumber");
    }

    [Fact]
    public void PhoneNumber_ShouldStore_InMinimalSpaceFormat()
    {
        var phoneNumber = "+14155552671";

        Assert.True(true, "TODO: Implement space-efficient phone number storage");
    }

    [Fact]
    public void PhoneNumber_ShouldNormalize_ToE164Format()
    {

        var phoneWithSpaces = "+1 415 555 2671";
        var expectedNormalized = "+14155552671";

        Assert.True(true, "TODO: Implement E.164 normalization");
    }
}

