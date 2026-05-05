using Moq;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Features.Customers.Commands;
using ShopProject.Application.Features.Customers.Commands.Handlers;
using ShopProject.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace ShopApp.Application.Tests.Features.Customers.Commands.Handlers
{
    public class CreateCustomerHandlerTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IGenericRepository<Customer>> _customerRepoMock;
        private readonly CreateCustomerHandler _handler;

        public CreateCustomerHandlerTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _customerRepoMock = new Mock<IGenericRepository<Customer>>();
            _uowMock.Setup(u => u.Customers).Returns(_customerRepoMock.Object);
            _handler = new CreateCustomerHandler(_uowMock.Object);
        }

        [Fact]
        public async Task Handle_Should_AddCustomerAndReturnId()
        {
            // Arrange
            var command = new CreateCustomerCommand 
            { 
                Name = "John Doe", 
                Email = "john@example.com", 
                Phone = "123456789" 
            };

            _uowMock.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _customerRepoMock.Verify(r => r.AddAsync(It.Is<Customer>(c => 
                c.Name == command.Name && 
                c.Email == command.Email && 
                c.Phone == command.Phone)), Times.Once);
            
            _uowMock.Verify(u => u.CompleteAsync(), Times.Once);
            result.Should().Be(0); // ID is 0 because it's not set by mock
        }
    }
}
