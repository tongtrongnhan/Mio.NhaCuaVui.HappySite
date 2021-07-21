using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class Ward
    {
        [Key]
        public int WardId { get; set; }
        public string Name { get; set; }

        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public District District { get; set; }

        
    }
}
