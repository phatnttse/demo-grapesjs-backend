namespace Demo_Grapesjs.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<T> UpdateAsync(T entity);
        IQueryable<T> GetQueryable();
    }
}
