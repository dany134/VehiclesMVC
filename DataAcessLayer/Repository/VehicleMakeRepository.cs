using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAcessLayer.Models;
using System.Threading.Tasks;
using DataAcessLayer.Context;
using System.Data.Entity;
using DataAcessLayer.Interfaces;
namespace DataAcessLayer.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private VehicleContext context;
        public VehicleMakeRepository(VehicleContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<VehicleMake>> GetMakes()
        {
            IEnumerable<VehicleMake> make = await context.VehicleMakes.ToListAsync();
            return make;
        }
        public async Task<VehicleMake> GetMakeById(int? makeId)
        {
            VehicleMake make = await context.VehicleMakes.FindAsync(makeId);
            return make;
        }
        public async Task<bool> InsertMake(VehicleMake make)
        {
            try
            {
                context.VehicleMakes.Add(make);
                await context.SaveChangesAsync();
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
                context.VehicleMakes.Remove(make);
                await context.SaveChangesAsync();
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
            context.Entry(make).State = System.Data.Entity.EntityState.Modified;
            await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
               
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
