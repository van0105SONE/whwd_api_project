using ApplicationCore.Dtos;
using ApplicationCore.Dtos.UserDto;
using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Users;



namespace Services.Service.UserService
{
    public interface IUserService
    {
        Task<ErrorOr<MessageReponse<UserReponseDto>>> createUser(UserDto user);
        Task<ErrorOr<MessageReponse<UserReponseDto>>> updateUser(string userId, UserDto user);
        Task<ErrorOr<bool>> deleteUser(Guid Id);
        Task<ApplicationUser> getUserById(string Id);
        List<UserReponseDto> GetUsers(BaseFilter filter);
    }
}
