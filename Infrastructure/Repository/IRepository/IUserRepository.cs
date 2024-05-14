using Infrastructure.Model.University;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.IRepository
{
    public interface IUserRepository
    {
    
        /// <summary>
        /// position
        /// </summary>
        /// <returns></returns>
        bool addPosition(Position position);
        bool deletePosition(Guid Id);
        Position getPositionById (Guid Id);
        List<Position> getPositions();

        /// <summary>
        /// Team blueprint
        /// </summary>
        /// <returns></returns>
        bool addTeam(ProjectTeam teamParams);
        bool deleteTeam(Guid Id);
        ProjectTeam getTeamById(Guid Id);
        List<ProjectTeam> GetTeams();



        /// get user Types
        /// 

        List<UserType> getUserTypes();
        UserType getUserTypeById(string Id);

        

    }
}
