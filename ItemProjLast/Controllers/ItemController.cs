using Repository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Dto;
using Microsoft.AspNetCore.Authorization;

namespace Controllers
{
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        public ItemRepository _itemRepository { get; }

        public ItemController(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        [Route("GetAll")]
        public async ValueTask<List<Item>> GetAll()
        {
            var EntityResoult = _itemRepository.GetAll();
            return EntityResoult.ToList();
        }
        
        [HttpGet]
        [Route("{id}")]
        public async ValueTask<Item?> GetById(int id)
        {
            var EntityReolt = await _itemRepository.GetByIdAsync(id);
            return EntityReolt;
        }

        [HttpPost()]
        [Route("Add")]
        public async ValueTask<ItemDto> CreateAsync(ItemDto item)
        {
            var data = new Item()
            {
                Name = item.Name,
                Time = item.Time,
                Type = item.Type
            };
            var data2 = await _itemRepository.CreateAsync(data);
            var entityResoult = new ItemDto()
            {
                Name = data2.Name,
                Type = data2.Type,
                Time = data2.Time
            };
            return entityResoult;
        }

        [HttpPut]
        [Route("")]
        public async ValueTask<Item?> UpdateAsync(Item data)
        {
            var EntityResoult = await _itemRepository.UpdateAsyc(data);
            return EntityResoult;

        }

        [HttpDelete]
        [Route("{id}")]
        public async ValueTask<Item?> DeleteAsync(int id)
        {
            var EntityResoult = await _itemRepository.DeleteAsync(id);
            return EntityResoult;
        }
    }
}
