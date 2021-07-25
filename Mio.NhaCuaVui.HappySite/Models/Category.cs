using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Unit { get; set; }

        public int NeedId { get; set; }
        [ForeignKey("NeedId")]
        public Need Need { get; set; }


        public List<DonationCategoryQuantity> DonationCategoryQuantities { get; set; }
        public List<BenificaryCategoryQuantity> BenificaryCategoryQuantities { get; set; }

        public List<DeliveryCategory> DeliveryCategories { get; set; }



    }
}
