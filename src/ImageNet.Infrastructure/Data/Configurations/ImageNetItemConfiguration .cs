using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ImageNet.Core.Entities;

namespace ImageNet.Infrastructure.Data.Configurations
{
    public class ImageNetItemConfiguration : IEntityTypeConfiguration<ImageNetItem>
    {
        public void Configure(EntityTypeBuilder<ImageNetItem> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.Name).IsRequired().HasMaxLength(500);
            builder.Property(i => i.Size).IsRequired();
        }
    }
}