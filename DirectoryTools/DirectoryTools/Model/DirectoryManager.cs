using DirectoryTools.Model.EventArguments;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Permissions;

namespace DirectoryTools.Model
{
    public class DirectoryManager
    {
        private readonly int MAX_DEPTH = 10;
        private int currentDepthLevel = 0;

        private readonly Dictionary<string, FileSystemElement> directoryQuickAccessDictionary = new Dictionary<string, FileSystemElement>();
        private readonly List<FileSystemElement> directoryTreeCollection = new List<FileSystemElement>();

        public DirectoryManager()
        {
            foreach (string s in Directory.GetLogicalDrives())
            {
                FileSystemElement drive = new LogicalDriveElement(s, s, currentDepthLevel);
                List<FileSystemElement> directories = new List<FileSystemElement>();

                directoryTreeCollection.Add(drive);
                CreateDirectoryWatcher(s);

                PopulateDirectoryTree(drive);
            }
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private bool PopulateDirectoryTree(FileSystemElement node)
        {
            if(directoryQuickAccessDictionary.ContainsKey(node.AbsolutePath))
            {
                directoryQuickAccessDictionary.Remove(node.AbsolutePath);
            }

            directoryQuickAccessDictionary.Add(node.AbsolutePath, node);

            currentDepthLevel++;

            IHasChildren nodeAsDirectory = node as IHasChildren;

            if (nodeAsDirectory == null)
            {
                currentDepthLevel--;
                return false;
            }

            foreach (string s in Directory.GetDirectories(node.AbsolutePath))
            {
                DirectoryElement directoryElement = new DirectoryElement(Path.GetFileName(s), s, currentDepthLevel);

                nodeAsDirectory.AddChild(directoryElement);

                if (currentDepthLevel < MAX_DEPTH)
                {
                    try
                    {
                        PopulateDirectoryTree(directoryElement);
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            foreach (string s in Directory.GetFiles(node.AbsolutePath))
            {
                nodeAsDirectory.AddChild(new FileElement(Path.GetFileName(s), s, currentDepthLevel));
            }

            currentDepthLevel--;
            return true;
        }

        public List<FileSystemElement> Directories
        {
            get { return directoryTreeCollection; }
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void CreateDirectoryWatcher(string path)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = path;

            watcher.NotifyFilter = NotifyFilters.LastAccess
                                    | NotifyFilters.LastWrite
                                    | NotifyFilters.FileName
                                    | NotifyFilters.DirectoryName;

            watcher.Changed += OnFileSystemElementChange;
            watcher.Deleted += OnFileSystemElementChange;
            watcher.Renamed += OnFileSystemElementChange;

            watcher.EnableRaisingEvents = true;
        }

        private void OnFileSystemElementChange(object o, FileSystemEventArgs e)
        {
            string parentString = Directory.GetParent(e.FullPath).ToString();
            IHasChildren parentObject;

            try
            {
                parentObject = (IHasChildren)directoryQuickAccessDictionary[parentString];
                parentObject.RemoveAllChilden();

                currentDepthLevel = ((FileSystemElement)parentObject).DepthFromRoot;

                PopulateDirectoryTree((FileSystemElement) parentObject);

                FileSystemChangedEventHandler?.Invoke(new FileSystemModifiedEventArgs(directoryTreeCollection));
            }
            catch (Exception ex)
            {

            }
        }

        private bool NodeHasChildren(FileSystemElement element)
        {
            IHasChildren nodeWithChildren = (IHasChildren)element;

            return nodeWithChildren != null;
        }

        public delegate void FileSystemChangedEvent(FileSystemModifiedEventArgs e);
        public FileSystemChangedEvent FileSystemChangedEventHandler { get; set; }
    }
}
