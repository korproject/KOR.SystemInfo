using System;

namespace KOR.SystemInfo.Models
{
	public class CPU
	{
		public string CPUId { get; set; }
		public string Name { get; set; }
		public string Manufacturer { get; set; }
		public UInt16 Family { get; set; }
		public UInt32 MaxClockSpeed { get; set; }
		public UInt32 Cores { get; set; }
		public UInt32 EnabledCores { get; set; }
		public UInt32 LogicalCores { get; set; }
		public string ProcessorType { get; set; }
		public string Architecture { get; set; }
	}
}
