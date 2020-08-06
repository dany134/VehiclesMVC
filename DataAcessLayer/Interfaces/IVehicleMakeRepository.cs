using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Models;

namespace DataAcessLayer.Interfaces
{
   public interface IVehicleMakeRepository : IDisposable
    {
        Task<IEnumerable<VehicleMake>> GetMakes();
        Task<VehicleMake> GetMakeById(int? makeId);
        Task<bool> InsertMake(VehicleMake make);
        Task<bool> UpdateMake(VehicleMake make);
        Task<bool> DeleteMake(VehicleMake make);
    }
}
