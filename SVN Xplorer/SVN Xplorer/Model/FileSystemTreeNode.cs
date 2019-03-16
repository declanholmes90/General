using System.Windows.Forms;

namespace DirectoryTools.Model
{
    public class FileSystemTreeNode : TreeNode
    {
        public FileSystemElement Element { get; }

        public FileSystemTreeNode(FileSystemElement element)
            : base(element.Name)
        {
            Element = element;
        }
    }
}
