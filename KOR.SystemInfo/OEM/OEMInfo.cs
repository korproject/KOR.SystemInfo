using KOR.SystemInfo.Models;
using System;
using System.Management;

namespace KOR.SystemInfo.OEM
{
	public class OEM
	{
		/// <summary>
		/// Get general oem info
		/// </summary>
		/// <returns></returns>
		public static OEMInfo GetOEMInfo()
		{
			OEMInfo oemInfo = new OEMInfo();

			// get baseboard info
			oemInfo.BaseBoard = GetBaseBoardInfo();
			// get cpu info
			oemInfo.CPU = GetCPUInfo();
			// get gpu info
			oemInfo.GPU = GetGPUInfo();
			// get ard disk info
			oemInfo.HardDrive = GetPrimaryHardDiskInfo();
			// get memory info
			oemInfo.Memory = GetMemoryInfo();
			// get monitor info
			oemInfo.Monitor = GetMonitorInfo();

			// get mac address
			oemInfo.MacAddress = GetMacAddress();

			return oemInfo;
		}

		/// <summary>
		/// Getting BaseBoard Info
		/// </summary>
		/// https://msdn.microsoft.com/en-us/library/aa394072(v=vs.85).aspx
		/// <returns></returns>
		public static BaseBoard GetBaseBoardInfo()
		{
			BaseBoard baseBoard = new BaseBoard();

			// select Win32_BaseBoard from Management Object Searcher
			ManagementObjectSearcher managmentSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
			
			// foreach as Managment Object
			foreach (ManagementObject managmentObject in managmentSearcher.Get())
			{
				// get properties
				foreach (PropertyData properties in managmentObject.Properties)
				{
					switch (properties.Name)
					{
						case "SerialNumber":
							baseBoard.BaseBoardId = (string)properties.Value;
							break;
						case "Name":
							baseBoard.Name = (string)properties.Value;
							break;
						case "Model":
							baseBoard.Model = (string)properties.Value;
							break;
						case "Manufacturer":
							baseBoard.Manufacturer = (string)properties.Value;
							break;
						default:
							break;
					}
				}

				// dispose managmentObject
				managmentObject.Dispose();
			}

			return baseBoard;
		}

		/// <summary>
		/// Getting CPU Info
		/// </summary>
		/// https://msdn.microsoft.com/en-us/library/aa394373%28v=vs.85%29.aspx
		/// <returns></returns>
		public static CPU GetCPUInfo()
		{
			CPU cpu = new CPU();

			// select Win32_Processor from Management Object Searcher
			ManagementObjectSearcher managmentSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

			// foreach as Managment Object
			foreach (ManagementObject managmentObject in managmentSearcher.Get())
			{
				// get properties
				foreach (PropertyData properties in managmentObject.Properties)
				{
					switch (properties.Name)
					{
						case "ProcessorId":
							cpu.CPUId = (string)properties.Value;
							break;
						case "Name":
							cpu.Name = (string)properties.Value;
							break;
						case "Manufacturer":
							cpu.Manufacturer = (string)properties.Value;
							break;
						case "Family":
							cpu.Family = (UInt16)properties.Value;
							break;
						case "MaxClockSpeed":
							cpu.MaxClockSpeed = (UInt32)properties.Value;
							break;
						case "NumberOfCores":
							cpu.Cores = (UInt32)properties.Value;
							break;
						case "NumberOfEnabledCore":
							cpu.EnabledCores = (UInt32)properties.Value;
							break;
						case "NumberOfLogicalProcessors":
							cpu.LogicalCores = (UInt32)properties.Value;
							break;
						case "Architecture":
							switch ((UInt16)properties.Value)
							{
								case 0: cpu.Architecture = "32";
									break;
								case 9:
									cpu.Architecture = "64";
									break;
								case 1:
									cpu.Architecture = "MIPS";
									break;
								case 2:
									cpu.Architecture = "Alpha";
									break;
								case 3:
									cpu.Architecture = "PowerPC";
									break;
								case 5:
									cpu.Architecture = "ARM";
									break;
								case 6:
									cpu.Architecture = "ia64";
									break;
								default:
									break;
							}
							break;
						case "ProcessorType":
							switch ((UInt16)properties.Value)
							{
								case 0:
									cpu.ProcessorType = "Other";
									break;
								case 9:
									cpu.ProcessorType = "Unknown";
									break;
								case 1:
									cpu.ProcessorType = "Central Processor";
									break;
								case 2:
									cpu.ProcessorType = "Math Processor";
									break;
								case 3:
									cpu.ProcessorType = "DSP Processor";
									break;
								case 5:
									cpu.ProcessorType = "Video Processor";
									break;
								default:
									break;
							}
							break;
						default:
							break;
					}
				}

				// dispose managmentObject
				managmentObject.Dispose();
			}

			return cpu;
		}

