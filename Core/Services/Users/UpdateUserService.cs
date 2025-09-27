using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using BusinessEntities;
using Common;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateUserService : IUpdateUserService
    {
        public async Task UpdateAsync(User user, string name, string email, UserTypes type, decimal? annualSalary, IEnumerable<string> tags)
        {
            // Only update email if a new value is provided
            if (!string.IsNullOrEmpty(email))
            {
                user.SetEmail(email);
            }
            user.SetName(name);
            user.SetType(type);
            if (annualSalary.HasValue)
            {
                user.SetMonthlySalary(annualSalary.Value / 12);
            }
            user.SetTags(tags);
 
            await Task.CompletedTask;
        }
    }
}