namespace KOR.SystemInfo.Models
{
	public class OEMInfo
	{
		public BaseBoard BaseBoard { get; set; }
		public CPU CPU { get; set; }
		public GPU GPU { get; set; }
		public HardDrive HardDrive { get; set; }
		public Memory Memory { get; set; }
		public Monitor Monitor { get; set; }
		public string MacAddress { get; set; }
	}
}
