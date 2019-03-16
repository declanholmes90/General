using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryTools.Model
{
    public class FileElement : FileSystemElement
    {
        public FileElement(string name, string absolutePath, int depthFromRoot)
            : base(name, absolutePath, depthFromRoot)
        {

        }
    }
}
