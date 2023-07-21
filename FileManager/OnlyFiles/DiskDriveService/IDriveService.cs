using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyFiles.DiskDrive
{
    public interface IDriveService
    {
        List<string> GetReadyDrives();
    }
}
