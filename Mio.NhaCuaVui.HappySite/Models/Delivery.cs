using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class Delivery
    {
        [Key]
        public int DeliveryId { get; set; }

        public int DonatorOrganizationId { get; set; }
        [ForeignKey("DonatorOrganizationId")]
        public DonatorOrganization DonatorOrganization { get; set; }

        public int? BeneficiaryId { get; set; }
        [ForeignKey("BeneficiaryId")]

        public Beneficiary Beneficiary { get; set; }
        public string BeneficiaryName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public int UserCreateId { get; set; }
        [ForeignKey("UserCreateId")]
        public User UserCreate { get; set; }

        public List<DeliveryCategory> DeliveryCategories { get; set; }

        [DefaultValue(false)]
        public bool IsDelivery { get; set; }
        public DateTime? DeliveredAt { get; set; }

        [DefaultValue(false)]
        public bool IsValidated { get; set; }
        public DateTime? ValidateddAt { get; set; }
        public int? ValidatedByUserId { get; set; }
        [ForeignKey("ValidatedByUserId")]
        public User ValidatedByUser { get; set; }

        public string GetBeneficaryName()
        {
            if (Beneficiary == null) return BeneficiaryName;
            return Beneficiary.OrganizationDisplayName;
        }

        public string GetBeneficaryAddress()
        {
            if (Beneficiary == null) return "không rõ";
            return Beneficiary.GetAddress();
        }


        public string GetEstimateDeliveryDateText()
        {
            if (DeliveryDate == null) return "không rõ";
            return DeliveryDate.Value.ToString("dd/MM/yyyy");
        }

        public string GetDeliveriedDateText()
        {
            if (DeliveredAt == null) return "không rõ";
            return DeliveredAt.Value.ToString("dd/MM/yyyy");
        }

    }
}
