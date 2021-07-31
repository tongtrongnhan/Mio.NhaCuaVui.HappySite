using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models.ViewModels
{
    public class DelieveryReportViewModel
    {
        public int BenificaryId { get; set; }
        public int CategoryId { get; set; }
        public int TotalDelivery { get; set; }

        public List<DelieveryReportOrganizationViewModel> DeliveryInfors { get; set; }
    }

    public class DelieveryReportOrganizationViewModel
    {
        public int BenificaryId { get; set; }
        public int CategoryId { get; set; }
        public int TotalDelivery { get; set; }
        public int DeliveryId { get; set; }
        public string OrganizationName { get; set; }
        public DateTime DeliveryAt { get; set; }
    }


    public class DelieveryReportPostViewModel
    {
        public int BenificaryId { get; set; }
        public int CategoryId { get; set; }
    }
}
