using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mio.NhaCuaVui.HappySite.Models.Configurations
{
    public class DeliveryCategoryQuatityConfig : IEntityTypeConfiguration<DeliveryCategory>
    {
        public void Configure(EntityTypeBuilder<DeliveryCategory> modelBuilder)
        {
            modelBuilder.ToTable("DeliveryCategory")
            .HasKey(sc => new { sc.CategoryId, sc.DeliveryId });

            modelBuilder
                .HasOne<Delivery>(sc => sc.Delivery)
                .WithMany(s => s.DeliveryCategories)
                .HasForeignKey(sc => sc.DeliveryId);

            modelBuilder
                .HasOne<Category>(sc => sc.Category)
                .WithMany(s => s.DeliveryCategories)
                .HasForeignKey(sc => sc.CategoryId);

        }
    }
}
