using System.Collections.Generic;
using minimal_api.Application.Models.DTOs;
using minimal_api.Domain.Models.Models;

namespace minimal_api.DomainServices.Interfaces
{
    public interface IVehicleService
    {
        IEnumerable<Vehicle> GetAll(int? page = 1, string name = null, string brand = null);
        Vehicle GetById(long id);
        long Insert(VehicleDto vehicleDto);
        void Update(long id, VehicleDto vehicleDto);
        void Delete(long id);
    }
}