using System;

namespace KOR.SystemInfo.Models
{
	public class Memory
	{
		public string Name { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public uint Speed { get; set; }
		public ulong Size { get; set; }
        public string SizeGB { get; set; }
    }
}
