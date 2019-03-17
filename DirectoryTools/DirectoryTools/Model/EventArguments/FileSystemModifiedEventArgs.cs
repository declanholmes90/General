using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryTools.Model.EventArguments
{
    public class FileSystemModifiedEventArgs : EventArgs
    {
        public DirectoryElement UpdatedDirectory { get; }

        public FileSystemModifiedEventArgs(DirectoryElement updatedDirectory)
        {
            UpdatedDirectory = updatedDirectory;
        }
    }
}
