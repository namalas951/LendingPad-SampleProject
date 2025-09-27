using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities;

namespace Core.Services.Users
{
    public interface IUpdateUserService
    {
        Task UpdateAsync(User user, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags);


    }
}