using KOR.SystemInfo.Helpers;
using KOR.SystemInfo.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;

namespace KOR.SystemInfo.OEM
{
	public class OEM
	{
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        /// <summary>
        /// Get general oem info
        /// </summary>
        /// <returns></returns>
        public static OEMInfo GetOEMInfo()
        {
            OEMInfo oemInfo = new OEMInfo
            {

                // get baseboard info
                BaseBoard = GetBaseBoardInfo(),
                // get cpu info
                CPU = GetCPUInfo(),
                // get gpu info
                GPU = GetGPUInfo(),
                // get ard disk info
                HardDrive = GetPrimaryHardDiskInfo(),
                // get memory info
                Memory = GetMemoryInfo(),
                // get monitor info
                Monitor = GetMonitorInfo(),

                // get mac address
                MacAddress = GetMacAddress()
            };

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
							cpu.Family = (ushort)properties.Value;
							break;
						case "MaxClockSpeed":
							cpu.MaxClockSpeed = properties.Value is null ? 0 : (uint)properties.Value;
							break;
						case "NumberOfCores":
							cpu.Cores = properties.Value is null ? 0 : (uint)properties.Value;
							break;
						case "NumberOfEnabledCore":
							cpu.EnabledCores = properties.Value is null ? 0 : (uint)properties.Value;
							break;
						case "NumberOfLogicalProcessors":
							cpu.LogicalCores = properties.Value is null ? 0 : (uint)properties.Value;
							break;
						case "Architecture":
							switch ((ushort)properties.Value)
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
							switch ((ushort)properties.Value)
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
			string MACAddress = string.Empty;

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
							hardDrive.Size = (ulong)properties.Value;
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
							memory.Speed = properties.Value is null ? 0 : (uint)properties.Value;
							break;
						case "Capacity":
							memory.Size = properties.Value is null ? 0 : (ulong)properties.Value;
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
							monitor.PixelsPerYLogicalInch = properties.Value is null ? 0 : (uint)properties.Value;
							break;
						case "PixelsPerXLogicalInch":
							monitor.PixelsPerXLogicalInch = properties.Value is null ? 0 : (uint)properties.Value;
							break;
						case "ScreenWidth":
							monitor.ScreenWidth = properties.Value is null ? 0 : (uint)properties.Value;
							break;
						case "ScreenHeight":
							monitor.ScreenHeight = properties.Value is null ? 0 : (uint)properties.Value;
							break;
						default:
							break;
					}
				}

				// dispose managmentObject
				managmentObject.Dispose();
			}

            if (CommonHelpers.IsDllLoaded("gdi32.dll"))
            {
                Graphics graphics = Graphics.FromHwnd(IntPtr.Zero); // using System.Drawing; (add from referances)
                IntPtr desktop = graphics.GetHdc();

                int monitorHeight = GetDeviceCaps(desktop, 6);
                int monitorWidth = GetDeviceCaps(desktop, 4);

                monitor.ScreenHeightMm = monitorHeight;
                monitor.ScreenWidthMm = monitorWidth;
                monitor.ScreenInchSize = Convert.ToDouble($"{Math.Sqrt(Math.Pow(monitorHeight, 2) + Math.Pow(monitorWidth, 2)) / 25,4:#,##0.00}");
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
							gpu.Ram = properties.Value is null ? 0 : (uint)properties.Value;
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

        public static List<Drive> GetDriveInfo()
        {
            List<Drive> drives = new List<Drive>();

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                drives.Add(new Drive()
                {
                    Letter = drive.Name,
                    Type = drive.DriveType,
                    FileSystem = drive.DriveFormat,
                    Size = drive.TotalSize,
                    AvailableSpace = drive.AvailableFreeSpace,
                    AvailableSpacePercent = 100 * (double)drive.TotalFreeSpace / drive.TotalSize,
                    Usage = drive.TotalSize - drive.AvailableFreeSpace,
                    UsagePercent = 100 - (100 * (double)drive.TotalFreeSpace / drive.TotalSize)
                });
            }

            return drives;
        }
	}
}
