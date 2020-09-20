using DotNetCore.AspNetCore;
using DotNetCore.Extensions;
using DotNetCore.Objects;
using DotNetCoreArchitecture.Application;
using DotNetCoreArchitecture.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Web
{
    [ApiController]
    [RouteController]
    public class UsersController : ControllerBase
    {
        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        private IUserService UserService { get; }

        [HttpPost]
        public Task<IDataResult<long>> AddAsync(AddUserModel addUserModel)
        {
            return UserService.AddAsync(addUserModel);
        }

        [HttpDelete("{userId}")]
        public Task<IResult> DeleteAsync(long userId)
        {
            return UserService.DeleteAsync(userId);
        }

        [HttpGet]
        public Task<IEnumerable<UserModel>> ListAsync()
        {
            return UserService.ListAsync();
        }

        [HttpGet("{userId}")]
        public Task<UserModel> SelectAsync(long userId)
        {
            return UserService.SelectAsync(userId);
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<DataResult> SignInAsync(SignInModel signInModel)
        {
            var tokenModelDataResult = await UserService.SignInJwtAsync(signInModel);

            return new DataResult(tokenModelDataResult);
        }

        [HttpPost("SignOut")]
        public Task SignOutAsync()
        {
            var signOutModel = new SignOutModel(User.Id());

            return UserService.SignOutAsync(signOutModel);
        }

        [HttpPut("{userId}")]
        public Task<IResult> UpdateAsync(long userId, UpdateUserModel updateUserModel)
        {
            updateUserModel.UserId = userId;

            return UserService.UpdateAsync(updateUserModel);
        }
    }
}
