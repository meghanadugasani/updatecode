using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace APILoanProduct.Interfaces.Repositories
{
    public interface IGenericRepository<T, K> where T : class
    {
        // CREATE
        Task<T> AddAsync(T entity);

        // READ
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(K id);

        // UPDATE
        Task<T> UpdateAsync(K id, T entity);

        // DELETE
        Task<bool> DeleteAsync(K id);

        // 🔍 Optional: Search (default no implementation)
        Task<IEnumerable<T>> SearchAsync(string? keyword)
        {
            throw new NotImplementedException("Search is not implemented for this entity.");
        }

        // 🔎 Optional: Filter (default no implementation)
        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>>? predicate)
        {
            throw new NotImplementedException("Filter is not implemented for this entity.");
        }
    }
}