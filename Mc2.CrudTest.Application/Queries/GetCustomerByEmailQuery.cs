using MediatR;
using Mc2.CrudTest.Contracts;

namespace Mc2.CrudTest.Application.Queries;

public class GetCustomerByEmailQuery : IRequest<CustomerDto?>
{
    public string Email { get; set; } = string.Empty;
}

