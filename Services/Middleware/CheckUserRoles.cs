using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Services.Middleware
{
    public class CheckUserRoles
    {
        private  DatabaseContexts _dbContexts { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        public CheckUserRoles(DatabaseContexts dbContext, UserManager<ApplicationUser> userManager) {
            _dbContexts = dbContext;
            _userManager = userManager;
        }


        public  async Task<bool> IsUserAllowed(ApplicationUser user, string role, string position)
          
        {
            bool isAllowed = await _userManager.IsInRoleAsync(user, role);
            if (isAllowed)
            {
              bool isExist = _dbContexts.position_teams.Include(t => t.Position).Any(t => t.Position.PositionName == position);
                if (isExist)
                {
                    return true;
                }else
                {
                    return false;
                }
            }else
            {
                return false;
            }
        }
    }
}
