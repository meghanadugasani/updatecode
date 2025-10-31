using APILoanProduct.Interfaces.Repositories;
using APILoanProduct.Interfaces.Services;

namespace APILoanProduct.Services
{
    public abstract class GenericService<T, K> : IGenericService<T, K> where T : class
    {
        protected readonly IGenericRepository<T, K> _repository;

        protected GenericService(IGenericRepository<T, K> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<T?> GetByIdAsync(K id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public abstract Task<T> CreateAsync<TDto>(TDto dto);
        public abstract Task<T> UpdateAsync<TDto>(K id, TDto dto);

        public virtual async Task<bool> DeleteAsync(K id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}