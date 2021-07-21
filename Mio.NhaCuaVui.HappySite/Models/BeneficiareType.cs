using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class BeneficiaryType
    {
        [Key]
        public int BeneficiaryTypeId { get; set; }
        public string Name { get; set; }

        public List<Beneficiary> Beneficiaries { get; set; }
    }
}
