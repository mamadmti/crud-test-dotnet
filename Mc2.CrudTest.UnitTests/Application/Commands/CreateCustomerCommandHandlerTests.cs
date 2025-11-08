using FluentAssertions;
using Moq;
using Xunit;
using Mc2.CrudTest.Application.Commands;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.ValueObjects;

namespace Mc2.CrudTest.UnitTests.Application.Commands;

public class CreateCustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _mockRepository;
    private readonly CreateCustomerCommandHandler _handler;

    public CreateCustomerCommandHandlerTests()
    {
        _mockRepository = new Mock<ICustomerRepository>();
        _handler = new CreateCustomerCommandHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateCustomer_WithValidData()
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = "+14155552671",
            Email = "john.doe@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };

        _mockRepository.Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _mockRepository.Setup(x => x.EmailExistsAsync(It.IsAny<Email>(), It.IsAny<Guid?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _mockRepository.Setup(x => x.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.FirstName.Should().Be("John");
        result.LastName.Should().Be("Doe");
        result.Email.Should().Be("john.doe@example.com");
        result.PhoneNumber.Should().Be("+14155552671");
        _mockRepository.Verify(x => x.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenEmailAlreadyExists()
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            FirstName = "Jane",
            LastName = "Smith",
            DateOfBirth = new DateTime(1985, 5, 20),
            PhoneNumber = "+14155552671",
            Email = "existing@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };

        _mockRepository.Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _mockRepository.Setup(x => x.EmailExistsAsync(It.IsAny<Email>(), It.IsAny<Guid?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await _handler.Handle(command, CancellationToken.None));

        exception.Message.Should().Contain("Email address already exists");
        _mockRepository.Verify(x => x.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenCustomerAlreadyExists()
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = "+14155552671",
            Email = "john@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };

        _mockRepository.Setup(x => x.ExistsAsync(command.FirstName, command.LastName, command.DateOfBirth, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await _handler.Handle(command, CancellationToken.None));

        exception.Message.Should().Contain("Customer already exists");
        _mockRepository.Verify(x => x.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldFail_WithInvalidPhoneNumber()
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = "+1234",
            Email = "john@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };

        _mockRepository.Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _mockRepository.Setup(x => x.EmailExistsAsync(It.IsAny<Email>(), It.IsAny<Guid?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await _handler.Handle(command, CancellationToken.None));

        exception.Message.Should().Contain("Invalid phone number");
    }

    [Fact]
    public async Task Handle_ShouldFail_WithLandlinePhoneNumber()
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 15),
            PhoneNumber = "+442071234567",
            Email = "john@example.com",
            BankAccountNumber = "GB82WEST12345698765432"
        };

        _mockRepository.Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _mockRepository.Setup(x => x.EmailExistsAsync(It.IsAny<Email>(), It.IsAny<Guid?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await _handler.Handle(command, CancellationToken.None));

        exception.Message.Should().Contain("Only mobile phone numbers are allowed");
    }
}

