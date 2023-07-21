using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlyFiles.DiskDrive
{
    public class DriveServices : IDriveService
    {
        public List<string> GetReadyDrives()
        {
            List<string> readyDrives = new List<string>();

            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    readyDrives.Add(drive.Name);
                }
            }

            return readyDrives;
        }
    }
}
