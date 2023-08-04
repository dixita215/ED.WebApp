using ED.Api.Model;
using ED.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ED.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingTrolleyController : ControllerBase
    {
        private readonly IShoppingTrolleyService _shoppingTrolleyService;

        public ShoppingTrolleyController(IShoppingTrolleyService shoppingTrolleyService)
        {
            _shoppingTrolleyService = shoppingTrolleyService;
        }
        private int GetCurrentUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userId);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {
            var user = _shoppingTrolleyService.Authenticate(credentials.Username, credentials.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(new { user.Id, user.Username });
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<ShoppingCartItem>> GetAllItems()
        {
            int userId = GetCurrentUserId();
            var items = _shoppingTrolleyService.GetAllItems(userId);
            return Ok(items);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<ShoppingCartItem> AddItem([FromBody] ShoppingCartItem newItem)
        {
            int userId = GetCurrentUserId();
            var addedItem = _shoppingTrolleyService.AddItem(userId, newItem);

            return CreatedAtAction(nameof(GetItem), new { id = addedItem.Id }, addedItem);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, [FromBody] ShoppingCartItem updatedItem)
        {
            int userId = GetCurrentUserId();
            _shoppingTrolleyService.UpdateItem(userId, id, updatedItem);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult RemoveItem(int id)
        {
            int userId = GetCurrentUserId();
            _shoppingTrolleyService.RemoveItem(userId, id);
            return NoContent();
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ShoppingCartItem> GetItem(int id)
        {
            int userId = GetCurrentUserId();
            var item = _shoppingTrolleyService.GetItem(userId, id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
    }
}
