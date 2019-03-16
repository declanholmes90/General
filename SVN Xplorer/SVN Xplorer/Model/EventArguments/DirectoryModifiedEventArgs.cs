using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVN_Xplorer.Model.EventArguments
{
    public class DirectoryModifiedEventArgs : EventArgs
    {
        public ObservableCollection<FileSystemElement> FilesAndFolderLabels { get; }

        public DirectoryModifiedEventArgs(ObservableCollection<FileSystemElement> filetree)
        {
            FilesAndFolderLabels = filetree;
        }
    }
}
