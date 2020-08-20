using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataAcessLayer.Context;
using DataAcessLayer.Models;
using DataAcessLayer.Extensions;
using DataAcessLayer.Interfaces;
namespace DataAcessLayer.Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private VehicleContext context;
        public VehicleModelRepository(VehicleContext context)
        {
            this.context = context;
            
        }
     
     
        public async Task<IEnumerable<VehicleModel>> GetModelsList(VehicleFilters filter, VehiclePaging page)
        {
            IQueryable<VehicleModel> model = from m in context.VehicleModels.Include(m => m.VehicleMake) select m;
            if (filter.Filters())
            {
                model = model.Where(v => v.Name.Contains(filter.FilterBy) || v.Abrv.Contains(filter.FilterBy) || v.VehicleMake.Name.Contains(filter.FilterBy));
            }
            page.TotalItems = model.Count();
            return await model.OrderBy(m => m.Name).Skip(page.Skip).Take(page.PageSize).ToListAsync();
        }
      
        public async Task<IEnumerable<VehicleModel>> GetModels()
        {
            
            return  await context.VehicleModels.Include(v => v.VehicleMake).ToListAsync();
        }
        public async Task<VehicleModel> GetModelById(int? modelId)
        {
            return await context.VehicleModels.FindAsync(modelId);
        }
        public async Task<bool> InsertModel(VehicleModel model)
        {
            try
            {
                context.VehicleModels.Add(model);
               await context.SaveChangesAsync();
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
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
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
                context.VehicleModels.Remove(model);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        private bool disposed = false;
        protected virtual void Dispose(bool dispsing)
        {
            if (!this.disposed)
            {
                if (dispsing)
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
