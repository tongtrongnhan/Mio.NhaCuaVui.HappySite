﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        public Guid UserGuid { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public int? MyDonatorOrganizationId { get; set; }
        [ForeignKey("MyDonatorOrganizationId")]

        public DonatorOrganization MyDonatorOrganization { get; set; }

        public ICollection<UserUserRole> UserUserRoles { get; set; }

        public string GetMyOrganizationName()
        {
            if(MyDonatorOrganization == null)
            {
                return "Chưa xác định";
            }
            return MyDonatorOrganization.OrganizationDisplay();

        }

    }
}
