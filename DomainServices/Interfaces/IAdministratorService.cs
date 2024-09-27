using minimal_api.Application.Models.DTOs;
using minimal_api.Domain.Models.Models;

namespace minimal_api.DomainServices.Interfaces
{
    public interface IAdministratorService
    {
        Administrator Login(LoginDto loginDto);
    }
}