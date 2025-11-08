using FluentAssertions;
using Moq;
using Xunit;
using Mc2.CrudTest.Application.Commands;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.ValueObjects;

namespace Mc2.CrudTest.UnitTests.Application.Commands;

public class UpdateCustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _mockRepository;
    private readonly UpdateCustomerCommandHandler _handler;

    public UpdateCustomerCommandHandlerTests()
    {
        _mockRepository = new Mock<ICustomerRepository>();
        _handler = new UpdateCustomerCommandHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateCustomer_WithValidData()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var existingCustomer = new Customer(
            "John",
            "Doe",
            new DateTime(1990, 1, 15),
            new PhoneNumber("+14155552671"),
            new Email("john@example.com"),
            new BankAccountNumber("GB82WEST12345698765432")
        );

        var command = new UpdateCustomerCommand
        {
            Id = customerId,
            PhoneNumber = "+14155559876",
            Email = "newemail@example.com",
            BankAccountNumber = "FR1420041010050500013M02606"
        };

        _mockRepository.Setup(x => x.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingCustomer);
        _mockRepository.Setup(x => x.EmailExistsAsync(It.IsAny<Email>(), customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.PhoneNumber.Should().Be("+14155559876");
        result.Email.Should().Be("newemail@example.com");
        _mockRepository.Verify(x => x.UpdateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenCustomerNotFound()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var command = new UpdateCustomerCommand
        {
            Id = customerId,
            PhoneNumber = "+14155559876",
            Email = "newemail@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };

        _mockRepository.Setup(x => x.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(
            async () => await _handler.Handle(command, CancellationToken.None));

        exception.Message.Should().Contain("not found");
        _mockRepository.Verify(x => x.UpdateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenEmailAlreadyTakenByAnotherCustomer()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var existingCustomer = new Customer(
            "John",
            "Doe",
            new DateTime(1990, 1, 15),
            new PhoneNumber("+14155552671"),
            new Email("john@example.com"),
            new BankAccountNumber("GB82WEST12345698765432")
        );

        var command = new UpdateCustomerCommand
        {
            Id = customerId,
            PhoneNumber = "+14155559876",
            Email = "taken@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };

        _mockRepository.Setup(x => x.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingCustomer);
        _mockRepository.Setup(x => x.EmailExistsAsync(It.IsAny<Email>(), It.IsAny<Guid?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await _handler.Handle(command, CancellationToken.None));

        exception.Message.Should().Contain("Email address already exists");
        _mockRepository.Verify(x => x.UpdateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}

