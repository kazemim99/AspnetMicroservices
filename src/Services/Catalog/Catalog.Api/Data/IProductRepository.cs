using Catalog.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Data
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<Product> GetProductByIdAsync(string id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
        Task<IEnumerable<Product>> GetProductByCategoryName(string name);
    }
}