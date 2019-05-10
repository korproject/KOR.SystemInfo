using System;

namespace KOR.SystemInfo.Models
{
	public class Monitor
	{
		public string MonitorManufacturer { get; set; }
		public string Name { get; set; }
		public string MonitorType { get; set; }
		public uint PixelsPerYLogicalInch { get; set; }
		public uint PixelsPerXLogicalInch { get; set; }
		public uint ScreenWidth { get; set; }
		public uint ScreenHeight { get; set; }
        public int ScreenWidthMm { get; set; }
        public int ScreenHeightMm { get; set; }
        public double ScreenInchSize { get; set; }
	}
}
