using Moq;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Features.Customers.Commands;
using ShopProject.Application.Features.Customers.Commands.Handlers;
using ShopProject.Domain.Entities;
using FluentAssertions;
using Xunit;
using AutoMapper;

namespace ShopApp.Application.Tests.Features.Customers.Commands.Handlers
{
    public class UpdateCustomerHandlerTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IGenericRepository<Customer>> _customerRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UpdateCustomerHandler _handler;

        public UpdateCustomerHandlerTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _customerRepoMock = new Mock<IGenericRepository<Customer>>();
            _mapperMock = new Mock<IMapper>();
            _uowMock.Setup(u => u.Customers).Returns(_customerRepoMock.Object);
            _handler = new UpdateCustomerHandler(_uowMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_Should_UpdateCustomer_WhenCustomerExists()
        {
            // Arrange
            var command = new UpdateCustomerCommand { Id = 1, Name = "Updated Name" };
            var existingCustomer = new Customer { Id = 1, Name = "Old Name" };

            _customerRepoMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(existingCustomer);
            _uowMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            _mapperMock.Verify(m => m.Map(command, existingCustomer), Times.Once);
            _customerRepoMock.Verify(r => r.Update(existingCustomer), Times.Once);
            _uowMock.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFalse_WhenCustomerDoesNotExist()
        {
            // Arrange
            var command = new UpdateCustomerCommand { Id = 1 };
            _customerRepoMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync((Customer)null!);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _uowMock.Verify(u => u.CompleteAsync(), Times.Never);
        }
    }
}
