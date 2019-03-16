using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVN_Xplorer.Model
{
    public interface IHasChildren
    {
        void AddChild(FileSystemElement node);

        ObservableCollection<FileSystemElement> GetChildren();
    }
}
