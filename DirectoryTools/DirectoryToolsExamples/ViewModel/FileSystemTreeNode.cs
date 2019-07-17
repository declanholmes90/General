using DirectoryTools.DirectoryClasses;
using System.Windows.Forms;

namespace DirectoryToolsExamples.Model
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
