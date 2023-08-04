using ED.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED.Service.Interfaces
{
    public interface IUserRepository
    {
       
        public Task<User> GetUser(int userId);

    }
}
