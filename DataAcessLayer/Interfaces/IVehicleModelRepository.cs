using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Models;
namespace DataAcessLayer.Interfaces
{
    public interface IVehicleModelRepository : IDisposable
    {
        Task<IEnumerable<VehicleModel>> GetModels();
        Task<VehicleModel> GetModelById(int? modelId);
        Task<bool> InsertModel(VehicleModel model);
        Task<bool> UpdateModel(VehicleModel model);
        Task<bool> DeleteModel(VehicleModel model);
    }
}
