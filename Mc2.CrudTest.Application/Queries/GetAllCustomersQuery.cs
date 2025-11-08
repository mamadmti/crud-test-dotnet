using MediatR;
using Mc2.CrudTest.Application.DTOs;

namespace Mc2.CrudTest.Application.Queries;

public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>
{
}

