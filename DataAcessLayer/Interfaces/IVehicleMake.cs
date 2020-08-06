using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Models;
namespace DataAcessLayer.Interfaces
{
    internal interface IVehicleMake
    {
        int Id { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
