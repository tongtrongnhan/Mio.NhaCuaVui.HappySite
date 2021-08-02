using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class Beneficiary
    {

        [Key]
        public int BeneficiaryId { get; set; }

        public int BeneficiaryTypeId { get; set; }
        [ForeignKey("BeneficiaryTypeId")]
        public BeneficiaryType BeneficiaryType { get; set; }
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
        public int NumberOfPeople { get; set; }

        public bool? HadTransportation { get; set; }


        public string Street { get; set; }

        public string Note { get; set; }

        public string ContactName { get; set; }
        public string ContactPhone { get; set; }

        public string ContactEmail { get; set; }



        public List<BenificaryCategoryQuantity> BenificaryCategoryQuantities { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ValidatedAt { get; set; }


        public bool? IsValidated { get; set; }
        public int? ValidatedUserId { get; set; }
        [ForeignKey("ValidatedUserId")]
        public User ValidatedUser { get; set; }

        public string NotValidateMessage { get; set; }

        public string ImageUrls { get; set; }

        public List<Delivery> Deliveries { get; set; }

        public List<Delivery> GetSuccessDeliveries()
        {
            if (Deliveries == null || Deliveries.Any() == false) return new List<Delivery>();
            return Deliveries.Where(x => x.IsValidated == true).ToList();
        }
        public string OrganizationDisplay()
        {
            if (!string.IsNullOrEmpty(OrganizationName)) return OrganizationName;
            if (!string.IsNullOrEmpty(ContactName)) return ContactName;
            return ProposetorName;
        }

        [NotMapped]
        public string OrganizationDisplayName
        {
            get
            {
                return OrganizationDisplay();
            }
        }

        public string GetNumberOfDelivery(int CategoryId)
        {
            if (Deliveries == null || Deliveries.Any() == false) return string.Empty;

            var categories = Deliveries.Where(x => x.DeliveryCategories.Any(c => c.CategoryId == CategoryId)).ToList();

            if (categories == null || categories.Any() == false) return string.Empty;

            var total = categories.SelectMany(x => x.DeliveryCategories).Where(x => x.CategoryId == CategoryId).Sum(x => x.Quantity);

            return "Đã nhận: " + total;


        }


        public List<string> GetDeliveryOrganization(int CategoryId)
        {
            if (Deliveries == null || Deliveries.Any() == false) return new List<string>();

            var delieveries = Deliveries.Where(x => x.DeliveryCategories.Any(c => c.CategoryId == CategoryId)).ToList();

            if (delieveries == null || delieveries.Any() == false) return new List<string>();

            

            var result = new List<string>();
            foreach(var item in delieveries)
            {
                if (item.DeliveryCategories == null || item.DeliveryCategories.Any() == false) continue;
                var category = item.DeliveryCategories.FirstOrDefault(x => x.CategoryId == CategoryId);
                if (category == null) continue;

                string resultformat = "Đã nhận: {0} - Từ: {1} - Lúc: {2}";

                result.Add(string.Format(resultformat, category.Quantity.ToString(), item.DonatorOrganization.OrganizationDisplay(), item.CreatedAt.ToString("dd/MM/yyyy")));

            }

            return result;

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

        public List<string> GetListImage()
        {
            if (ImageUrls == null) return new List<string>();

            var list = ImageUrls.Split(";").ToList().Where(x => x != null && x != string.Empty).ToList();

            if (list == null || !list.Any()) return new List<string>();

            return list;
        }
    }
}
