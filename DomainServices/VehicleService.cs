using System.Collections.Generic;
using System.Linq;
using minimal_api.Application.Models.DTOs;
using minimal_api.Domain.Models.Models;
using minimal_api.DomainServices.Exceptions;
using minimal_api.DomainServices.Interfaces;
using minimal_api.Infrastructure.Data;

namespace minimal_api.DomainServices
{
    public class VehicleService : IVehicleService
    {
        private readonly DataContext _dataContext;

        public VehicleService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public long Insert(VehicleDto vehicleDto)
        {
            var vehicle = VehicleTransformation(vehicleDto);
            _dataContext.Vehicles.Add(vehicle);
            _dataContext.SaveChanges();

            return vehicle.Id;
        }

        private Vehicle VehicleTransformation(VehicleDto vehicleDto)
        {
            var vehicle = new Vehicle
            {
                Name = vehicleDto.Name,
                Brand = vehicleDto.Brand,
                Year = vehicleDto.Year
            };

            return vehicle;
        }

        public IEnumerable<Vehicle> GetAll(int? page = 1, string name = null, string brand = null)
        {
            var query = _dataContext.Vehicles.AsQueryable();
            
            if (!string.IsNullOrEmpty(name)) 
                query = query.Where(x => x.Name.ToLower().Contains(name));

            var itemsPerPage = 10;

            if (page != null) 
                query = query.Skip(((int)page - 1) * itemsPerPage).Take(itemsPerPage);

            return query;
        }

        public Vehicle GetById(long id)
        {
            var vehicle = _dataContext.Vehicles.FirstOrDefault(x => x.Id.Equals(id));

            if (vehicle is null) throw new NotFoundException($"Vehicle for Id: {id} not found.");

            return vehicle;
        }

        private void UpdateVehicle(Vehicle vehicleFound, VehicleDto vehicleDto)
        {
            vehicleFound.Name = vehicleDto.Name;
            vehicleFound.Brand = vehicleDto.Brand;
            vehicleFound.Year = vehicleDto.Year;
        }

        public void Update(long id, VehicleDto vehicleDto)
        {
            var vehicleFound = GetById(id);
            UpdateVehicle(vehicleFound, vehicleDto);
            
            _dataContext.Update(vehicleFound);
            _dataContext.SaveChanges();
        }

        public void Delete(long id)
        {
            var vehicle = GetById(id);

            _dataContext.Vehicles.Remove(vehicle);
            _dataContext.SaveChanges();
        }
    }
}