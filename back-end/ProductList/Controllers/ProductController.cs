using Microsoft.AspNetCore.Mvc;
using ProductList.Infrastructure;

namespace ProductList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryProduct _repositoryProduct;
        private readonly IConfiguration _configuration;

        public ProductController
            (
                IRepositoryProduct repositoryProduct,
                IConfiguration configuration

            )
        {
            _repositoryProduct = repositoryProduct;
            _configuration = configuration;
        }

        [HttpGet()]
        [Route("GetAll")]
        public async Task<IActionResult> Get([FromHeader(Name = "X-API-KEY")] string apiKey)
        {
            if (!IsValidApiKey(apiKey)) return Unauthorized();

            var listProducts = await _repositoryProduct.GetAll();

            return Ok(listProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, [FromHeader(Name = "X-API-KEY")] string apiKey)
        {
            if (!IsValidApiKey(apiKey)) return Unauthorized();

            var product = await _repositoryProduct.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        private bool IsValidApiKey(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey)) return false;

            return _configuration.GetSection("apiKey").Value == apiKey;
        }
    }
}
