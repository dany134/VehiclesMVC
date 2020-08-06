using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Interfaces;

namespace DataAcessLayer.Models
{
    public class VehicleModel : IVehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public int VehicleMakeID { get; set; }
        public virtual VehicleMake VehicleMake { get; set; }
    }
}
