namespace ImageNet.Core.Entities
{
    public class TreeNode
    {
        public string Name { get; set; } = string.Empty;
        public int Size { get; set; }
        public List<TreeNode> Children { get; set; } = new List<TreeNode>();
    }
}