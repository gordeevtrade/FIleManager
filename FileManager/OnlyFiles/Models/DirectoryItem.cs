using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyFiles.Class
{
    public class DirectoryItem
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public long DirectorySize { get; set; }
        public ObservableCollection<object> SubItems { get; set; }
        public bool IsFolder { get; set; }
        public bool IsRoot { get; set; }

        public DirectoryItem(string name, string fullPath, long directorySize, bool isRoot = false)
        {
            Name = name;
            FullPath = fullPath;
            DirectorySize = directorySize;
            SubItems = new ObservableCollection<object>();
            IsRoot = isRoot;

        }
    }
}
