namespace minimal_api.Application.Models.DTOs;

public abstract class CreateAdministrator
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Profile { get; set; }
}