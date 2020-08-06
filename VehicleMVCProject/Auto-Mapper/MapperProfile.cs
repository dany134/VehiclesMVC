using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DataAcessLayer.Models;
using VehicleMVCProject.ViewModels;

namespace VehicleMVCProject.Auto_Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>();
            CreateMap<VehicleModel, VehicleModelViewModel>();
        }
    }
}