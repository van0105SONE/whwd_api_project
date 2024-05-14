using ApplicationCore.Filter;
using Infrastructure.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.UserService
{
    public interface IUserService
    {
        Task<bool> createUser(ApplicationUser user);
        bool updateUser(ApplicationUser user);
        bool deleteUser(Guid Id);

        List<ApplicationUser> GetUsers(BaseFilter filter);
        List<UserType> getUserTypes();
    }
}
