namespace whwd_web_api.Dtos.AuthenticationDto
{
    public class AuthenticatedResponse
    {
        public String Token { get; set; }   
        public String RefreshToken { get; set; }
    }
}
