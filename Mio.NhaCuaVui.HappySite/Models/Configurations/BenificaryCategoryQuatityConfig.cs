using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mio.NhaCuaVui.HappySite.Models.Configurations
{
    public class BenificaryCategoryQuatityConfig : IEntityTypeConfiguration<BenificaryCategoryQuantity>
    {
        public void Configure(EntityTypeBuilder<BenificaryCategoryQuantity> modelBuilder)
        {
            modelBuilder.ToTable("BenificaryCategoryQuantity")
            .HasKey(sc => new { sc.CategoryId, sc.BenificaryId });

            modelBuilder
                .HasOne<Beneficiary>(sc => sc.Beneficiary)
                .WithMany(s => s.BenificaryCategoryQuantities)
                .HasForeignKey(sc => sc.BenificaryId);

            modelBuilder
                .HasOne<Category>(sc => sc.Category)
                .WithMany(s => s.BenificaryCategoryQuantities)
                .HasForeignKey(sc => sc.CategoryId);

        }
    }
}
