using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DirectoryTools.Model
{
    public interface IHasChildren
    {
        void AddChild(FileSystemElement node);

        void RemoveAllChilden();

        List<FileSystemElement> GetChildren();
    }
}
