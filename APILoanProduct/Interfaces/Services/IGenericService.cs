namespace APILoanProduct.Interfaces.Services
{
    public interface IGenericService<T, K> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(K id);
        Task<T> CreateAsync<TDto>(TDto dto);
        Task<T> UpdateAsync<TDto>(K id, TDto dto);
        Task<bool> DeleteAsync(K id);
    }
}