using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Users;
using Raven.Abstractions.Exceptions;
using WebApi.App_Start;
using WebApi.Models.Users;

namespace WebApi.Controllers
{
    [RoutePrefix("users")]

    [ContextInitializeAttribute]
    public class UserController : BaseApiController
    {
        private readonly ICreateUserService _createUserService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IGetUserService _getUserService;
        private readonly IUpdateUserService _updateUserService;

        public UserController(ICreateUserService createUserService, IDeleteUserService deleteUserService, IGetUserService getUserService, IUpdateUserService updateUserService)
        {
            _createUserService = createUserService;
            _deleteUserService = deleteUserService;
            _getUserService = getUserService;
            _updateUserService = updateUserService;
        }

        [Route("{userId:guid}/create")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateUser(Guid userId, [FromBody] UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestForModelState();
            }
            try
            {
                var user = await _createUserService.CreateAsync(userId, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
                return Found(new UserData(user));
            }
            catch (ConcurrencyException)
            {
                // User already exists
                return Conflict("Users already exists with same userID.");
            }
            catch (Exception ex)
            {
                // Other errors
                return InternalServerError(ex);
            }
        }

        [Route("{userId:guid}/update")]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateUser(Guid userId, [FromBody] UserModel model)
        {
            var user = await _getUserService.GetUserAsync(userId);
            if (user == null)
            {
                return DoesNotExist();
            }
            await _updateUserService.UpdateAsync(user, model.Name, model.Email, model.Type, model.AnnualSalary, model.Tags);
            return Found(new UserData(user));
        }

        [Route("{userId:guid}/delete")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteUser(Guid userId)
        {
            var user = await _getUserService.GetUserAsync(userId);
            if (user == null)
            {
                return DoesNotExist();
            }
            await _deleteUserService.DeleteAsync(user);
            return Found();
        }

        [Route("{userId:guid}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetUser(Guid userId)
        {
            var user = await _getUserService.GetUserAsync(userId);
            return Found(new UserData(user));
        }

        [Route("list")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetUsers(int skip, int take, UserTypes? type = null, string name = null, string email = null)
        {
            var users = (await _getUserService.GetUsersAsync(type, name, email))
                                       .Skip(skip).Take(take)
                                       .Select(q => new UserData(q))
                                       .ToList();
            return Found(users);
        }

        [Route("clear")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAllUsers()
        {
            await _deleteUserService.DeleteAllAsync();
            return Found();
        }

        [Route("list/tag")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetUsersByTag([FromUri] string tag)
        {
            
            if (string.IsNullOrEmpty(tag))
            {
                return BadRequest("Tag is required.");
            }

            var users = await _getUserService.GetUsersAsync(tag);
            var userDataList = users.Select(u => new UserData(u)).ToList();
            return Found(userDataList);

        }
    }

}