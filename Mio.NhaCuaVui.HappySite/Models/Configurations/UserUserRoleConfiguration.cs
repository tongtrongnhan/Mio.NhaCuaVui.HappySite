using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mio.NhaCuaVui.HappySite.Models.Configurations
{
    public class UserUserRoleConfiguration : IEntityTypeConfiguration<UserUserRole>
    {
        public void Configure(EntityTypeBuilder<UserUserRole> modelBuilder)
        {
            modelBuilder.ToTable("UserUserRole")
                .HasKey(sc => new { sc.UserId, sc.UserRoleId });

            modelBuilder
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.UserUserRoles)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder
                .HasOne<UserRole>(sc => sc.UserRole)
                .WithMany(s => s.UserUserRoles)
                .HasForeignKey(sc => sc.UserRoleId);
        }
    }
}
