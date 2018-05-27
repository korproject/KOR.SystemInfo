using KOR.SystemInfo.Helpers;
using Microsoft.Win32;
using System;
using System.Text.RegularExpressions;

namespace KOR.SystemInfo.System
{
	public class SystemInfo
	{
		#region OS

		/// <summary>
		/// Get general operating system info
		/// </summary>
		/// <returns></returns>
		public static OperatingSystem GetOSInfo()
		{
			OperatingSystem OS = new OperatingSystem();

			#region Operating System Name-Version

			var osName = GetOperatingSystemName();

			OS.OSName = osName;

			#endregion

			#region Detect Major-Minor Version

			var version = GetMajorMinorVersion();

			// it is maybe Win7 or Win8
			if (version.Major == 6 && (version.Minor == 2 || version.Minor == 3))
			{
				// it is definitely Win10
				if (IsWin10())
				{
					version.Major = 10;
					version.Minor = 0;
				}
			}

			OS.MajorMinorVersion = version;

			#endregion

			OS.Build = GetCurrentBuild();

			OS.PlatformdId = Environment.OSVersion.Platform.ToString();
			OS.ServicePack = Environment.OSVersion.ServicePack;
			OS.OSBit = Environment.Is64BitOperatingSystem ? 64 : 32;

			return OS;
		}

		/// <summary>
		/// Get operating system full name
		/// </summary>
		/// <returns></returns>
		public static OSName GetOperatingSystemName()
		{
			var result = RegistryHelper.ReadKey(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", null);

			if (string.IsNullOrEmpty(result))
			{
				return null;
			}

			var name = Regex.Match(result, @"(Windows)( ).{1,2}([0-9.])").Groups;
			var version = Regex.Match(result, @"([0-9.]){1,3}").Groups;

			return new OSName()
			{
				Name = name.Count > 0 ? name[0].Value : string.Empty,
				VersionName = version.Count > 0 ? Double.Parse(version[0].Value) : 0,
				Edition = GetOSEdition()
			};
		}

		/// <summary>
		/// Get operating system major and minor versions
		/// </summary>
		/// <returns></returns>
		public static MajorMinorVersion GetMajorMinorVersion()
		{
			var result = RegistryHelper.ReadKey(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentVersion", null);

			if (string.IsNullOrEmpty(result))
			{
				return null;
			}

			var Mm = result.Split('.');

			return new MajorMinorVersion()
			{
				Major = Int32.Parse(Mm[0]),
				Minor = Int32.Parse(Mm[1])
			};
		}

		/// <summary>
		/// Windows 10 checks need registry records only
		/// </summary>
		/// <returns></returns>
		public static bool IsWin10()
		{
			var result = RegistryHelper.ReadKey(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", null);

			if (string.IsNullOrEmpty(result))
			{
				return false;
			}

			return result.IndexOf("Windows 10") != -1 ? true : false;
		}

		/// <summary>
		/// Get current os build
		/// </summary>
		/// <returns></returns>
		public static int GetCurrentBuild()
		{
			var result = RegistryHelper.ReadKey(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuild", null);

			if (string.IsNullOrEmpty(result))
			{
				return 0;
			}

			return Int32.Parse(result);
		}

		/// <summary>
		/// Get OS edition
		/// </summary>
		/// <returns></returns>
		public static string GetOSEdition()
		{
			var result = RegistryHelper.ReadKey(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "EditionID", null);

			if (string.IsNullOrEmpty(result))
			{
				return string.Empty;
			}

			return result;
		}


		#endregion
	}

	public class OperatingSystem
	{
		public OSName OSName { get; set; }
		public MajorMinorVersion MajorMinorVersion { get; set; }
		public int Build { get; set; }

		public string PlatformdId { get; set; }
		public string ServicePack { get; set; }
		public int OSBit { get; set; }
	}

	public class MajorMinorVersion
	{
		public int Major { get; set; }
		public int Minor { get; set; }
	}

	public class OSName
	{
		public string Name { get; set; }
		public double VersionName { get; set; }
		public string Edition { get; set; }
	}
}