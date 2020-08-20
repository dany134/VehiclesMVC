using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Repository;
using DataAcessLayer.Interfaces;
using DataAcessLayer.Models;
using DataAcessLayer.Extensions;

namespace DataAcessLayer.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private  IVehicleMakeRepository _vehicleRepository;
        public VehicleMakeService()
        {
            _vehicleRepository = new VehicleMakeRepository(new Context.VehicleContext());
        }
        public VehicleMakeService(IVehicleMakeRepository vehicleMakeRepository)
        {
            _vehicleRepository = vehicleMakeRepository;
        }
        public async Task<IEnumerable<VehicleMake>> GetMakesList(VehicleFilters filter, VehiclePaging pages)
        {
            return await _vehicleRepository.GetMakesList(filter, pages);
        }
        public async Task<IEnumerable<VehicleMake>> GetMakes()
        {
            return  await _vehicleRepository.GetMakes();
        }
        public async Task<VehicleMake> GetMakeById(int? makeId)
        {
            VehicleMake make = await _vehicleRepository.GetMakeById(makeId);
            return make;
        }
        public async Task<bool> InsertMake(VehicleMake make)
        {
            try
            {
           await _vehicleRepository.InsertMake(make);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public async Task<bool> DeleteMake(VehicleMake make)
        {
            try
            {
            await _vehicleRepository.DeleteMake(make);
                return true;
            }
            catch
            {
                return false;
            }
            
            
        }
        public async Task<bool> UpdateMake(VehicleMake make)
        {
            try
            {
            await _vehicleRepository.UpdateMake(make);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
