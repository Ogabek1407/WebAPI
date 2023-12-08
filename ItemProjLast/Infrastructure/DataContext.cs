using ItemProjLast.Domian.Dto;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Infrastructure
{
    public class DataContext:DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AppSettingsDto> AppSettings { get; set; }
        public DataContext(DbContextOptions<DataContext> options):base(options) 
        {   
        }
    }
}
