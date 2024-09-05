using Demo_Grapesjs.Core.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Demo_Grapesjs.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(Guid id) => await _context.Set<T>().FindAsync(id);

        public Task<T> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            return Task.FromResult(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
                _context.Set<T>().Remove(entity);
        }

        public Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public IQueryable<T> GetQueryable() => _context.Set<T>();


    }
}
