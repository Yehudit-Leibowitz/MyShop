using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class ProductService : IProductService
    {
        IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }


        public async Task<List<Product>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            return await productRepository.GetProducts(desc, minPrice, maxPrice, categoryIds);
        }

        public async Task<Product> GetProductbyId(int id)
        {
            return await productRepository.GetProductById(id);
        }
    }
}

