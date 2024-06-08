using Infrastructure.Model.Address;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Place
{
    public class FundRaisingPlace : BaseModel
    {
        public required string  PlaceName { get; set; }
        public string?  PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Other { get; set; }
        public string? Facebook { get; set; }
        public double Longtitude { get; set; } = 0;
        public double Latitude { get; set; } = 0;

        public string Status { get; set; }
        public ApplicationUser CoordinateBy { get; set; }

        public required Village Village { get; set; }

    }
}
