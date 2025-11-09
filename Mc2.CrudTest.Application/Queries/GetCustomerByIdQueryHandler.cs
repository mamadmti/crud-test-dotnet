using MediatR;
using Mc2.CrudTest.Contracts;
using Mc2.CrudTest.Domain.Interfaces;

namespace Mc2.CrudTest.Application.Queries;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    private readonly ICustomerRepository _repository;

    public GetCustomerByIdQueryHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (customer == null)
            return null;

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

