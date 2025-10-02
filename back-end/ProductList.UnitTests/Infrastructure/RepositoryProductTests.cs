using Moq;
using ProductList.Domain;
using ProductList.Infrastructure;


namespace ProductList.UnitTests.Infrastructure
{
    public class RepositoryProductTests
    {
        [Fact]
        public async Task GetAll_ReturnsListOfProducts()
        {
            // Arrange
            var mockRepo = new Mock<IRepositoryProduct>();
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product A", Description = "Desc A", Price = 10.0m },
                new Product { Id = 2, Name = "Product B", Description = "Desc B", Price = 20.0m }
            };
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(expectedProducts);

            // Act
            var result = await mockRepo.Object.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Product A", result[0].Name);
            Assert.Equal("Product B", result[1].Name);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsProduct()
        {
            // Arrange
            var mockRepo = new Mock<IRepositoryProduct>();
            var expectedProduct = new Product { Id = 1, Name = "Product A", Description = "Desc A", Price = 10.0m };
            mockRepo.Setup(r => r.GetById(1)).ReturnsAsync(expectedProduct);

            // Act
            var result = await mockRepo.Object.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Product A", result.Name);
        }

        [Fact]
        public async Task GetById_NonExistingId_ReturnsNull()
        {
            // Arrange
            var mockRepo = new Mock<IRepositoryProduct>();
            mockRepo.Setup(r => r.GetById(99)).ReturnsAsync((Product)null);

            // Act
            var result = await mockRepo.Object.GetById(99);

            // Assert
            Assert.Null(result);
        }
    }
}