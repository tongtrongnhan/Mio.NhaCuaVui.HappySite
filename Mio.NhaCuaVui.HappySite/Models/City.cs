using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }

        public List<District> Districts { get; set; }

    }
}
