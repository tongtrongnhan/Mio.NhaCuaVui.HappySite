using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class DonatorOrganizationType
    {
        [Key]
        public int DonatorOrganizationTypeId { get; set; }
        public string Name { get; set; }

        public List<DonatorOrganization> DonatorOrganizations { get; set; }
    }
}
