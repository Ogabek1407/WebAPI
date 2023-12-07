using Infrastructure;
using Models;
namespace Repository
{
    public class ItemRepository : RepositoryBase<Item>
    {
        public ItemRepository(DataContext context) : base(context)
        {
        }
    }
}
