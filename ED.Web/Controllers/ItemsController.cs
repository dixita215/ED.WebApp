using ED.Api.Model;
using ED.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ED.Web.Controllers
{
    [Route("api/Items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
       
        private readonly IItemRepository _itemRepository;
        public ItemsController(IItemRepository itemRepo)
        {
            _itemRepository = itemRepo;
       }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
        {
            return await _itemRepository.GetAllItems();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _itemRepository.GetItem(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
    }
}
