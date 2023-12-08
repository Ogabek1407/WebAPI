using Infrastructure;
using Interface;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Repository
{
    public class ItemRepository:IItemRepository
    {
        private readonly DataContext _context;

        public ItemRepository(DataContext context)
        {
            _context = context;
        }
        public DbSet<Item> GetAll()
        {
            return _context.Set<Item>();
        }

        public async ValueTask<Item?> GetById(int id)
        {
            return await GetAll().Where(x => x.ItemId == id).FirstOrDefaultAsync();
        }

        public async ValueTask<Item?> CreateAsync(Item entity)
        {
            if (entity is null)
                new ArgumentNullException(nameof(entity));
            var entityResoult =await GetAll().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entityResoult.Entity;
        }

        public async ValueTask<Item?> UpdateAsync(Item entity)
        {
            if (await GetById(entity.ItemId) is  null)
                new ArgumentNullException(nameof(entity));
            var entityResould = GetAll().Update(entity);
            await _context.SaveChangesAsync();
            return entityResould.Entity;
        }

        public async ValueTask<Item?> DeleteAsync(int id)
        {
            if (await GetById(id) is null)
                new ArgumentNullException(nameof(id));
            var entityResoult = GetAll().Remove(await GetById(id));
            await _context.SaveChangesAsync();
            return entityResoult.Entity;

        }
    }
}
