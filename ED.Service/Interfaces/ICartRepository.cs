using ED.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED.Service.Interfaces
{
    public interface ICartRepository
    {
        Task<List<CartItem>> GetAllCartItems(int userId);
        Task<CartItem> GetSingleCartItems(int itemId);
        Task<CartItem> AddCartItem(CartItem entity);
        Task<bool> UpdateCartItem(CartItem entity);
        Task<bool> RemoveCartItem(int itemId);        

    }
}
