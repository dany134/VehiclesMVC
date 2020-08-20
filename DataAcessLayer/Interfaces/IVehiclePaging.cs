using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Interfaces
{
    internal interface IVehiclePaging
    {
        int? Page { get; set; }
      
        int TotalItems { get; set; }
        int PageSize { get; set; }
    }
}
