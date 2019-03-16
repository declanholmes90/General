using System.Collections.ObjectModel;

namespace DirectoryTools.Model
{
    public class LogicalDriveElement : FileSystemElement, IHasChildren
    {

        private ObservableCollection<FileSystemElement> children = new ObservableCollection<FileSystemElement>();

        public LogicalDriveElement(string name, string absolutePath, int depthFromRoot)
            :base(name, absolutePath, depthFromRoot)
        {

        }

        public void AddChild(FileSystemElement child)
        {
            children.Add(child);
        }

        public ObservableCollection<FileSystemElement> GetChildren()
        {
            return children;
        }
    }
}
