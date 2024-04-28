using Infrastructure.Model.Volunteer;
using Infrastructure.Model.Work;

namespace whwd_web_api.Dtos.UserDto
{
    public class UserDto
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Occupation { get; set; }
        public Department Department { get; set; }
        public Generation Generation { get; set; }

    }
}
