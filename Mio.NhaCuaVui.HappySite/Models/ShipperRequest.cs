using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class ShipperRequest
    {
        [Key]
        public int ShipperRequestId { get; set; }
        [MaxLength(128)]
        public string ShipperName { get; set; }
        [MaxLength(20)]
        public string ShipperPhone { get; set; }
        public DateTime AvailableDate { get; set; }
        public int AvailableFromHour { get; set; }
        public int AvailableToHour { get; set; }
        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public District District { get; set; }
        public string Transportation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ValidatedAt { get; set; }
        public bool? IsValidated { get; set; }
        public int? ValidatedUserId { get; set; }
        [ForeignKey("ValidatedUserId")]
        public User ValidatedUser { get; set; }
        public string ValidateMessage { get; set; }
    }
}
