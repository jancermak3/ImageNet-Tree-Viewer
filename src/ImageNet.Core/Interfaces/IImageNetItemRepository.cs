using ImageNet.Core.Entities;

namespace ImageNet.Core.Interfaces
{
    public interface IImageNetItemRepository
    {
        IEnumerable<ImageNetItem> GetAll();
        void SaveAll(IEnumerable<ImageNetItem> items);
    }
}