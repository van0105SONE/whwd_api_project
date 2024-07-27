using ApplicationCore.Dtos.Address;
using ApplicationCore.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Place
{
    public class PlaceResponseDto
    {
        public Guid Id { get; set; }
        public required string PlaceName { get; set; }
        public string Status { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Other { get; set; }
        public string? Facebook { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }
        
        public string? googleMapLink { get; set; }
        public VillageReponseDto village { get; set; }

        public UserReponseDto CoordinateBy { get; set; }

    }
}
