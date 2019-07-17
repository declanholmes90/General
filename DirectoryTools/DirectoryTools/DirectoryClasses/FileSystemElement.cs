using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryTools.DirectoryClasses
{
    public abstract class FileSystemElement
    {
        public string Name { get; protected set; }
        public string AbsolutePath { get; protected set; }
        public int DepthFromRoot { get; protected set; }
        public FileSystemElement Parent { get; protected set; }

        public FileSystemElement(string name, string absolutePath, int depthFromRoot)
        {
            Name = name;
            AbsolutePath = absolutePath;
            DepthFromRoot = depthFromRoot;
        }
    }
}
