using ED.Api.Model;

namespace ED.Api.Services
{
    public interface IShoppingTrolleyService
    {
        IEnumerable<ShoppingCartItem> GetAllItems(int userId);
        ShoppingCartItem AddItem(int userId, ShoppingCartItem newItem);
        void UpdateItem(int userId, int itemId, ShoppingCartItem updatedItem);
        void RemoveItem(int userId, int itemId);
        ShoppingCartItem GetItem(int userId, int itemId);

        User Authenticate(string username, string password);
    }
    public class ShoppingTrolleyService : IShoppingTrolleyService
    {
        private readonly Dictionary<int, List<ShoppingCartItem>> _shoppingTrolleys;
        private readonly List<User> _users;

        public ShoppingTrolleyService()
        {
            _shoppingTrolleys = new Dictionary<int, List<ShoppingCartItem>>();
            _users = new List<User>
        {
            new User { Id = 1, Username = "user1", Password = "password1" },
            new User { Id = 2, Username = "user2", Password = "password2" },
        };
        }

        public IEnumerable<ShoppingCartItem> GetAllItems(int userId)
        {
            if (_shoppingTrolleys.TryGetValue(userId, out var shoppingTrolley))
            {
                return shoppingTrolley;
            }

            return new List<ShoppingCartItem>();
        }

        public ShoppingCartItem AddItem(int userId, ShoppingCartItem newItem)
        {
            if (!_shoppingTrolleys.ContainsKey(userId))
            {
                _shoppingTrolleys[userId] = new List<ShoppingCartItem>();
            }

            // Generate a unique Id (for simplicity, incrementing the last Id in the list)
            int newId = _shoppingTrolleys[userId].Count > 0 ? _shoppingTrolleys[userId][_shoppingTrolleys[userId].Count - 1].Id + 1 : 1;
            newItem.Id = newId;

            _shoppingTrolleys[userId].Add(newItem);

            return newItem;
        }

        public void UpdateItem(int userId, int itemId, ShoppingCartItem updatedItem)
        {
            var shoppingTrolley = _shoppingTrolleys.GetValueOrDefault(userId);

            if (shoppingTrolley != null)
            {
                var item = shoppingTrolley.Find(i => i.Id == itemId);
                if (item != null)
                {
                    item.ProductName = updatedItem.ProductName;
                    item.Price = updatedItem.Price;
                    item.Quantity = updatedItem.Quantity;
                }
            }
        }

        public void RemoveItem(int userId, int itemId)
        {
            var shoppingTrolley = _shoppingTrolleys.GetValueOrDefault(userId);

            if (shoppingTrolley != null)
            {
                var item = shoppingTrolley.Find(i => i.Id == itemId);
                if (item != null)
                {
                    shoppingTrolley.Remove(item);
                }
            }
        }

        public ShoppingCartItem GetItem(int userId, int itemId)
        {
            var shoppingTrolley = _shoppingTrolleys.GetValueOrDefault(userId);
            return shoppingTrolley?.Find(i => i.Id == itemId);
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }
    }
}
