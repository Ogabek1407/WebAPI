using ItemProjLast.Domian.Models;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Infrastructure
{
    public class DataContext:DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AppSettings> AppSettingss { get; set; }
        public DataContext(DbContextOptions<DataContext> options):base(options) 
        {   
        }
    }
}
