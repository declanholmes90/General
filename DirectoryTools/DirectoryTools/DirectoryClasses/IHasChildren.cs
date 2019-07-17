using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DirectoryTools.DirectoryClasses
{
    public interface IHasChildren
    {
        void AddChild(FileSystemElement node);

        void RemoveAllChilden();

        List<FileSystemElement> GetChildren();
    }
}
