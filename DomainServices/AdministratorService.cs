using System;
using System.Collections.Generic;
using System.Linq;
using minimal_api.Application.Models.DTOs;
using minimal_api.Application.Models.Enums;
using minimal_api.Application.Models.ModelView;
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

        public long Insert(CreateAdministrator createAdministrator)
        {
            ValidateFieldsAdministrator(createAdministrator);
            var administrator = TransformDtoInModel(createAdministrator);
            
            _dataContext.Administrators.Add(administrator);
            _dataContext.SaveChanges();

            return administrator.Id;
        }

        public List<AdministratorModelView> GetAll(int? page)
        {
            var query = _dataContext.Administrators.AsQueryable();
            var itemsPerPage = 10;

            if (page != null) 
                query = query.Skip((int)((page - 1) * itemsPerPage)).Take(itemsPerPage);

            var administrators = CreateAdministratorModelView(query);
            
            return administrators.ToList();
        }

        private List<AdministratorModelView> CreateAdministratorModelView(IQueryable<Administrator> administrators)
        {
            var modelView = new List<AdministratorModelView>();
            
            foreach (var administrator in administrators)
            {
                var admin = new AdministratorModelView()
                {
                    Id = administrator.Id,
                    Email = administrator.Email,
                    Profile = administrator.Profile.ToString()
                };
                
                modelView.Add(admin);
            }
        
            return modelView;
        }
        
        private void ValidateFieldsAdministrator(CreateAdministrator createAdministrator)
        {
            if (string.IsNullOrEmpty(createAdministrator.Email))
                throw new BadRequestException("Name is required");
            if (string.IsNullOrEmpty(createAdministrator.Password))
                throw new BadRequestException("Brand is required");
            if (!Enum.TryParse<Profile>(createAdministrator.Profile, true, out _))
                throw new BadRequestException($"Perfil '{createAdministrator.Profile}' não é válido.");
        }

        private Administrator TransformDtoInModel(CreateAdministrator createAdministrator)
        {
            var x = new Administrator
            {
                Email = createAdministrator.Email,
                Password = createAdministrator.Password,
                Profile = (Profile)Enum.Parse(typeof(Profile), createAdministrator.Profile)
            };

            return x;
        }
    }
}