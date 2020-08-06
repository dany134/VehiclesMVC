using AutoMapper;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleMVCProject.Controllers;
using System.Web;
using VehicleMVCProject.Auto_Mapper;

namespace VehicleMVCProject.App_Start
{
    public class MapperModule : NinjectModule
    {
        public override void Load()
        {
            var mapperconfig = CreateConfing();
            Bind<MapperConfiguration>().ToConstant(mapperconfig).InSingletonScope();
            Bind<IMapper>().ToMethod(ctx => new Mapper(mapperconfig, type => ctx.Kernel.Get(type)));
            Bind<VehicleMakeController>().ToSelf().InTransientScope();
            Bind<VehicleModelController>().ToSelf().InTransientScope();
        }
        private MapperConfiguration CreateConfing()
        {
            var config = new MapperConfiguration(cfg =>
          {
              cfg.AddProfile<MapperProfile>();
          });
            return config;
        }
    }
}