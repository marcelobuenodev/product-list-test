using ProductList.Domain;

namespace ProductList.Infrastructure
{
    public class RepositoryProduct : IRepositoryProduct
    {
        public async Task<List<Product>> GetAll()
        {
            return new List<Product>
            {
                new Product()
                {
                    Id = 1,
                    Name = "Product 1",
                    Description = "Description 1",
                    Price = 10.0m
                },
                new Product()
                {
                    Id = 2,
                    Name = "Product 2",
                    Description = "Description 2",
                    Price = 20.0m
                },
                new Product()
                {
                    Id = 3,
                    Name = "Product 3",
                    Description = "Description 3",
                    Price = 30.0m
                }
            };
        }

        public async Task<Product> GetById(int id)
        {
            var listProduct = await GetAll();
            return listProduct.FirstOrDefault(p => p.Id == id) ?? new Product();
        }
    }
}
