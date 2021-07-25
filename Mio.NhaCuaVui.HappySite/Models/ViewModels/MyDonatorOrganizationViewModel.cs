using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models.ViewModels
{
    public class MyDonatorOrganizationViewModel
    {
        public bool HadOrganization { get; set; }
        public DonatorOrganization Donator { get; set; }

        public List<Delivery> PendingForDeliveries { get; set; }

        public List<Delivery> Deliveries { get; set; }

    }
}
