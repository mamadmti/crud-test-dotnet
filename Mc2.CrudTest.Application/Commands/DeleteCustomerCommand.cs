using MediatR;

namespace Mc2.CrudTest.Application.Commands;

public class DeleteCustomerCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

