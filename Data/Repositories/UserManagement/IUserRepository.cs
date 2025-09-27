using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities;

namespace Data.Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAsync(UserTypes? userType = null, string name = null, string email = null);
        Task DeleteAllAsync();
        Task<IEnumerable<User>> GetUsersAsync(string tag);


    }
}