using System;

namespace KOR.SystemInfo.Models
{
	public class HardDrive
	{
		public string HardDiskId { get; set; }
		public string Name { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public UInt64 Size { get; set; }
	}
}
