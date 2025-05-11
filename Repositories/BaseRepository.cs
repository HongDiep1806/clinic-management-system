
using ClinicManagementSystem.DAL;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> Create(T entity)
        {
            var newItem = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return newItem.Entity;
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();   
        }

        public async Task<T> GetById(int id)
        {
           var item = await _context.Set<T>().FindAsync(id);
            return item;
        }

        public async Task<bool> Update(int id, T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(id);
            if (existingEntity == null)
            {
                return false;
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
