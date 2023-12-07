using Microsoft.EntityFrameworkCore;
using Models;
namespace Repository
{
    public class RepositoryBase<T> where T :Item
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }
        public DbSet<T> GetAll()
        {
            return this._context.Set<T>();
        }

        public async ValueTask<T?> GetByIdAsync(long id)
        {
            return await this.GetAll().Where(x=>x.Id==id).FirstOrDefaultAsync();
        }

        public async ValueTask<T> CreateAsync(T entity)
        {
            var result = await GetAll().AddAsync(entity);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async ValueTask<T> UpdateAsyc(T entity)
        {
            var result = GetAll().Update(entity);
            await this._context.SaveChangesAsync();

            return result.Entity;
        }

        public async ValueTask<T> DeleteAsync(int id)
        {
            var result = GetAll().Remove(await GetAll().Where(x=>x.Id==id).FirstOrDefaultAsync());
            await this._context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
