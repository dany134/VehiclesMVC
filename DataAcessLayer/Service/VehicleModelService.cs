using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Models;
using DataAcessLayer.Interfaces;
using DataAcessLayer.Context;
using DataAcessLayer.Repository;
using DataAcessLayer.Extensions;

namespace DataAcessLayer.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private IVehicleModelRepository _modelRepository;
        public VehicleModelService()
        {
            _modelRepository = new VehicleModelRepository(new VehicleContext());
        }
        public VehicleModelService(IVehicleModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }
    
        public async Task<IEnumerable<VehicleModel>> GetModelsList(VehicleFilters filter, VehiclePaging page)
        {
            return await _modelRepository.GetModelsList(filter, page);
        }
        public async Task<IEnumerable<VehicleModel>> GetModels()
        {
            return await _modelRepository.GetModels();
        }
        public async Task<VehicleModel> GetModelById(int? modelId)
        {
            return await _modelRepository.GetModelById(modelId);
        }
        public async Task<bool> InsertModel(VehicleModel model)
        {
            try
            {
                await _modelRepository.InsertModel(model);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateModel(VehicleModel model)
        {
            try
            {
                await _modelRepository.UpdateModel(model);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteModel(VehicleModel model)
        {
            try
            {
                await _modelRepository.DeleteModel(model);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
