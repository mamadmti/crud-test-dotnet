using System.Text.RegularExpressions;
using Mc2.CrudTest.Domain.Common;

namespace Mc2.CrudTest.Domain.ValueObjects;

public sealed class BankAccountNumber : ValueObject
{
    private static readonly Regex IbanRegex = new(
        @"^[A-Z]{2}[0-9]{2}[A-Z0-9]+$",
        RegexOptions.Compiled);

    public string Value { get; }

    public BankAccountNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Bank account number cannot be empty.", nameof(value));

        var normalized = value.Replace(" ", "").ToUpperInvariant();

        if (!IbanRegex.IsMatch(normalized))
            throw new ArgumentException($"Invalid IBAN format: {value}", nameof(value));

        if (normalized.Length < 15 || normalized.Length > 34)
            throw new ArgumentException($"IBAN length must be between 15 and 34 characters: {value}", nameof(value));

        if (!IsValidIbanChecksum(normalized))
            throw new ArgumentException($"Invalid IBAN checksum: {value}", nameof(value));

        Value = normalized;
    }

    private static bool IsValidIbanChecksum(string iban)
    {
        var rearranged = iban.Substring(4) + iban.Substring(0, 4);
        var numericString = string.Empty;

        foreach (var c in rearranged)
        {
            if (char.IsDigit(c))
                numericString += c;
            else if (char.IsLetter(c))
                numericString += (c - 'A' + 10).ToString();
            else
                return false;
        }

        return Mod97(numericString) == 1;
    }

    private static int Mod97(string numericString)
    {
        var remainder = 0;
        foreach (var digit in numericString)
        {
            remainder = (remainder * 10 + (digit - '0')) % 97;
        }
        return remainder;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(BankAccountNumber bankAccountNumber) => bankAccountNumber.Value;
}

