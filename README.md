# General

----------
DIRECTORY TOOLS SLN

Overview:
The general purpose of this application is to model a file system.

Components:
- DirectoryManager project
  - Creates the model of the filesystem, registers for changes to the file system.
  - Contains Client/Server code, intended to allow for a remote entities file system to be modeled, and sent to
    connected clients via JSON over socket.
                            
- ClientTest project
  - Light weight project, creates an instance of the client class contained in DirectoryManager project.

- DirectoryToolsExamples    
  - Light weight project, creates an instance of the DirectoryManager, which models the file system on the local                               machine.
  - Registers to the fileSystemChangedEvent exposed by DirectoryManager.cs.
  - Processes the file system model created by the DirectoryManager to a for suitable for display.
                          
To Do:

- Fix onFileSystemChanged handler in DirectoryToolsExamples; currently a file system change wipes the model bar the root elements (C:// etc).

- Implement multithreading to ease performance issues when the file system is initially modeled;
  - The client should receive an initial incomplete files system model (e.g. file depth = 10).
  - Threads should be created to map the rest of the file system, updating the model at intervals (e.g. depth interval = 10).
  
- FileSystemElement depth var is being set to incorrect value after a file system change occurs (e.g. c:// depth is being set to 8 where it should be 1).

- Integrate Client/Server with DirectoryTools and DirectoryToolsExamples projects.

- Persist a file system model on the client (DirectoryToolsExamples), between application instances.
  - Will require a lightweight means of checking whether a file sytem change event has occured on the server side at client start up.

