using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Interface;
public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public DbSet<User> GetAll()
    {
        return _context.Set<User>();
    }

    public async ValueTask<User?> GetById(int id)
    {
        return await GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async ValueTask<User> CreateAsync(User entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        if (entity.Password.Length >= 8)
            throw new Exception();
        var data=GetAll().Where(x=>x.Email == entity.Email);
        if (data is not null)
            throw new Exception();
        var entityResoult = await GetAll().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entityResoult.Entity;
    }

    public async ValueTask<User> UpdateAsync(User entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        var data = await GetAll().Where(x => x.Email == entity.Email).FirstOrDefaultAsync();
        if (data is not null)
            throw new Exception("Bu Gmail oldin ruyxatdan utgan");
        var entityResoult = await GetAll().Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
        if (entityResoult is null)
            throw new ArgumentNullException(nameof(entityResoult));
        entityResoult.FirstName= entity.FirstName;
        entityResoult.LastName= entity.LastName;
        entityResoult.Email = entity.Email;
        entityResoult.Password= entity.Password;
        
        await _context.SaveChangesAsync();
        return entityResoult;
    }

    public async ValueTask<User?> DeleteAsync(int id)
    {
        var entity = await GetAll().Where(x => x.Id == id).FirstOrDefaultAsync();
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
        var entityResoult = GetAll().Remove(entity);
        await _context.SaveChangesAsync();
        return entityResoult.Entity;
    }
}