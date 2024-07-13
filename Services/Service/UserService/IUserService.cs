using ApplicationCore.Dtos;
using ApplicationCore.Dtos.UserDto;
using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Users;



namespace Services.Service.UserService
{
    public interface IUserService
    {
        Task<ErrorOr<MessageReponse<ApplicationUser>>> createUser(UserDto user);
        Task<ErrorOr<ApplicationUser>> updateUser(UserUpdateDto user);
        Task<ErrorOr<bool>> deleteUser(Guid Id);
        Task<ApplicationUser> getUserById(string Id);
        List<ApplicationUser> GetUsers(BaseFilter filter);
    }
}
