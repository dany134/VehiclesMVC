using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using DataAcessLayer.Interfaces;
using DataAcessLayer.Service;
using DataAcessLayer.Repository;

namespace VehicleMVCProject.App_Start
{
    public class VehicleModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleMakeService>().To<VehicleMakeService>();
            Bind<IVehicleModelService>().To<VehicleModelService>();
            Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            Bind<IVehicleModelRepository>().To<VehicleModelRepository>();

        }
    }
}