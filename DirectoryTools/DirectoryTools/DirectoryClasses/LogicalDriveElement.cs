using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DirectoryTools.DirectoryClasses
{
    public class LogicalDriveElement : DirectoryElement
    {

        private List<FileSystemElement> children = new List<FileSystemElement>();

        public LogicalDriveElement(string name, string absolutePath, int depthFromRoot)
            :base(name, absolutePath, depthFromRoot)
        {

        }
    }
}
