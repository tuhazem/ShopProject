using Moq;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Features.Categories.Commands;
using ShopProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Xunit;

namespace ShopApp.Application.Tests.Features.Categories.Commands
{
    public class CreateCategoryCommandHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _contextMock;
        private readonly CreateCategoryCommandHandler _handler;

        public CreateCategoryCommandHandlerTests()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            _handler = new CreateCategoryCommandHandler(_contextMock.Object);
        }

        [Fact]
        public async Task Handle_Should_AddCategoryAndReturnId()
        {
            // Arrange
            var command = new CreateCategoryCommand { Name = "Test Category" };
            var categories = new List<Category>();
            var mockSet = CreateMockDbSet(categories);

            _contextMock.Setup(c => c.Categories).Returns(mockSet.Object);
            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _contextMock.Verify(c => c.Categories.Add(It.Is<Category>(cat => cat.Name == command.Name)), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            // Result is expected to be 0 as the mock doesn't set the ID
            result.Should().Be(0); 
        }

        private static Mock<DbSet<T>> CreateMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);
            return mockSet;
        }
    }
}
