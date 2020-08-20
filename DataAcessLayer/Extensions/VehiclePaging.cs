using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Interfaces;
namespace DataAcessLayer.Extensions
{
   public class VehiclePaging : IVehiclePaging
    {
        public int PageSize { get; set; }
        public int? Page { get; set; }

        public int Skip { get; set; }
        public int TotalItems { get; set; }

        public VehiclePaging(int? page)
        {
            PageSize = 5;
            Page = page;
            Skip = PageSize * ((Page ?? 1) - 1);
        }
    }
}
