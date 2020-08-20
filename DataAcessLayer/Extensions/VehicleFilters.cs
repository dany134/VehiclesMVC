using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Interfaces;
namespace DataAcessLayer.Extensions
{
    public class VehicleFilters : IVehicleFilters
    {
        public string SearchString { get; set; }
        public string CurrentFilter { get; set; }
        public string FilterBy { get; set; }

        public VehicleFilters(string searchString, string currentFilter)
        {
            CurrentFilter = currentFilter;
            SearchString = searchString;
        }
        public bool Filters()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                FilterBy = SearchString;
                return true;
            }
            if (!string.IsNullOrEmpty(CurrentFilter))
            {
                FilterBy = CurrentFilter;
                return true;
            }
            return false;
        }
    }
}
