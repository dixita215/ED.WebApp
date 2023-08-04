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
using System.Reflection.Metadata;

namespace ED.Service
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users;
        protected readonly ILogger<UserRepository> _logger;

        public UserRepository(ILogger<UserRepository> logger)
        {
            _logger = logger;
            _users = new List<User>
            {
                new User { Id = 1, Username = "Kane" },
                new User { Id = 2, Username = "Barbie"},
            };
        }
       
        public async Task<User> GetUser(int userId)
        {
            _logger.LogInformation("Get User method called");
            try
            {
                return _users.Find(x=>x.Id == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get User with id {userId}");
            }
            return null;
        }
    }
}
