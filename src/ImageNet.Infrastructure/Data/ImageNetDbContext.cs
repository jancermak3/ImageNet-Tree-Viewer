using Microsoft.EntityFrameworkCore;
using ImageNet.Core.Entities;

namespace ImageNet.Infrastructure.Data
{
    public class ImageNetDbContext : DbContext
    {
        public ImageNetDbContext(DbContextOptions<ImageNetDbContext> options) : base(options) {}

        public DbSet<ImageNetItem> ImageNetItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ImageNetDbContext).Assembly);
        }
    }
}