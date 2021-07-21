using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models.ViewModels
{
    public class BeneficaryProposeViewModel
    {
        // render for display
        public List<BeneficiaryType> AllBeneficiaryType { get; set; }
        public List<Need> AllNeed { get; set; }
        public List<Category> AllCategory { get; set; }

        public List<City> Cities { get; set; }



        // render for receved data
        public List<int> CategoryIds { get; set; }
        public int NeedId { get; set; }
        public int WardId { get; set; }
        public int BenificaryTypeId { get; set; }
        public List<BenificaryCategoryQuantity> BenificaryCategoryQuantities { get; set; }

        public string OrganizationName { get; set; }
        public int? NumberOfPeople { get; set; }

        public string ProposetorName { get; set; }
        public string ProposetorPhone { get; set; }

        public string Street { get; set; }

        public string ProposetorEmail { get; set; }

        public bool? HadTransportation { get; set; }



        public string Note { get; set; }

    }
}
