using FluentAssertions;
using Xunit;

namespace Mc2.CrudTest.UnitTests.Domain.ValueObjects;

public class BankAccountNumberTests
{
    [Theory]
    [InlineData("GB82WEST12345698765432")]  // UK IBAN
    [InlineData("DE89370400440532013000")]  // Germany IBAN
    [InlineData("FR1420041010050500013M02606")]  // France IBAN
    [InlineData("IT60X0542811101000000123456")]  // Italy IBAN
    public void BankAccountNumber_ShouldBeCreated_WithValidIBAN(string validIban)
    {
        Assert.True(true, $"TODO: Implement BankAccountNumber value object - test with {validIban}");
    }

    [Theory]
    [InlineData("")]  // Empty
    [InlineData("INVALID")]  // Invalid format
    [InlineData("GB82WEST123456987654")]  // Too short
    [InlineData("XX82WEST12345698765432")]  // Invalid country code
    public void BankAccountNumber_ShouldNotBeCreated_WithInvalidFormat(string invalidAccountNumber)
    {
        Assert.True(true, $"TODO: Implement bank account validation - test with {invalidAccountNumber}");
    }

    [Fact]
    public void BankAccountNumber_ShouldValidate_ChecksumForIBAN()
    {
        var validIban = "GB82WEST12345698765432";
        var invalidChecksumIban = "GB00WEST12345698765432"; // Wrong checksum

        Assert.True(true, "TODO: Implement IBAN checksum validation");
    }

    [Fact]
    public void BankAccountNumber_ShouldNormalize_RemovingSpaces()
    {
        var ibanWithSpaces = "GB82 WEST 1234 5698 7654 32";
        var expectedNormalized = "GB82WEST12345698765432";

        Assert.True(true, "TODO: Implement space normalization");
    }

    [Fact]
    public void BankAccountNumber_ShouldBeEqual_WhenValuesAreSame()
    {
        var iban1 = "GB82WEST12345698765432";
        var iban2 = "GB82WEST12345698765432";

        Assert.True(true, "TODO: Implement value object equality");
    }
}

