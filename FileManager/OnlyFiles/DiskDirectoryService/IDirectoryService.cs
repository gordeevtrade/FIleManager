
using System.IO;
using System.Threading.Tasks;
using OnlyFiles.Class;

namespace OnlyFiles.DiskDirectory
{
    public interface IDirectoryService
    {
        Task<DirectoryItem> CreateDirectoryItemAsync(DirectoryInfo directoryInfo);
    }
}
