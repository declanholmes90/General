using System.Collections.ObjectModel;

namespace DirectoryTools.Model
{
    public interface IHasChildren
    {
        void AddChild(FileSystemElement node);

        ObservableCollection<FileSystemElement> GetChildren();
    }
}
