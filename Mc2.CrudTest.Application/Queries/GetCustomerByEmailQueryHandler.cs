using MediatR;
using Mc2.CrudTest.Application.DTOs;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrudTest.Domain.ValueObjects;

namespace Mc2.CrudTest.Application.Queries;

public class GetCustomerByEmailQueryHandler : IRequestHandler<GetCustomerByEmailQuery, CustomerDto?>
{
    private readonly ICustomerRepository _repository;

    public GetCustomerByEmailQueryHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerDto?> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var email = new Email(request.Email);
            var customer = await _repository.GetByEmailAsync(email, cancellationToken);

            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                BankAccountNumber = customer.BankAccountNumber
            };
        }
        catch
        {
            return null;
        }
    }
}

