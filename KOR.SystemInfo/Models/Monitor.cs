using System;

namespace KOR.SystemInfo.Models
{
	public class Monitor
	{
		public string MonitorManufacturer { get; set; }
		public string Name { get; set; }
		public string MonitorType { get; set; }
		public UInt32 PixelsPerYLogicalInch { get; set; }
		public UInt32 PixelsPerXLogicalInch { get; set; }
		public UInt32 ScreenWidth { get; set; }
		public UInt32 ScreenHeight { get; set; }
	}
}
