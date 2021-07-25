using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models.ViewModels
{
    public class DeliveryCRUDViewModel
    {
        public int DonatorOrganizationId { get; set; }
        public int? BeneficiaryId { get; set; }
        public Beneficiary Beneficiary { get; set; }
        public string BeneficiaryName { get; set; }
        public DateTime? DeliveryDate { get; set; }


        public List<DeliveryCategory> DeliveryCategories { get; set; }


        public List<Beneficiary> AllActiveBeneficiary { get; set; }
        public List<Category> MyCategories { get; set; }



    }
}
