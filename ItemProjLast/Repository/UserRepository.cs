using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;
using ArgumentNullException = System.ArgumentNullException;

namespace Interface;

public class UserRepository:IUserRepository
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
            new ArgumentNullException(nameof(entity));
        var entityResoult = await GetAll().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entityResoult.Entity;
    }

    public async ValueTask<User> UpdateAsync(User entity)
    {
        if (await GetById(entity.Id) is null)
            new ArgumentNullException(nameof(entity));
        var entityResoult = GetAll().Update(entity);
        await _context.SaveChangesAsync();
        return entityResoult.Entity;
    }

    public async ValueTask<User> DeleteAsync(int id)
    {
        if (await GetById(id) is null)
            new ArgumentNullException(nameof(id));
        var entityResoult = GetAll().Remove(await GetById(id));
        await _context.SaveChangesAsync();
        return entityResoult.Entity;
    }
}