using ImageNet.Core.Entities;
using ImageNet.Core.Interfaces;
using ImageNet.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace ImageNet.Infrastructure.Repositories
{
    public class ImageNetItemRepository : IImageNetItemRepository
    {
        private readonly ImageNetDbContext _context;
        private readonly ILogger<ImageNetItemRepository> _logger;

        public ImageNetItemRepository(ImageNetDbContext context, ILogger<ImageNetItemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<ImageNetItem> GetAll()
        {
            return _context.ImageNetItems.ToList();
        }

        public void SaveAll(IEnumerable<ImageNetItem> items)
        {
            const int batchSize = 1000;
            var itemsList = items.ToList();
            var totalItems = itemsList.Count;

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                for (int i = 0; i < totalItems; i += batchSize)
                {
                    var batch = itemsList.Skip(i).Take(batchSize).ToList();
                    _context.ImageNetItems.AddRange(batch);
                    _context.SaveChanges();
                    _logger.LogInformation("Saved {count} items to database", batch.Count);
                }

                transaction.Commit();
                _logger.LogInformation("All {count} items saved successfully", totalItems);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "Error occurred while saving items to database");
                throw;
            }
        }
    }
}