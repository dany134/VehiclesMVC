using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Models;
namespace DataAcessLayer.Interfaces
{
    internal interface IVehicleModel
    {
      int Id { get; set; }
      string Name { get; set; }
      string Abrv { get; set; }
      int VehicleMakeID { get; set; }
      VehicleMake VehicleMake { get; set; }
    }
}
