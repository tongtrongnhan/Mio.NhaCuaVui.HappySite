using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mio.NhaCuaVui.HappySite.Models.Configurations;

namespace Mio.NhaCuaVui.HappySite.Models
{
    public class ZDbContext : DbContext
    {
        public ZDbContext()
        {

        }

        public ZDbContext(DbContextOptions<ZDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder
              .ApplyConfiguration(new BenificaryCategoryQuatityConfig());
            builder
              .ApplyConfiguration(new DonatorCategoryQuatityConfig());

            builder
             .ApplyConfiguration(new UserUserRoleConfiguration());

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<District> Districts { get; set; }
        public DbSet<DonationCategoryQuantity> DonationCategoryQuantities { get; set; }
        public DbSet<DonatorOrganization> DonatorOrganizations { get; set; }
        public DbSet<DonatorOrganizationType> DonatorOrganizationTypes { get; set; }

        public DbSet<Need> Needs { get; set; }
        public DbSet<Ward> Wards { get; set; }

        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<BeneficiaryType> BeneficiaryTypes { get; set; }
        public DbSet<BenificaryCategoryQuantity> BenificaryCategoryQuantities { get; set; }


        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserUserRole> UserUserRole { get; set; }



    }
}
