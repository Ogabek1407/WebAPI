using Microsoft.AspNetCore.Mvc;
using Models;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Interface;

namespace Controllers
{
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        public IItemRepository ItemRepository { get; }

        public ItemController(IItemRepository itemRepository)
        {
            ItemRepository = itemRepository;
        }

        [HttpGet]
        [Route("GetAll")]
        public async ValueTask<List<Item>> GetAll()
        {
            var EntityResoult = ItemRepository.GetAll();
            return EntityResoult.ToList();
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Item>> GetById(int id)
        {
            var EntityResoult = await ItemRepository.GetById(id);
            if (EntityResoult is null)
                return NotFound(nameof(EntityResoult));
            return Ok(EntityResoult);
        }

        [HttpPost()]
        [Route("Add")]
        public async ValueTask<ActionResult<Item>> CreateAsync(Item item)
        {
            try
            {
                if (item is null)
                    new ArgumentNullException("obect is null");

                var entityResoult = await ItemRepository.CreateAsync(item);
                if (entityResoult is null)
                    throw new ArgumentNullException();
                return entityResoult;
            }
            catch (Exception)
            {
                return BadRequest("bad object");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async ValueTask<ActionResult<Item?>> UpdateAsync(Item data)
        {
            try
            {
                if (data is null)
                    throw new ArgumentNullException(nameof(data));
                var EntityResoult = await ItemRepository.UpdateAsync(data);
                if (EntityResoult is null)
                    BadRequest(nameof(EntityResoult));
                return EntityResoult;
            }
            catch (Exception)
            {
                return BadRequest("not fount");
            }

        }

        [HttpDelete]
        [Route("Delete")]
        public async ValueTask<ActionResult<Item?>> DeleteAsync(int id)
        {
            try
            {
                var EntityResoult = await ItemRepository.DeleteAsync(id);
                if (EntityResoult is null)
                    throw new ArgumentNullException(nameof(EntityResoult));
                return EntityResoult;
            }
            catch (Exception)
            {
                return BadRequest("not fount");
            }
        }
    }
}
