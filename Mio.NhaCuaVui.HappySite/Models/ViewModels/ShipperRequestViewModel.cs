using System;
using System.Collections.Generic;

namespace Mio.NhaCuaVui.HappySite.Models.ViewModels
{
    public class ShipperRequestViewModel
    {
        public List<City> Cities { get; set; }
        public List<SelectListItem> Hours { get; set; }
        public string ShipperName { get; set; }
        public string ShipperPhone { get; set; }
        public DateTime AvailableDate { get; set; }
        public int AvailableFromHour { get; set; }
        public int AvailableToHour { get; set; }
        public int DistrictId { get; set; }
        public string Transportation { get; set; }


    }
}
