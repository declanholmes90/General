using DirectoryTools.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DirectoryToolsExamples.Model;
using DirectoryTools.Model.EventArguments;
using System;

namespace DirectoryToolsExamples.ViewModel
{
    public class XplorerViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<FileSystemTreeNode> _treeViewNodes = new ObservableCollection<FileSystemTreeNode>();

        public ObservableCollection<FileSystemTreeNode> TreeViewNodes
        {
            get
            {
                return _treeViewNodes;
            }
        }

        DirectoryManager directoryManager = new DirectoryManager();

        public string testText
        {
            get
            {
                return _testText;
            }

            set
            {
                _testText = value;

                OnPropertyChanged(nameof(testText));
            }
        }
        private string _testText;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public XplorerViewModel()
        {
            directoryManager.FileSystemChangedEventHandler += FileSystemChangedHandler;

            UpdateRoots(directoryManager.Directories);

            foreach (FileSystemTreeNode t in TreeViewNodes)
            {
                CreateTreeNodes(t);
            }
        }

        private void UpdateRoots(List<FileSystemElement> roots)
        {
            TreeViewNodes.Clear();

            foreach (FileSystemElement drive in roots)
            {
                TreeViewNodes.Add(new FileSystemTreeNode(drive));
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

        private void FileSystemChangedHandler(FileSystemModifiedEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                UpdateRoots(directoryManager.Directories);

                foreach (FileSystemTreeNode t in _treeViewNodes)
                {
                    CreateTreeNodes(t);
                }
            });

            testText = "Updated " + DateTime.UtcNow;
        }
    }
}
