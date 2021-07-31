using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class District
    {
        [Key]
        public int DistrictId { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]

        public City City { get; set; }

        public string Name { get; set; }

        public List<Ward> Wards { get; set; }

        [NotMapped]
        public string FullName { 
            get
            {
                if (City == null) return Name;

                return (Name + " - "+ City.Name);
            }
        }

    }
}
