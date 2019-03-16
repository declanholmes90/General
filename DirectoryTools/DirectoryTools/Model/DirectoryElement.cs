using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryTools.Model
{
    public class DirectoryElement : FileSystemElement, IHasChildren
    {
        private ObservableCollection<FileSystemElement> children = new ObservableCollection<FileSystemElement>();

        public DirectoryElement(string name, string absolutePath, int depthFromRoot)
            : base(name, absolutePath, depthFromRoot)
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
