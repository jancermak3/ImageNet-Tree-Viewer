using ImageNet.Core.Entities;
using ImageNet.Core.Interfaces;

namespace ImageNet.Core.Services
{
    public class TreeTransformator : ITreeTransformator
    {
        public TreeNode TransformToTree(IEnumerable<ImageNetItem> data)
        {
            var dataList = data.ToList();

            if (dataList.Count == 0)
            {
                throw new ArgumentException("Data collection is empty. Cannot build a tree.");
            }

            // Initialize the root node with the first item in the list
            var firstItem = dataList.First();
            var root = new TreeNode
            {
                Name = firstItem.Name,
                Size = firstItem.Size
            };            

            // Process the remaining items
            foreach (var imageNetItem in dataList.Skip(1))
            {
                var parts = imageNetItem.Name.Split('>').Select(p => p.Trim()).ToList();

                // Skip the first part if it's the same as the root name
                if (parts.First().Equals(root.Name, StringComparison.OrdinalIgnoreCase))
                {
                    parts = parts.Skip(1).ToList();
                }

                if (parts.Count > 0)
                {
                    InsertNode(root, parts, imageNetItem.Size, 0);
                }
            }

            return root;
        }

        private void InsertNode(TreeNode currentNode, List<string> parts, int size, int depth)
        {
            if (depth >= parts.Count)
            {
                // Reached the intended depth, no further action needed
                return;
            }

            var childName = parts[depth];
            var child = currentNode.Children
                                   .FirstOrDefault(c => c.Name.Equals(childName, StringComparison.OrdinalIgnoreCase));

            if (child == null)
            {
                child = new TreeNode { Name = childName, Size = size };
                currentNode.Children.Add(child);
            }

            InsertNode(child, parts, size, depth + 1);
        }
    }
}