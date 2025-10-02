using Microsoft.Extensions.DependencyInjection;

namespace ProductList.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<Infrastructure.IRepositoryProduct, Infrastructure.RepositoryProduct>();
            return services;
        }
    }
}
