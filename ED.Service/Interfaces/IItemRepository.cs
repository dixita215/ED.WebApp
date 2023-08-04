using ED.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED.Service.Interfaces
{
    public interface IItemRepository
    {
        public Task<List<Item>> GetAllItems();

        public Task<Item> GetItem(int itemId);

    }
}
