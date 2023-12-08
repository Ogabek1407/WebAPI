using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Interface;

public interface IItemRepository
{
    DbSet<Item> GetAll();

    ValueTask<Item?> GetById(int id);

    ValueTask<Item?> CreateAsync(Item entity);

    ValueTask<Item?> UpdateAsync(Item entity);

    ValueTask<Item?> DeleteAsync(int id);
}