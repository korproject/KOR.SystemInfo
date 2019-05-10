using System;

namespace KOR.SystemInfo.Models
{
	public class CPU
	{
		public string CPUId { get; set; }
		public string Name { get; set; }
		public string Manufacturer { get; set; }
		public ushort Family { get; set; }
		public uint MaxClockSpeed { get; set; }
		public uint Cores { get; set; }
		public uint EnabledCores { get; set; }
		public uint LogicalCores { get; set; }
		public string ProcessorType { get; set; }
		public string Architecture { get; set; }
	}
}
