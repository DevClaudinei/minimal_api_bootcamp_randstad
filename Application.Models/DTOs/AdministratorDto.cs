using minimal_api.Application.Models.Enums;

namespace minimal_api.Application.Models.DTOs;

public class AdministratorDto
{
    public long Id { get; set; }
    public string Email { get; set; }
    public Profile? Profile { get; set; }
}