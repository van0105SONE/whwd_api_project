using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Users
{
    public class PositionTeam
    {
        public Guid Id { get; set; }
        public Position Position { get; set; }
        public ProjectTeam Team { get; set; }
    }
}
