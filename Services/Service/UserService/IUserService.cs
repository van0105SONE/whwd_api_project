using ApplicationCore.Dtos.UserDto;
using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Services.Service.UserService
{
    public interface IUserService
    {
        Task<bool> createUser(UserDto user);
        Task<ErrorOr<bool>> updateUser(UserUpdateDto user);
        Task<ErrorOr<bool>> deleteUser(Guid Id);
        Task<ApplicationUser> getUserById(string Id);
        List<ApplicationUser> GetUsers(BaseFilter filter);
        List<UserType> getUserTypes();
    }
}
