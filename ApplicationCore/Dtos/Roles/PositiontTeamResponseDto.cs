using ApplicationCore.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Roles
{
    public class PositiontTeamResponseDto
    {
        public Guid Id { get; set; }
        public PositionResponse Position { get; set; }
        public ProjectTeamResponse Team { get; set; }
    }
}
