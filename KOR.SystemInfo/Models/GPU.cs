using System;

namespace KOR.SystemInfo.Models
{
	public class GPU
	{
		public string Name { get; set; }
		public string AdapterDACType { get; set; }
		public UInt32 Ram { get; set; }
		public string VideoProcessor { get; set; }
	}
}
