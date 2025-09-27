using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities;

namespace Core.Services.Users
{
    public interface IGetUserService
    {
        Task<User> GetUserAsync(Guid id);

        Task<IEnumerable<User>> GetUsersAsync(UserTypes? userType = null, string name = null, string email = null);

        Task<IEnumerable<User>> GetUsersAsync(string type);
    }
}