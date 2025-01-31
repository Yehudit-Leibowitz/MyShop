using Entity;

namespace service
{
    public interface IProductService
    {
        Task<Product> GetProductbyId(int id);
        Task<List<Product>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}