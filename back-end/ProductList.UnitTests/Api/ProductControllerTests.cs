using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using ProductList.Controllers;
using ProductList.Domain;
using ProductList.Infrastructure;

namespace ProductList.UnitTests.Api
{
    public class ProductControllerTests
    {
        private readonly Mock<IRepositoryProduct> _mockRepo;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly ProductController _controller;
        private const string ValidApiKey = "12345";

        public ProductControllerTests()
        {
            _mockRepo = new Mock<IRepositoryProduct>();
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(c => c.GetSection("apiKey").Value).Returns(ValidApiKey);
            _controller = new ProductController(_mockRepo.Object, _mockConfig.Object);
        }

        [Fact]
        public async Task Get_WithInvalidApiKey_ReturnsUnauthorized()
        {
            // Act
            var result = await _controller.Get("wrong-key");

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Get_WithNullApiKey_ReturnsUnauthorized()
        {
            // Act
            var result = await _controller.Get(null);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Get_WithValidApiKey_ReturnsOkWithProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "A", Description = "Desc", Price = 1.0m }
            };
            _mockRepo.Setup(r => r.GetAll()).Returns(Task.FromResult(products));

            // Act
            var result = await _controller.Get(ValidApiKey);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(products, okResult.Value);
        }

        [Fact]
        public async Task GetById_WithInvalidApiKey_ReturnsUnauthorized()
        {
            // Act
            var result = await _controller.GetById(1, "wrong-key");

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task GetById_WithValidApiKey_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetById(1)).Returns(Task.FromResult<Product>(null));

            // Act
            var result = await _controller.GetById(1, ValidApiKey);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_WithValidApiKey_ProductFound_ReturnsOk()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "A", Description = "Desc", Price = 1.0m };
            _mockRepo.Setup(r => r.GetById(1)).Returns(Task.FromResult(product));

            // Act
            var result = await _controller.GetById(1, ValidApiKey);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product, okResult.Value);
        }
    }
}
