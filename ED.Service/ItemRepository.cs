using ED.Api.Model;
using ED.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ED.Service
{
    public class ItemRepository : IItemRepository
    {
        private readonly List<Item> _items;
        protected readonly ILogger<ItemRepository> _logger;

        public ItemRepository(ILogger<ItemRepository> logger)
        {
            _logger = logger;
            _items = new List<Item>
            {
                new Item { Id = 1, ProductName = "Victoria Bitter",Description="Beer", Price = 21.49M, Quantity = 2 },
                new Item { Id = 2, ProductName = "Crown Lager",Description="Beer", Price = 22.99M, Quantity = 1 },
                new Item { Id = 3, ProductName = "Coopers",Description="Beer", Price = 20.49M, Quantity = 3 },
                new Item { Id = 4, ProductName = "Tooheys Extra Dry",Description="Beer", Price = 19.99M, Quantity = 3 }
            };
        }
        public Task<List<Item>> GetAllItems()
        {
            _logger.LogInformation("Get All Item method called");
            try
            {
                return Task.Run(() =>
                {
                   return _items;
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to Items");
            }
            return null;
        }

        public async Task<Item> GetItem(int itemId)
        {
            _logger.LogInformation("Get Item method called");
            try
            {
                return _items.Find(x=>x.Id == itemId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get Item with id {itemId}");
            }
            return null;
        }
    }
}
