using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public ICollection<UserUserRole> UserUserRoles { get; set; }

    }
}
