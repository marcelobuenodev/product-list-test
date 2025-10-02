namespace ProductList.Infrastructure
{
    public interface IRepositoryProduct
    {
        Task<List<Domain.Product>> GetAll();

        Task<Domain.Product> GetById(int id);
    }
}