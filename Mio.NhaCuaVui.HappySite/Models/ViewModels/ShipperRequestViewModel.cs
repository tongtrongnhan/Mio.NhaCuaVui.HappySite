using System;
using System.Collections.Generic;

namespace Mio.NhaCuaVui.HappySite.Models.ViewModels
{
    public class ShipperRequestViewModel
    {
        public List<City> Cities { get; set; }
        public List<SelectListItem> Hours { get; set; }
        public int ShipperRequestId { get; set; }
        public string ShipperName { get; set; }
        public string ShipperPhone { get; set; }
        public DateTime AvailableDate { get; set; }
        public int AvailableFromHour { get; set; }
        public int AvailableToHour { get; set; }
        public int DistrictId { get; set; }
        public string Transportation { get; set; }
        public bool? IsValidated { get; set; }
        public string ValidateMessage { get; set; }
        public User ValidatedBy { get; set; }
        public DateTime? ValidatedAt { get; set; }
        public District District { get; set; }
        public string DistrictInfo
        {
            get
            {
                if (District == null) return "Không rõ khu vực";
                return $"{District.Name} - {District.City.Name}";
            }
        }
    }
}
