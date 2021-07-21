using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class DonationCategoryQuantity
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int DonatorOrganizationId { get; set; }

        public Category Category { get; set; }

        public DonatorOrganization DonatorOrganization { get; set; }

        public int Quantity { get; set; }
        public string Description { get; set; }

    }
}
