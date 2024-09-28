using System.Collections.Generic;
using minimal_api.Application.Models.DTOs;
using minimal_api.Application.Models.ModelView;
using minimal_api.Domain.Models.Models;

namespace minimal_api.DomainServices.Interfaces
{
    public interface IAdministratorService
    {
        Administrator Login(LoginDto loginDto);
        long Insert(CreateAdministrator createAdministrator);
        List<AdministratorModelView> GetAll(int? page);
    }
}