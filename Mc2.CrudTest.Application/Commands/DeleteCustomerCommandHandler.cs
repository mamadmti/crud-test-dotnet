using MediatR;
using Mc2.CrudTest.Domain.Interfaces;

namespace Mc2.CrudTest.Application.Commands;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
{
    private readonly ICustomerRepository _repository;

    public DeleteCustomerCommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (customer == null)
            throw new KeyNotFoundException($"Customer with ID {request.Id} not found.");

        await _repository.DeleteAsync(request.Id, cancellationToken);

        return Unit.Value;
    }
}

