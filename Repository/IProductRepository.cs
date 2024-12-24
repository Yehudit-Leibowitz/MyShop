using Entity;

namespace Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}