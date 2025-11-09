using MediatR;
using Mc2.CrudTest.Contracts;

namespace Mc2.CrudTest.Application.Queries;

public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>
{
}

