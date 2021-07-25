using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class DonatorOrganization
    {
        [Key]
        public int DonatorOrganizationId { get; set; }

        public int DonatorOrganizationTypeId { get; set; }
        [ForeignKey("DonatorOrganizationTypeId")]
        public DonatorOrganizationType DonatorOrganizationType { get; set; }
        public string ProposetorName { get; set; }
        public string ProposetorPhone { get; set; }
        public string ProposetorEmail { get; set; }


        public int? NeedId { get; set; }
        [ForeignKey("NeedId")]
        public Need Need { get; set; }

        public int? WardId { get; set; }
        [ForeignKey("WardId")]
        public Ward Ward { get; set; }

        public string OrganizationName { get; set; }


        public string Street { get; set; }

        public string Note { get; set; }

        public string ContactName { get; set; }
        public string ContactPhone { get; set; }

        public string ContactEmail { get; set; }

        public bool? HadTransportation { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime ValidatedAt { get; set; }


        public bool? IsValidated { get; set; }
        public int? ValidatedUserId { get; set; }
        [ForeignKey("ValidatedUserId")]
        public User ValidatedUser { get; set; }
        public string NotValidateMessage { get; set; }


        public string OrganizationDisplay()
        {
            if (!string.IsNullOrEmpty(OrganizationName)) return OrganizationName;
            if (!string.IsNullOrEmpty(ContactName)) return ContactName;
            return ProposetorName;
        }

        [NotMapped]
        public string OrganizationNameDisplay
        {
            get
            {
                return OrganizationDisplay();
            }
        }


        public string GetAddress()
        {
            if (Ward == null) return "Không rõ";
            return Street + ", " + Ward.Name + ", " + (Ward.District == null ? "" : Ward.District.Name) + ", " + (Ward.District == null ? "" : Ward.District.City == null ? "" : Ward.District.City.Name); ;
        }

        public string GetTranspotationInformation()
        {
            if (HadTransportation == null) return "Không rõ có thể nhận hàng được ko";
            if (HadTransportation == true) return "Có phương tiện di chuyển";

            return "Không thể đi nhận hàng";
        }


        public List<DonationCategoryQuantity> DonationCategoryQuantities { get; set; }

    }
}
