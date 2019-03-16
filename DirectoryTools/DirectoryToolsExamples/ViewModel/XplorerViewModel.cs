using DirectoryTools.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using DirectoryToolsExamples.Model;

namespace DirectoryToolsExamples.ViewModel
{
    public class XplorerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FileSystemTreeNode> TreeViewRoots { get; } = new ObservableCollection<FileSystemTreeNode>();

        DirectoryManager directoryManager = new DirectoryManager();

        public event PropertyChangedEventHandler PropertyChanged;

        public XplorerViewModel()
        {
            //Directories = directoryManager.Directories;

            foreach (FileSystemElement drive in directoryManager.Directories)
            {
                TreeViewRoots.Add(new FileSystemTreeNode(drive));
            }

            foreach (FileSystemTreeNode t in TreeViewRoots)
            {
                CreateTreeNodes(t);
            }
        }

        private void CreateTreeNodes(FileSystemTreeNode node)
        {
            IHasChildren nodeWithChildren = null;

            if (node.Element.GetType() == typeof(LogicalDriveElement))
            {
                nodeWithChildren = (LogicalDriveElement)node.Element;
            }
            else if (node.Element.GetType() == typeof(DirectoryElement))
            {
                nodeWithChildren = (DirectoryElement)node.Element;
            }
            else
            {
                return;
            }

            foreach (FileSystemElement e in nodeWithChildren.GetChildren())
            {
                FileSystemTreeNode childNode = new FileSystemTreeNode(e);

                node.Nodes.Add(childNode);

                CreateTreeNodes(childNode);
            }
        }
    }
}
