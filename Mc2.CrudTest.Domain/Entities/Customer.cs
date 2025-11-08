using Mc2.CrudTest.Domain.ValueObjects;

namespace Mc2.CrudTest.Domain.Entities;

public class Customer
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public DateTime DateOfBirth { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public BankAccountNumber BankAccountNumber { get; private set; } = null!;

    private Customer() { }

    public Customer(
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        PhoneNumber phoneNumber,
        Email email,
        BankAccountNumber bankAccountNumber)
    {
        Id = Guid.NewGuid();

        SetFirstName(firstName);
        SetLastName(lastName);
        SetDateOfBirth(dateOfBirth);

        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        BankAccountNumber = bankAccountNumber ?? throw new ArgumentNullException(nameof(bankAccountNumber));
    }

    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
    }

    public void UpdateEmail(Email email)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    public void UpdateBankAccountNumber(BankAccountNumber bankAccountNumber)
    {
        BankAccountNumber = bankAccountNumber ?? throw new ArgumentNullException(nameof(bankAccountNumber));
    }

    private void SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));

        FirstName = firstName.Trim();
    }

    private void SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

        LastName = lastName.Trim();
    }

    private void SetDateOfBirth(DateTime dateOfBirth)
    {
        if (dateOfBirth >= DateTime.UtcNow.Date)
            throw new ArgumentException("Date of birth must be in the past.", nameof(dateOfBirth));

        DateOfBirth = dateOfBirth.Date;
    }
}

