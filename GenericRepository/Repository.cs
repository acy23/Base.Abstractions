using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Linq.Expressions;

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

        public async Task<T> Update<T>(T entity, int id) where T : class
        {
            DbSet<T> dbSet = _context.Set<T>();
            T existingItem = await dbSet.FindAsync(id);

            if (existingItem == null)
            {
                throw new System.Exception("Item not found.");
            }

            _context.Entry(existingItem).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();

            return existingItem;
        }

        public async Task<List<T>> GetList<T>() where T : class
        {
            DbSet<T> dbSet = _context.Set<T>();

            List<T> entityList = await dbSet
                .ToListAsync();

            return entityList;
        }

        public async Task<List<T>> GetListByExpression<T>(Expression<Func<T,bool>> predicate) where T : class
        {
            DbSet<T> dbSet = _context.Set<T>();

            List<T> entityList = await dbSet
                .Where(predicate)
                .ToListAsync();

            return entityList;
        }
    }
}