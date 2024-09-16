using ImageNet.Core.Entities;

namespace ImageNet.Core.Interfaces
{
    public interface ITreeTransformator
    {
        TreeNode TransformToTree(IEnumerable<ImageNetItem> data);
    }
}