using Microsoft.EntityFrameworkCore;
using Models;

namespace Interface;
public interface IUserRepository
{
    DbSet<User> GetAll();

    ValueTask<User?> GetById(int id);

    ValueTask<User> CreateAsync(User entity);

    ValueTask<User> UpdateAsync(User entity);

    ValueTask<User> DeleteAsync(int id);
}