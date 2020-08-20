using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Models;
using DataAcessLayer.Extensions;
namespace DataAcessLayer.Interfaces
{
   public interface IVehicleMakeService
    {
        Task<IEnumerable<VehicleMake>> GetMakesList(VehicleFilters filter, VehiclePaging page);
        Task<IEnumerable<VehicleMake>> GetMakes();
        Task<VehicleMake> GetMakeById(int? makeId);
        Task<bool> InsertMake(VehicleMake make);
        Task<bool> DeleteMake(VehicleMake make);
        Task<bool> UpdateMake(VehicleMake make);
    }
}
