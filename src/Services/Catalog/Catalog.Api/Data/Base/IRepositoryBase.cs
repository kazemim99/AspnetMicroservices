using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Data
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(string id);
        Task UpdateAsync(T obj);
        Task InsertAsync(T obj);
    }
}
