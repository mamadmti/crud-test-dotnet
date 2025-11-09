using MediatR;
using Mc2.CrudTest.Contracts;

namespace Mc2.CrudTest.Application.Queries;

public class GetCustomerByIdQuery : IRequest<CustomerDto?>
{
    public Guid Id { get; set; }
}

