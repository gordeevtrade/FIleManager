using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnlyFiles.Class;

namespace OnlyFiles.DiskDirectory
{
    public class DirectoryService : IDirectoryService
    {
       
        public async Task<DirectoryItem> CreateDirectoryItemAsync(DirectoryInfo directoryInfo)
        {
            return await Task.Run(() => CreateDirectoryItem(directoryInfo, true));

        }

        private DirectoryItem CreateDirectoryItem(DirectoryInfo directoryInfo, bool isAsync = false)
        {
            DirectoryItem folder = new DirectoryItem(directoryInfo.Name, directoryInfo.FullName, 0, isAsync);
            long directorySize = 0;
            List<DirectoryItem> subFolders = new List<DirectoryItem>();
            List<FileItem> files = new List<FileItem>();
            object subFoldersLock = new object();
            object filesLock = new object();

            Parallel.ForEach(directoryInfo.GetDirectories().Where(subDirectoryInfo => !IsSymbolicLink(subDirectoryInfo)), subDirectoryInfo =>
            {
                try
                {
                    DirectoryItem underFolder = CreateDirectoryItem(subDirectoryInfo);
                    lock (subFoldersLock)
                    {
                        subFolders.Add(underFolder);
                    }
                    Interlocked.Add(ref directorySize, underFolder.DirectorySize);
                }
                catch (UnauthorizedAccessException)
                {
                }
            });

            Parallel.ForEach(directoryInfo.GetFiles(), fileInfo =>
            {
                try
                {
                    FileItem fileItem = new FileItem(fileInfo.Name, fileInfo.Length);
                    lock (filesLock)
                    {
                        files.Add(fileItem);
                    }
                    Interlocked.Add(ref directorySize, fileInfo.Length);
                }
                catch (UnauthorizedAccessException)
                {
                }
            });

            folder.SubItems = new ObservableCollection<object>(subFolders.Concat<object>(files));
            folder.DirectorySize = directorySize;
            return folder;
        }

        private bool IsSymbolicLink(DirectoryInfo directoryInfo)
        {
            var fileAttributes = directoryInfo.Attributes;
            return (fileAttributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint;
        }
    }
}
