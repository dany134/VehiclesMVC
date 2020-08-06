using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAcessLayer.Models;

namespace VehicleMVCProject.ViewModels
{
    public class VehicleMakeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}