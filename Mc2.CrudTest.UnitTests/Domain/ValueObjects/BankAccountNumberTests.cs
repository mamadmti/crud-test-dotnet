using FluentAssertions;
using Xunit;
using Mc2.CrudTest.Domain.ValueObjects;

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
        // Act
        var bankAccount = new BankAccountNumber(validIban);

        // Assert
        bankAccount.Should().NotBeNull();
        bankAccount.Value.Should().Be(validIban.Replace(" ", "").ToUpperInvariant());
    }

    [Theory]
    [InlineData("")]  // Empty
    [InlineData("INVALID")]  // Invalid format
    [InlineData("GB82WEST123456987654")]  // Too short
    [InlineData("XX82WEST12345698765432")]  // Invalid country code
    public void BankAccountNumber_ShouldNotBeCreated_WithInvalidFormat(string invalidAccountNumber)
    {
        // Act & Assert
        Action act = () => new BankAccountNumber(invalidAccountNumber);
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void BankAccountNumber_ShouldValidate_ChecksumForIBAN()
    {
        // Arrange
        var validIban = "GB82WEST12345698765432";
        var invalidChecksumIban = "GB00WEST12345698765432"; // Wrong checksum

        // Act
        var validAccount = new BankAccountNumber(validIban);
        Action invalidAct = () => new BankAccountNumber(invalidChecksumIban);

        // Assert
        validAccount.Should().NotBeNull();
        invalidAct.Should().Throw<ArgumentException>()
            .WithMessage("*checksum*");
    }

    [Fact]
    public void BankAccountNumber_ShouldNormalize_RemovingSpaces()
    {
        // Arrange
        var ibanWithSpaces = "GB82 WEST 1234 5698 7654 32";
        var expectedNormalized = "GB82WEST12345698765432";

        // Act
        var bankAccount = new BankAccountNumber(ibanWithSpaces);

        // Assert
        bankAccount.Value.Should().Be(expectedNormalized);
    }

    [Fact]
    public void BankAccountNumber_ShouldBeEqual_WhenValuesAreSame()
    {
        // Arrange
        var iban1 = new BankAccountNumber("GB82WEST12345698765432");
        var iban2 = new BankAccountNumber("GB82WEST12345698765432");

        // Act & Assert
        iban1.Should().Be(iban2);
        (iban1 == iban2).Should().BeTrue();
    }
}
