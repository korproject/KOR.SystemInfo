using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KOR.SystemInfo.Models
{
    public class Bios
    {
        public string Version { get; set; }
        public string BuildNumber { get; set; }
        public string Caption { get; set; }
        public string CodeSet { get; set; }
        public string CurrentLanguage { get; set; }
        public string Description { get; set; }
        public string InstallDate { get; set; }
        public string Manufacturer { get; set; }
    }
}
