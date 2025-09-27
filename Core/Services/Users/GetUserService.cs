using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessEntities;
using Common;
using Data.Repositories.Users;

namespace Core.Services.Users
{
    [AutoRegister]
    public class GetUserService : IGetUserService
    {
        private readonly IUserRepository _userRepository;

        public GetUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _userRepository.GetAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync(UserTypes? userType = null, string name = null, string email = null)
        {
            return await _userRepository.GetAsync(userType, name, email);
        }


        public async Task<IEnumerable<User>> GetUsersAsync(string type)
        {
            return await _userRepository.GetUsersAsync(type);
        }

    }
}