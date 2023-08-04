using ED.Api.Model;
using ED.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED.Service
{
    public class CartRepository : ICartRepository
    {
        // private readonly List<CartItem> _cartItems;
        protected readonly DataContext _context;
        protected readonly ILogger<CartRepository> _logger;

        public CartRepository(ILogger<CartRepository> logger, DataContext dbContext)
        {
            _logger = logger;
            _context = dbContext;
        }
        public async Task<CartItem> AddCartItem(CartItem entity)
        {
            _logger.LogInformation("Add method called");

            try
            {
                _context.CartItems.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Add Cart Item");
                return null;
            }

        }

        public async Task<List<CartItem>> GetAllCartItems(int userId)
        {
            try
            {
                var query = from x in _context.CartItems select x;

                // Applying Filters
                if (userId > 0)
                    query = query.Where(x => x.UserId == userId);
                
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<CartItem>();
            }
        }
        public async Task<CartItem> GetSingleCartItems(int itemId)
        {
            try
            {
                return await _context.CartItems.FirstOrDefaultAsync(x=>x.ItemId == itemId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> RemoveCartItem(int id)
        {
            _logger.LogInformation("Delete method called");
            try
            {
                var entity = await _context.CartItems.FindAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Cart item not found for deletion");
                    return false;
                }

                _context.CartItems.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete Cart Item");
                return false;
            }
        }

        public async Task<bool> UpdateCartItem(CartItem entity)
        {
            _logger.LogInformation("Update method called");
            try
            {
                _context.CartItems.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update Cart Item");
                return false;
            }
        }
    }
}
