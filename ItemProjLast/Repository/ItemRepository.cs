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
                throw new ArgumentNullException(nameof(entity));
            var entityResoult =await GetAll().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entityResoult.Entity;
        }

        public async ValueTask<Item?> UpdateAsync(Item entity)
        {
            if (entity is  null)
                throw new ArgumentNullException(nameof(entity));
            var entityResould =await GetAll().Where(x => x.ItemId == entity.ItemId).FirstOrDefaultAsync();
            if(entityResould is null)
                throw new Exception(nameof(entityResould));
            entityResould.ItemName = entity.ItemName;
            entityResould.ItemType = entity.ItemType;
            entityResould.ItemDate= entity.ItemDate;
            await _context.SaveChangesAsync();
            return entityResould;
        }

        public async ValueTask<Item?> DeleteAsync(int id)
        {
            var entity =await GetAll().Where(x => x.ItemId == id).FirstOrDefaultAsync();
            if (entity is null)
                throw new ArgumentNullException(nameof(id));
            var entityResoult = GetAll().Remove(entity);
            await _context.SaveChangesAsync();
            return entityResoult.Entity;

        }
    }
}
