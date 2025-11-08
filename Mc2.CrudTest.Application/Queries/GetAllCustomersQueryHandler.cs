using MediatR;
using Mc2.CrudTest.Application.DTOs;
using Mc2.CrudTest.Domain.Interfaces;

namespace Mc2.CrudTest.Application.Queries;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    private readonly ICustomerRepository _repository;

    public GetAllCustomersQueryHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _repository.GetAllAsync(cancellationToken);

        return customers.Select(customer => new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DateOfBirth = customer.DateOfBirth,
            PhoneNumber = customer.PhoneNumber.Value,
            Email = customer.Email.Value,
            BankAccountNumber = customer.BankAccountNumber.Value
        });
    }
}

