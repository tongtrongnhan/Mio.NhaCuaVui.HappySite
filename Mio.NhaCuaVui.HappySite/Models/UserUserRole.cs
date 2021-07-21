using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class UserUserRole
    {
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public User User { get; set; }
        public UserRole UserRole { get; set; }

    }
}
