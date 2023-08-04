using ED.Api.Model;
using ED.Service;
using ED.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;

namespace ED.Web.Controllers
{
    [Route("api/Cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartController> _logger;
        public CartController(ICartRepository cartRepo,
            IUserRepository userRepo,
            ILogger<CartController> logger)
        {
            _cartRepository = cartRepo;
            _userRepository = userRepo;
            _logger = logger;
        }
        // GET: api/cart
        [HttpGet]
        public async Task<ActionResult<List<CartItem>>> GetCartItemsList()
        {
            try
            {
                var cartItems = await _cartRepository.GetAllCartItems(1);
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                _logger.LogError(ex, "Failed to retrieve cart items");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }



        // POST: api/cart
        [HttpPost]
        public async Task<IActionResult> AddCartItem(Item item)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID
            int userId = 1;
            var cartItems = await _cartRepository.GetAllCartItems(userId);

            var existingItem = cartItems.FirstOrDefault(x => x.ItemId == item.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                await _cartRepository.UpdateCartItem(existingItem);
            }
            else
            {
                CartItem cartItem = new CartItem()
                {

                    ItemId = item.Id,
                    Name = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    UserId = 1
                };
                await _cartRepository.AddCartItem(cartItem);
            }


            return CreatedAtAction(nameof(AddCartItem), new { id = item.Id }, item);
        }
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetCartItemCount()
        {
            var cartItemList = await _cartRepository.GetAllCartItems(1);
            int itemCount = cartItemList.Sum(item => item.Quantity);
            return itemCount;
        }
        // PUT: api/cart/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, CartItem updatedItem)
        {
            var cartItem = await _cartRepository.GetSingleCartItems(updatedItem.ItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = updatedItem.Quantity;


            if (cartItem.Quantity == 0)
            {
                await _cartRepository.RemoveCartItem(cartItem);
            }
            else
            {
                await _cartRepository.UpdateCartItem(cartItem);
            }

            return Ok();
        }

        // DELETE: api/cart/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
          
            var cartItem = await _cartRepository.GetSingleCartItems(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            await _cartRepository.RemoveCartItem(cartItem);


            return Ok();
        }
    }
}
