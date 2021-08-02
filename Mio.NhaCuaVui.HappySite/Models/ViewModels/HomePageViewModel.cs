using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models.ViewModels
{
    public class HomePageViewModel
    {
        public int NumberOfPeople { get; set; }
        public int NumberOfDonator { get; set; }

        public List<Delivery> DeliveriesSuccess { get; set; }

        public List<DonationCategoryQuantity> DonatorCategories { get; set; }
        public List<BenificaryCategoryQuantity> BenificaryCategories { get; set; }


    }

}
