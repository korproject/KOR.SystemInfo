using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOR.SystemInfo.Models
{
    public class Drive
    {
        public string Letter { get; set; }
        public DriveType Type { get; set; }
        public string FileSystem { get; set; }
        public long TotalSize { get; set; }
        public long Usage { get; set; }
        public double UsagePercent { get; set; }
        public long AvailableSpace { get; set; }
        public double AvailableSpacePercent { get; set; }
    }
}
