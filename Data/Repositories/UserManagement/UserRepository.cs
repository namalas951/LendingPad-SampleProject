using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities;
using Common;
using Data.Indexes;
using Data.Repositories.Users;
using Raven.Client;

namespace Data.Repositories.UserManagement
{
    [AutoRegister]
    public class UserRepository : RavenRepository<User>, IUserRepository
    {
        private readonly IAsyncDocumentSession _documentSession;

        public UserRepository(IAsyncDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public async Task<IEnumerable<User>> GetAsync(UserTypes? userType = null, string name = null, string email = null)
        {
            var query = _documentSession.Advanced.AsyncDocumentQuery<User, UsersListIndex>();

            var hasFirstParameter = false;
            if (userType != null)
            {
                query = query.WhereEquals("Type", (int)userType);
                hasFirstParameter = true;
            }

            if (name != null)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                else
                {
                    hasFirstParameter = true;
                }
                query = query.Where($"Name:*{name}*");
            }

            if (email != null)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query = query.WhereEquals("Email", email);
            }
            var result = await query.ToListAsync();
            return result;
        }

        public async Task DeleteAllAsync()
        {
            await base.DeleteAllAsync<UsersListIndex>();
        }

        public async Task<IEnumerable<User>> GetUsersAsync(string tag)
        {
            var users = await _documentSession.Query<User>()
           .Where(u => u.Tags.Contains(tag))
            .ToListAsync();

            return users;
        }
    }
}