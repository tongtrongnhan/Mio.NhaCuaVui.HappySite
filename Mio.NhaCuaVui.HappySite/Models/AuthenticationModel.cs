using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class AuthenticationModel
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public Guid UserGuid { get; set; }

        public List<string> UserRoles { get;set;}
    }
}
