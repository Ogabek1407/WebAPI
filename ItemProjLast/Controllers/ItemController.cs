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
        public ItemRepository ItemRepository { get; }

        public ItemController(ItemRepository itemRepository)
        {
            ItemRepository = itemRepository;
        }

        [HttpGet]
        [Route("GetAll")]
        public async ValueTask<ActionResult<List<Item>>> GetAll()
        {
            var EntityResoult = ItemRepository.GetAll();
            return Ok(EntityResoult.ToList());
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<OkObjectResult> GetById(int id)
        {
            var EntityResoult = await ItemRepository.GetById(id);
            if (EntityResoult is null)
                NotFound(nameof(EntityResoult));
            return Ok(EntityResoult);
        }

        [HttpPost()]
        [Route("Add")]
        public async ValueTask<ActionResult<ItemDto>> CreateAsync(ItemDto itemDto)
        {
            if (itemDto is null)
                new ArgumentNullException(nameof(itemDto));
            var data = new Item()
            {
                ItemName = itemDto.Name,
                ItemDate = itemDto.Time,
                ItemType = itemDto.Type
            };
            var data2 = await ItemRepository.CreateAsync(data);
            if (data2 is null)
                new ArgumentNullException(nameof(data2));
            var entityResoult = new ItemDto()
            {
                Name = data2.ItemName,
                Type = data2.ItemType,
                Time = data2.ItemDate
            };
            return entityResoult;
        }

        [HttpPut]
        [Route("Update")]
        public async ValueTask<Item?> UpdateAsync(Item data)
        {
            if (data is null)
                new ArgumentNullException(nameof(data));
            var EntityResoult = await ItemRepository.UpdateAsync(data);
            return EntityResoult;

        }

        [HttpDelete]
        [Route("Delete")]
        public async ValueTask<Item?> DeleteAsync(int id)
        {
            var EntityResoult = await ItemRepository.DeleteAsync(id);
            if (EntityResoult is null)
                new ArgumentNullException(nameof(EntityResoult));
            return EntityResoult;
        }
    }
}
