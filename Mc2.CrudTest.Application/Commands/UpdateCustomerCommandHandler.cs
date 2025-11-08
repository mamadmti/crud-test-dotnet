using MediatR;
using Mc2.CrudTest.Application.DTOs;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrudTest.Domain.ValueObjects;

namespace Mc2.CrudTest.Application.Commands;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _repository;

    public UpdateCustomerCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (customer == null)
            throw new KeyNotFoundException($"Customer with ID {request.Id} not found.");

        // Create value objects (validation happens in constructors)
        var email = new Email(request.Email);
        var phoneNumber = new PhoneNumber(request.PhoneNumber);
        var bankAccountNumber = new BankAccountNumber(request.BankAccountNumber);

        // Check email uniqueness (excluding current customer)
        if (await _repository.EmailExistsAsync(email, customer.Id, cancellationToken))
            throw new InvalidOperationException("Email address already exists.");

        // Update through domain methods
        customer.UpdatePhoneNumber(phoneNumber);
        customer.UpdateEmail(email);
        customer.UpdateBankAccountNumber(bankAccountNumber);

        await _repository.UpdateAsync(customer, cancellationToken);

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

