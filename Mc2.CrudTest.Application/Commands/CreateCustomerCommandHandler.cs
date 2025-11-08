using MediatR;
using Mc2.CrudTest.Application.DTOs;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrudTest.Domain.ValueObjects;

namespace Mc2.CrudTest.Application.Commands;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _repository;

    public CreateCustomerCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        // Create value objects (validation happens in constructors)
        var email = new Email(request.Email);
        var phoneNumber = new PhoneNumber(request.PhoneNumber);
        var bankAccountNumber = new BankAccountNumber(request.BankAccountNumber);

        // Check for uniqueness
        if (await _repository.ExistsAsync(request.FirstName, request.LastName, request.DateOfBirth, cancellationToken))
            throw new InvalidOperationException("Customer already exists with the same first name, last name, and date of birth.");

        if (await _repository.EmailExistsAsync(email, null, cancellationToken))
            throw new InvalidOperationException("Email address already exists.");

        // Create domain entity (business rules enforced in constructor)
        var customer = new Customer(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            phoneNumber,
            email,
            bankAccountNumber);

        await _repository.AddAsync(customer, cancellationToken);

        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DateOfBirth = customer.DateOfBirth,
            PhoneNumber = customer.PhoneNumber.Value,
            Email = customer.Email.Value,
            BankAccountNumber = customer.BankAccountNumber.Value
        };
    }
}

