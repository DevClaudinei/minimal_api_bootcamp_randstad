using System.Linq;
using minimal_api.Application.Models.DTOs;
using minimal_api.Domain.Models.Models;
using minimal_api.DomainServices.Exceptions;
using minimal_api.DomainServices.Interfaces;
using minimal_api.Infrastructure.Data;

namespace minimal_api.DomainServices
{
    public class AdministratorService : IAdministratorService
    {
        private readonly DataContext _dataContext;

        public AdministratorService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Administrator Login(LoginDto loginDto)
        {
            var admin = _dataContext.Administrators
                .FirstOrDefault(a => a.Email == loginDto.Email && a.Password == loginDto.Password);
            
            if (admin == null)
                throw new BadRequestException($"Login or password are incorrect.");       
            
            return admin;
        }
    }
}