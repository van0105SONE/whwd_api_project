using Infrastructure.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.RoleRepository
{
    public interface IRoleRepository
    {


        public bool createRole();
        public bool addRole();
        public bool addPosition(PositionTeam position);

        List<string> getRoles();

		List<PositionTeam> getPositionTeamByUserId(string userId);

		List<PositionTeam> getPositionTeams();
        public List<Position> getPositions();

        public List<ProjectTeam> getTeams();

        public Position getPositionById(Guid Id);
        public ProjectTeam getTeamById(Guid Id);
    }
}
