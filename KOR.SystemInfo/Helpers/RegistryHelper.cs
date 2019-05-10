using Microsoft.Win32;
using System;

namespace KOR.SystemInfo.Helpers
{
	public class RegistryHelper
	{
        /// <summary>
        /// Registy Key Reader
        /// </summary>
        /// <param name="registryRoot">Enum RegistryRoot</param>
        /// <param name="field">part field</param>
        /// <param name="key">property key</param>
        /// <returns></returns>
        public static string ReadKey(RegistryHive registryRoot, string field, string key, string defaultValue)
		{
            RegistryKey registryKey;

            // create registry key

            if (Environment.Is64BitOperatingSystem)
			{
				registryKey = RegistryKey.OpenBaseKey(registryRoot, RegistryView.Registry64);
			}
			else
			{
				registryKey = RegistryKey.OpenBaseKey(registryRoot, RegistryView.Registry32);
			}

            string value = registryKey.OpenSubKey(field).GetValue(key, defaultValue).ToString();

            registryKey.Close();

			return value;
		}
	}
}
