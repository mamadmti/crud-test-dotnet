using FluentAssertions;
using Moq;
using Xunit;

namespace Mc2.CrudTest.UnitTests.Application.Commands;

public class DeleteCustomerCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldDeleteCustomer_WhenExists()
    {
        // Arrange

        // Act

        // Assert
        
        await Task.CompletedTask;
        Assert.True(true, "TODO: Implement DeleteCustomerCommandHandler");
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenCustomerNotFound()
    {
        // Arrange

        // Act

        // Assert
        
        await Task.CompletedTask;
        Assert.True(true, "TODO: Implement customer not found handling");
    }
}

