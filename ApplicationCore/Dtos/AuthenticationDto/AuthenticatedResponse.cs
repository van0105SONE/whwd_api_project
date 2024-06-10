﻿using ApplicationCore.Dtos.UserDto;

namespace ApplicationCore.Dtos.AuthenticationDto
{
    public class AuthenticatedResponse
    {
        public String Token { get; set; }   
        public String RefreshToken { get; set; }

        public UserReponseDto userDto { get; set; }
    }
}
