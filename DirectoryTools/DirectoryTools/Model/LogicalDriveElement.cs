using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DirectoryTools.Model
{
    public class LogicalDriveElement : FileSystemElement, IHasChildren
    {

        private List<FileSystemElement> children = new List<FileSystemElement>();

        public LogicalDriveElement(string name, string absolutePath, int depthFromRoot)
            :base(name, absolutePath, depthFromRoot)
        {

        }

        public void AddChild(FileSystemElement child)
        {
            children.Add(child);
        }

        public List<FileSystemElement> GetChildren()
        {
            return children;
        }

        public void RemoveAllChilden()
        {
            children = new List<FileSystemElement>();
        }
    }
}
