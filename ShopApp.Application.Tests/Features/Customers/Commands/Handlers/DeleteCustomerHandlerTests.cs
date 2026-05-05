using Moq;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Features.Customers.Commands;
using ShopProject.Application.Features.Customers.Commands.Handlers;
using ShopProject.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace ShopApp.Application.Tests.Features.Customers.Commands.Handlers
{
    public class DeleteCustomerHandlerTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IGenericRepository<Customer>> _customerRepoMock;
        private readonly DeleteCustomerHandler _handler;

        public DeleteCustomerHandlerTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _customerRepoMock = new Mock<IGenericRepository<Customer>>();
            _uowMock.Setup(u => u.Customers).Returns(_customerRepoMock.Object);
            _handler = new DeleteCustomerHandler(_uowMock.Object);
        }

        [Fact]
        public async Task Handle_Should_MarkAsDeleted_WhenCustomerExists()
        {
            // Arrange
            var command = new DeleteCustomerCommand(1);
            var existingCustomer = new Customer { Id = 1, IsDeleted = false };

            _customerRepoMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(existingCustomer);
            _uowMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            existingCustomer.IsDeleted.Should().BeTrue();
            _customerRepoMock.Verify(r => r.Delete(existingCustomer), Times.Once);
            _uowMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFalse_WhenCustomerDoesNotExist()
        {
            // Arrange
            var command = new DeleteCustomerCommand(1);
            _customerRepoMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync((Customer)null!);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _uowMock.Verify(u => u.CompleteAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_ReturnFalse_WhenCustomerAlreadyDeleted()
        {
            // Arrange
            var command = new DeleteCustomerCommand(1);
            var existingCustomer = new Customer { Id = 1, IsDeleted = true };
            _customerRepoMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(existingCustomer);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _uowMock.Verify(u => u.CompleteAsync(), Times.Never);
        }
    }
}
