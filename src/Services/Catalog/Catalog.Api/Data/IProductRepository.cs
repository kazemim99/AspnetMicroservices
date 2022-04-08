using Catalog.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Data
{
    public interface IProductRepository:IRepositoryBase<Product>
    {
    }
}