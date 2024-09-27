namespace minimal_api.Application.Models.DTOs
{
    public class LoginDto
    {
        public LoginDto() {}
        
        public LoginDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}