		/// <summary>
		/// Getting Mac Adress
		/// </summary>
		/// <returns></returns>
		public static string GetMacAddress()
		{
			string MACAddress = String.Empty;

			// select Win32_NetworkAdapterConfiguration from Management Object Searcher
			ManagementObjectSearcher managmentSearcher = new ManagementObjectSearcher("Select * FROM Win32_NetworkAdapterConfiguration");

			// foreach as Managment Object
			foreach (ManagementObject managmentObject in managmentSearcher.Get())
			{
				// if mac address not null
				if (managmentObject["MacAddress"] != null)
				{
					// get mac address
					MACAddress = managmentObject["MacAddress"].ToString();
					break;
				}

				// dispose managmentObject
				managmentObject.Dispose();
			}

			return MACAddress;
		}

		/// <summary>
		/// Getting Primary Hard Disk Info
		/// </summary>
		/// <returns></returns>
		public static HardDrive GetPrimaryHardDiskInfo()
		{
			HardDrive hardDrive = new HardDrive();

			// select Win32_DiskDrive from Management Object Searcher
			ManagementObjectSearcher managmentSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

			// foreach as Managment Object
			foreach (ManagementObject managmentObject in managmentSearcher.Get())
			{
				// get properties
				foreach (PropertyData properties in managmentObject.Properties)
				{
					switch (properties.Name)
					{
						case "SerialNumber" :
							hardDrive.HardDiskId = (string)properties.Value;
							break;
						case "Manufacturer":
							hardDrive.Manufacturer = (string)properties.Value;
							break;
						case "Name":
							hardDrive.Name = (string)properties.Value;
							break;
						case "Model":
							hardDrive.Model = (string)properties.Value;
							break;
						case "Size":
							hardDrive.Size = (UInt64)properties.Value;
							break;
						default:
							break;
					}
				}

				// dispose managmentObject
				managmentObject.Dispose();
			}

			return hardDrive;
		}

		/// <summary>
		/// Getting Physical Memory Info
		/// </summary>
		/// <returns></returns>
		public static Memory GetMemoryInfo()
		{
			Memory memory = new Memory();

			// select Win32_DiskDrive from Management Object Searcher
			ManagementObjectSearcher managmentSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");

			// foreach as Managment Object
			foreach (ManagementObject managmentObject in managmentSearcher.Get())
			{
				// get properties
				foreach (PropertyData properties in managmentObject.Properties)
				{
					switch (properties.Name)
					{
						case "Manufacturer":
							memory.Manufacturer = (string)properties.Value;
							break;
						case "Name":
							memory.Name = (string)properties.Value;
							break;
						case "Model":
							memory.Model = (string)properties.Value;
							break;
						case "Speed":
							memory.Speed = (UInt32)properties.Value;
							break;
						case "Capacity":
							memory.Size = (UInt64)properties.Value;
							break;
						default:
							break;
					}
				}

				// dispose managmentObject
				managmentObject.Dispose();
			}


			return memory;
		}

		/// <summary>
		/// Getting Monitor Info
		/// </summary>
		/// <returns></returns>
		public static Monitor GetMonitorInfo()
		{
			Monitor monitor = new Monitor();

			// select Win32_DiskDrive from Management Object Searcher
			ManagementObjectSearcher managmentSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DesktopMonitor");

			// foreach as Managment Object
			foreach (ManagementObject managmentObject in managmentSearcher.Get())
			{
				// get properties
				foreach (PropertyData properties in managmentObject.Properties)
				{
					switch (properties.Name)
					{
						case "MonitorManufacturer":
							monitor.MonitorManufacturer = (string)properties.Value;
							break;
						case "Name":
							monitor.Name = (string)properties.Value;
							break;
						case "MonitorType":
							monitor.MonitorType = (string)properties.Value;
							break;
						case "PixelsPerYLogicalInch":
							monitor.PixelsPerYLogicalInch = (UInt32)properties.Value;
							break;
						case "PixelsPerXLogicalInch":
							monitor.PixelsPerXLogicalInch = (UInt32)properties.Value;
							break;
						case "ScreenWidth":
							monitor.ScreenWidth = (UInt32)properties.Value;
							break;
						case "ScreenHeight":
							monitor.ScreenHeight = (UInt32)properties.Value;
							break;
						default:
							break;
					}
				}

				// dispose managmentObject
				managmentObject.Dispose();
			}


			return monitor;
		}

		/// <summary>
		/// Get CPU Info
		/// </summary>
		/// <returns></returns>
		public static GPU GetGPUInfo()
		{
			GPU gpu = new GPU();

			// select Win32_Processor from Management Object Searcher
			ManagementObjectSearcher managmentSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

			// foreach as Managment Object
			foreach (ManagementObject managmentObject in managmentSearcher.Get())
			{
				// get properties
				foreach (PropertyData properties in managmentObject.Properties)
				{
					switch (properties.Name)
					{
						case "Name":
							gpu.Name = (string)properties.Value;
							break;
						case "AdapterDACType":
							gpu.Name = (string)properties.Value;
							break;
						case "AdapterRAM":
							gpu.Ram = (UInt32)properties.Value;
							break;
						case "VideoProcessor":
							gpu.VideoProcessor = (string)properties.Value;
							break;
						default:
							break;
					}
				}

				// dispose managmentObject
				managmentObject.Dispose();
			}

			return gpu;
		}
	}
}
