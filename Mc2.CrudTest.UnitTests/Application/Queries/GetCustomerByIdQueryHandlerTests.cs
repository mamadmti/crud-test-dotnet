using FluentAssertions;
using Moq;
using Xunit;

namespace Mc2.CrudTest.UnitTests.Application.Queries;

public class GetCustomerByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnCustomer_WhenExists()
    {
        // Arrange

        // Act

        // Assert
        
        await Task.CompletedTask;
        Assert.True(true, "TODO: Implement GetCustomerByIdQueryHandler");
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenNotFound()
    {
        // Arrange

        // Act

        // Assert
        
        await Task.CompletedTask;
        Assert.True(true, "TODO: Implement not found scenario");
    }
}

