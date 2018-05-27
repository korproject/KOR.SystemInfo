using System;

namespace KOR.SystemInfo.Models
{
	public class Memory
	{
		public string Name { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public UInt32 Speed { get; set; }
		public UInt64 Size { get; set; }
	}
}
