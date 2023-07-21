
namespace OnlyFiles.Class
{
    public class FileItem
    {
        public string Name { get; set; }
        public long FileSize { get; set; }
        public bool IsFolder { get; set; }
        public FileItem(string name, long size)
        {
            Name = name;
            FileSize = size;
        }
    }
}
