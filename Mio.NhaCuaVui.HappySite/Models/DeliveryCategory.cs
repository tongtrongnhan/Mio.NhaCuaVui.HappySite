using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class DeliveryCategory
    {

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int DeliveryId { get; set; }

        public Category Category { get; set; }

        public Delivery Delivery { get; set; }

        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
