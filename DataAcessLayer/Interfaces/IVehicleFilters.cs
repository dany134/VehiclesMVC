using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Interfaces
{
    internal interface IVehicleFilters
    {
        string SearchString { get; set; }
        string CurrentFilter { get; set; }
        string FilterBy { get; set; }

        bool Filters();
    }
}
