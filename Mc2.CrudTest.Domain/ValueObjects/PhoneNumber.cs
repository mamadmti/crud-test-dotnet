using Mc2.CrudTest.Domain.Common;
using PhoneNumbers;

namespace Mc2.CrudTest.Domain.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    private static readonly PhoneNumberUtil PhoneUtil = PhoneNumberUtil.GetInstance();

    public string Value { get; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number cannot be empty.", nameof(value));

        try
        {
            var phoneNumber = PhoneUtil.Parse(value, null);

            if (!PhoneUtil.IsValidNumber(phoneNumber))
                throw new ArgumentException($"Invalid phone number: {value}", nameof(value));

            var numberType = PhoneUtil.GetNumberType(phoneNumber);
            if (numberType != PhoneNumberType.MOBILE && numberType != PhoneNumberType.FIXED_LINE_OR_MOBILE)
                throw new ArgumentException($"Only mobile phone numbers are allowed: {value}", nameof(value));

            Value = PhoneUtil.Format(phoneNumber, PhoneNumberFormat.E164);
        }
        catch (NumberParseException ex)
        {
            throw new ArgumentException($"Invalid phone number format: {value}", nameof(value), ex);
        }
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
}

