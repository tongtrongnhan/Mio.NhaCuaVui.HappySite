using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mio.NhaCuaVui.HappySite.Models.Configurations
{
    public class DonatorCategoryQuatityConfig : IEntityTypeConfiguration<DonationCategoryQuantity>
    {
        public void Configure(EntityTypeBuilder<DonationCategoryQuantity> modelBuilder)
        {
            modelBuilder.ToTable("DonationCategoryQuantity")
            .HasKey(sc => new { sc.CategoryId, sc.DonatorOrganizationId });

            modelBuilder
                .HasOne<DonatorOrganization>(sc => sc.DonatorOrganization)
                .WithMany(s => s.DonationCategoryQuantities)
                .HasForeignKey(sc => sc.DonatorOrganizationId);

            modelBuilder
                .HasOne<Category>(sc => sc.Category)
                .WithMany(s => s.DonationCategoryQuantities)
                .HasForeignKey(sc => sc.CategoryId);

        }
    }
}
