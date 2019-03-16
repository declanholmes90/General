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

        private readonly Dictionary<string, FileSystemElement> DirectoryQuickAccessDictionary = new Dictionary<string, FileSystemElement>();

        private readonly ObservableCollection<FileSystemElement> directoryTreeCollection = new ObservableCollection<FileSystemElement>();

        public DirectoryManager()
        {
            foreach (string s in Directory.GetLogicalDrives())
            {
                FileSystemElement drive = new LogicalDriveElement(s, s, currentDepthLevel);
                ObservableCollection<FileSystemElement> directories = new ObservableCollection<FileSystemElement>();

                directoryTreeCollection.Add(drive);
                CreateDirectoryWatcher(s);

                PopulateDirectoryTree(drive);
            }
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private bool PopulateDirectoryTree(FileSystemElement node)
        {
            DirectoryQuickAccessDictionary.Add(node.AbsolutePath, node);

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

        public ObservableCollection<FileSystemElement> Directories
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

            watcher.Changed += OnChangedHandler;
            watcher.Created += OnChangedHandler;
            watcher.Deleted += OnChangedHandler;
            watcher.Renamed += OnChangedHandler;

            watcher.EnableRaisingEvents = true;

            directoryModifiedEvent?.Invoke(new DirectoryModifiedEventArgs(directoryTreeCollection));
        }

        private void OnChangedHandler(object o, FileSystemEventArgs e)
        {
            directoryModifiedEvent.Invoke(new DirectoryModifiedEventArgs(directoryTreeCollection));
        }

        public delegate void DirectoryModifiedEvent(DirectoryModifiedEventArgs e);
        public DirectoryModifiedEvent directoryModifiedEvent { get; }
    }
}
