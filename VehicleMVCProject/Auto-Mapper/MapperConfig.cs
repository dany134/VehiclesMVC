using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace VehicleMVCProject.Auto_Mapper
{
    public class MapperConfig
    {
        private readonly IMapper _mapper;
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });
            return config;
        }
    }
}