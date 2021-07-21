using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class Need
    {
        [Key]
        public int NeedId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }

        public List<Category> Categories { get; set; }

    }
}
