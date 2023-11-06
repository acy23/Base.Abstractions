using Microsoft.EntityFrameworkCore;

namespace GenericRepository
{
    public class Repository : IRepository 
    {
        public Repository()
        {

        }

        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public async Task<T?> Get<T>(int id) where T : class
        {
            DbSet<T> dbSet = _context.Set<T>();
            T? entity = await dbSet.FindAsync(id);
            return entity;
        }

        public async Task<T> Create<T>(T entity) where T : class
        {
            DbSet<T> dbSet = _context.Set<T>();
            dbSet.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}